using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 playerVelocity;

    public bool isGrounded;
    public bool isSprinting;
    public bool isJumping;
    public bool isWallrunning;

    public Vector3 currentMoveVector;
    

    [SerializeField]
    private GameObject playerCamera;
    public Transform player;

    private Player playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GetComponent<Player>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        var hInput = Input.GetAxis("Horizontal");
        var vInput = Input.GetAxis("Vertical");

        moveDirection = new Vector3(hInput, 0, vInput);
        moveDirection = transform.TransformDirection(moveDirection);

        GroundedCheck();
        Jump();
        SprintCheck();
        Sprint();
        Wallrun();

        player.transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);

        
        moveDirection = moveDirection * playerStats.moveSpeed;
        moveDirection = Vector3.ClampMagnitude(moveDirection, playerStats.moveSpeed);

        if (!isGrounded && isWallrunning == false)
        {
            playerVelocity.y -= playerStats.gravityValue * Time.deltaTime;
        }
        else if (isWallrunning)
        {
            playerVelocity.y -= playerStats.wallrunGravityValue * Time.deltaTime;
        }
        

        characterController.Move((moveDirection + playerVelocity) * Time.deltaTime);

        
        //Debug.Log(isGrounded);
    }


    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            currentMoveVector = moveDirection;
            playerVelocity.y = Mathf.Sqrt(playerStats.jumpHeight * playerStats.gravityValue);
            isJumping = true;
        }

        if (isJumping)
        {
            moveDirection += (currentMoveVector * 0.75f);
        }
        else
        {
            currentMoveVector = Vector3.zero;
        }
        
    }

    private void Wallrun()
    {
        if (isJumping)
        {

            RaycastHit hitLeft;
            RaycastHit hitRight;
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position - new Vector3(0, 0.05f, 0), characterController.radius * 0.35f, -transform.right, out hitLeft, 0.2f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    //this.transform.SetParent(hitLeft.transform, true);
                    isWallrunning = true;
                    playerVelocity.y = 0;
                }
            }
            else
            {
                isWallrunning = false;
            }

            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position - new Vector3(0,0.05f,0), characterController.radius * 0.35f, transform.right, out hitRight, 0.2f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    //this.transform.SetParent(hitRight.transform, true);
                    isWallrunning = true;
                    playerVelocity.y = 0;
                }
            }
            else
            {
                isWallrunning = false;
            }

            //Debug.Log(isWallrunning);
        }
    }

    private void SprintCheck()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftShift)) 
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    private void Sprint()
    {
        if (isSprinting)
        {
            playerStats.moveSpeed = 5f;
        }
        else
        {
            playerStats.moveSpeed = 3f;
        }
    }

    private void GroundedCheck()
    {
        RaycastHit hit;

        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, characterController.radius * 0.25f, -transform.up, out hit, 0.15f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor))
        {
            isGrounded = true;
            isJumping = false;
        }
        else
        {
            isGrounded = false;
        }
    }
}