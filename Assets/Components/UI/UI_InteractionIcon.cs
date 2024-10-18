using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InteractionIcon : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
    }
}
