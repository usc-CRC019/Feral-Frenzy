using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Level : MonoBehaviour
{
    public Player player;
    public GameObject tmp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetLevelText(int level)
    {
        string text = level.ToString();
        tmp.GetComponent<TMPro.TMP_Text>().SetText(text);
    }
}
