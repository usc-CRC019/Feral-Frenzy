using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    private float destroyTime;

    private void Awake()
    {
        bulletRigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        destroyTime = Time.time + 10f;
        bulletRigidbody.velocity = transform.forward * 20f;
    }

    private void Update()
    {
        if (destroyTime < Time.time)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);

        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }


}
