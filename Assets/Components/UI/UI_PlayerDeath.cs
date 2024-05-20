using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerDeath : MonoBehaviour
{
    public Image blackScreen;

    public Image respawnBackground;
    private float respawnBackgroundCurrentAlpha;

    public Image respawnImage;
    private float respawnImageCurrentAlpha;

    public GameObject deathTextObject;
    private TextMeshProUGUI deathText;

    public Button respawnButton;
    private TextMeshProUGUI respawnText;

    public Player player;



    private void Awake()
    {
        deathText = deathTextObject.GetComponent<TextMeshProUGUI>();
        respawnText = respawnButton.GetComponentInChildren<TextMeshProUGUI>();  

        respawnButton.interactable = false;
        respawnBackground.CrossFadeAlpha(0f, 0f, false);
        respawnImage.CrossFadeAlpha(0f, 0f, false);
        deathText.enabled = false;
        respawnText.enabled = false;

    }

    // Start is called before the first frame update
    void Start()
    {
        blackScreen.CrossFadeAlpha(0f, 4f, false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            player.GetComponent<Player>().playerHealth = 0f;
        }
        
    }

    public void FadeIn()
    {
        blackScreen.CrossFadeAlpha(1f, 2f, false);
        
    }

    public void FadeOut()
    {
        blackScreen.CrossFadeAlpha(0f, 2f, false);
        
    }

    public void UI_DeathProcess()
    {
        StartCoroutine(UI_DeathFading());
    }

    private IEnumerator UI_DeathFading()
    {
        FadeIn();

        yield return new WaitForSeconds(2.05f);

        respawnBackground.CrossFadeAlpha(1f, 0f, false);
        respawnImage.CrossFadeAlpha(1f, 0f, false);
        respawnButton.interactable = true;
        respawnText.enabled = true;
        deathText.enabled = true;
        Cursor.lockState = CursorLockMode.None;

        FadeOut();
    }

    public void UI_RespawnButtonClicked()
    {
        respawnButton.interactable = false;
        respawnText.enabled = false;
        StartCoroutine(UI_RespawnProcess());
    }

    private IEnumerator UI_RespawnProcess()
    {
        FadeIn();

        yield return new WaitForSeconds(2.05f);

        respawnBackground.CrossFadeAlpha(0f, 0f, false);
        respawnImage.CrossFadeAlpha(0f, 0f, false);
        deathText.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;

        player.RespawnPlayer();

        FadeOut();
    }

}
