using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;
    private float gravityValue = 25;

    [SerializeField]
    [Header("Player Speed")]
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(hInput, 0, vInput);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;
        //moveDirection = Vector3.ClampMagnitude(moveDirection, speed);

        if (!characterController.isGrounded)
        {
            playerVelocity.y -= gravityValue * Time.deltaTime;
        }

        characterController.Move((moveDirection + playerVelocity) * Time.deltaTime);
    }
}
