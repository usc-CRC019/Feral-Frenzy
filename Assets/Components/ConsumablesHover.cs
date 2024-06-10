using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesHover : MonoBehaviour
{
    private float currentY;

    // Start is called before the first frame update
    void Start()
    {
        currentY = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentY < 360)
        {
            currentY += 30 * Time.deltaTime;
        }
        else
        {
            currentY = 0f;
        }

        transform.rotation = Quaternion.Euler(0f, currentY, 0f);
    }
}
