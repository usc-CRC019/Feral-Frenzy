using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Dialogue : MonoBehaviour
{

    //UI References
    [SerializeField]
    private GameObject dialogueCanvas;

  [SerializeField]
    private TMP_Text speakerText;

    [SerializeField]
    private TMP_Text dialogueText;

    [SerializeField]
    private Image portraitImage;

    //Dialogue content

    [SerializeField]
    private string[] speaker;

    [SerializeField]
    [TextArea]
    private string[] dialogueWords;

    [SerializeField]
    private Sprite[] portrait;

    private bool dialogueActivated;
    private int step;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact") && dialogueActivated == true)
        {
            if (step >= speaker.Length)
            {
                dialogueCanvas.SetActive(false);
                step = 0;
            }

            else
            {
                dialogueCanvas.SetActive(true);
                speakerText.text = speaker[step];
                dialogueText.text = dialogueWords[step];
                portraitImage.sprite = portrait[step];
                step += 1;
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag =="Player")
        {
            dialogueActivated = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        dialogueActivated = false;
        dialogueCanvas.SetActive(false);
    }
}
