using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject player;

    [SerializeField]
    private GameObject endGate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<Player>().TeleportPlayer(endGate);
            
        }
    }
}
