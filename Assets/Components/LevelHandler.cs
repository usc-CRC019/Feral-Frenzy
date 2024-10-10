using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public Player player;
    public int level1_SnackCount = 5;
    public int level2_SnackCount = 7;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (player.currentLevel)
        {
            default:
                break;
            case "Level1":
                if (player.playerTunaSnackCount == level1_SnackCount)
                {
                    if (Input.GetKey(KeyCode.R))
                    {
                        player.RestartCurrentLevel();
                    }
                }
                break;

        }
    }
}
