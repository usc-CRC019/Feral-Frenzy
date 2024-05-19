using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerInteract : MonoBehaviour
{
    public GameObject player;
    public GameObject cam;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInteraction();
    }

    private void PlayerInteraction()
    {
        RaycastHit hitInfo;

        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(player.transform.position, player.GetComponent<CharacterController>().height * 0.01f, cam.transform.forward, out hitInfo, 2f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (hitInfo.transform.tag == "TunaSnack")
                {
                    player.GetComponent<Player>().GiveTunaSnack();
                    Destroy(hitInfo.transform.gameObject);
                }
            }

        }
    }
}
