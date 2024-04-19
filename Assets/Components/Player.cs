using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerLevel;
    public int reputation;
    public float moveSpeed;
    public float jumpHeight;
    public float gravityValue;
    public float wallrunGravityValue;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
