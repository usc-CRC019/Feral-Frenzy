using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UI_Level : MonoBehaviour
{
    public Player player;
    public GameObject tmp;
    public GameObject fill;
    public GameObject slider;
    private Image image;
    private Slider slide;

    // Start is called before the first frame update
    void Start()
    {
        slide = slider.GetComponent<Slider>();
        image = fill.transform.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        //switch (player.playerLevel)
        //{
        //    case 0:
        //        slide.minValue = 0;
        //        slide.maxValue = 59;
        //        break;
        //    case 1:
        //        slide.minValue = 60;
        //        slide.maxValue = 119;
        //        break;
        //    case 2:
        //        slide.minValue = 120;
        //        slide.maxValue = 199;
        //        break;
        //    case 3:
        //        slide.minValue = 200;
        //        slide.maxValue = 299;
        //        break;
        //    case 4:
        //        slide.minValue = 300;
        //        slide.maxValue = 399;
        //        break;
        //    case 5:
        //        slide.minValue = 400;
        //        slide.maxValue = 499;
        //        break;
        //    case 6:
        //        slide.minValue = 500;
        //        slide.maxValue = 599;
        //        break;
        //    case 7:
        //        slide.minValue = 600;
        //        slide.maxValue = 699;
        //        break;
        //    case 8:
        //        slide.minValue = 700;
        //        slide.maxValue = 799;
        //        break;
        //    case 9:
        //        slide.minValue = 800;
        //        slide.maxValue = 899;
        //        break;
        //}

        switch (player.currentLevel)
        {
            default:
                slide.minValue = 0;
                slide.maxValue = 50;
                break;
            case "Level1":
                slide.minValue = 0;
                slide.maxValue = 250;
                break;
        }

        slide.value = player.bankedXP;

        if (player.bankedXP <= 0f)
        {
            //Fade out
            image.CrossFadeAlpha(0f, 0.2f, false);
        }
        else
        {
            //Fade in
            image.CrossFadeAlpha(1f, 0.5f, false);
        }
    }

    public void SetLevelText(int level)
    {
        string text = level.ToString();
        tmp.GetComponent<TMPro.TMP_Text>().SetText(text);
    }
}
