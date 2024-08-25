using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script sets player to become child of platform on collision and uncouples on trigger exit
//Resolves player jitter on platform collision

public class PlatformCollision : MonoBehaviour

{

    [SerializeField] string playerTag = "Player";
    [SerializeField] Transform platform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            other.gameObject.transform.parent = platform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            other.gameObject.transform.parent = null;
        }
    }
}
