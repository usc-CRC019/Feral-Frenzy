using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBarrier : MonoBehaviour
{
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player.gameObject)
        {
            player.RespawnPlayer();
            Debug.Log("Hit");
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        player.RespawnPlayer();
    //    }
    //}
}
