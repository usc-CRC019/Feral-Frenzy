using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_MainMenuButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string buttonName;
    public MainMenu menu;

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        switch (buttonName)
        {
            default:
                break;
            case "Start":
                menu.background.sprite = menu.backgrounds[1];
                break;
            case "Options":
                menu.background.sprite = menu.backgrounds[2];
                break;
            case "Exit":
                menu.background.sprite = menu.backgrounds[3];
                break;
        }

        //Output to console the GameObject's name and the following message
        //Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //public void Update()
    //{
    //    Debug.Log(lastHovered);
    //}

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        menu.background.sprite = menu.backgrounds[0];
        //Output the following message with the GameObject's name
        //Debug.Log("Cursor Exiting " + name + " GameObject");
    }



}
