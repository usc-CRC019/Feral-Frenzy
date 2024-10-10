using UnityEngine;

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

        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.queriesHitTriggers = true;
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(player.transform.position, player.GetComponent<CharacterController>().height * 0.01f, cam.transform.forward, out hitInfo, 2f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (hitInfo.transform.tag == "TunaSnack")
                {
                    player.GetComponent<Player>().GiveTunaSnack();
                    Destroy(hitInfo.transform.gameObject);
                }

                if (hitInfo.transform.tag == "PlayerHouse")
                {
                    player.GetComponent<Player>().HomeVisit();
                }
            }
        }

        Physics.queriesHitTriggers = false;
    }
}
