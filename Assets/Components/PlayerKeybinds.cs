using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerKeybinds : MonoBehaviour
{
    private PlayerMovement playerMovement;

    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    // public Text forward, back, left, right, jump, sprint; *delete // for when UI is in*

    private GameObject currentKey;

    //Colors for UI when you select a rechanging of a key, can change
    private Color32 normal = new Color32(39, 171, 249, 255);
    private Color32 selected = new Color32(239, 116, 36, 255);

    void Start()
    {
        keys.Add("Forward", KeyCode.W);
        keys.Add("Back", KeyCode.S);
        keys.Add("Left", KeyCode.L);
        keys.Add("Right", KeyCode.R);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Sprint", KeyCode.LeftShift);
    }

    void Update()
    {
        
    }

    //After clicked the UI it waits for next key
    void OnGUI()
    {
        if (currentKey != null)
        {
            Event e = Event.current;
            if(e.isKey)
            {
                keys[currentKey.name] = e.keyCode;
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                currentKey.GetComponent<Image>().color = normal;
                currentKey = null;
            }
        }
    }

    // Once clicked the UI it wont change to mouse button, GameObject is button for UI
    public void ChangeKey(GameObject clicked)
    {
        if (currentKey != null)
        {
            currentKey.GetComponent<Image>().color = normal;
        }

        currentKey = clicked;
        //changes the UI color once selected to rebind
        currentKey.GetComponent<Image>().color = selected;
    }
}
