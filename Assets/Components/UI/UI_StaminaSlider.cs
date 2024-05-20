using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_StaminaSlider : MonoBehaviour
{
    private Player player;
    private Slider slider;
    public GameObject fill;
    private Image image;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        image = fill.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.playerStamina;
        

        if (player.playerStamina <= 0f)
        {
            //Fade out
            image.CrossFadeAlpha(0f, 0.2f, false);
        }
        else
        {
            //Fade in
            image.CrossFadeAlpha(1f, 0.2f, false);
        }
    }
}
