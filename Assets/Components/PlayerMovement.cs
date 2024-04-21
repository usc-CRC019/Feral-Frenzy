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

    public float wallrunCooldown;

    bool isWallrunningLeft;
    bool isWallrunningRight;

    public Vector3 currentMoveVector;

    private float builtUpJumpPower;
    

    [SerializeField]
    private GameObject playerCamera;

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
        SprintCheck();
        Sprint();
        Wallrun();
        Jump();

        //rotate player to match camera rotation unless wallrunning
        if (!isWallrunning)
        {
            this.transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
        }
        

        moveDirection = moveDirection * playerStats.moveSpeed;
        moveDirection = Vector3.ClampMagnitude(moveDirection, playerStats.moveSpeed);

        if (!isGrounded && !isWallrunning)
        {
            playerVelocity.y -= playerStats.gravityValue * Time.deltaTime;
        }
        else if (isWallrunning)
        {
            playerVelocity.y -= playerStats.wallrunGravityValue * Time.deltaTime;
        }
        

        characterController.Move((moveDirection + playerVelocity) * Time.deltaTime);


        //Debug.Log(isGrounded);
        //Debug.Log(characterController.velocity.magnitude);
    }


    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            if (builtUpJumpPower <= 3f)
            {
                builtUpJumpPower += 0.01f;
            }
            else
            {
                builtUpJumpPower = 3;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded)
        {
            currentMoveVector = moveDirection;
            playerVelocity.y = Mathf.Sqrt((playerStats.jumpHeight + builtUpJumpPower) * playerStats.gravityValue);
            builtUpJumpPower = 0f;
            isJumping = true;
            StartCoroutine(GradualJumpSpeed());
        }

        if (Input.GetKeyDown(KeyCode.Space) && isWallrunning)
        {
            isWallrunning = false;
            isWallrunningLeft = false;
            isWallrunningRight = false;

            wallrunCooldown = Time.time + 0.35f;

            currentMoveVector = moveDirection;
            playerVelocity.y = Mathf.Sqrt((playerStats.jumpHeight * playerStats.gravityValue) + 10);
            isJumping = true;
        }

        //Adds movement vector the player had at beginning of jump throughout the jump
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

        if (!isGrounded && wallrunCooldown <= Time.time)
        {
            RaycastHit hitLeft;
            RaycastHit hitRight;

            //Left wall check
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, 0.1f, -transform.right, out hitLeft, 0.2f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    isWallrunningLeft = true;
                }
                else
                {
                    isWallrunningLeft = false;
                }
            }
            

            //Right wall check
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, 0.1f, transform.right, out hitRight, 0.2f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    isWallrunningRight = true;
                    
                }
                else
                {
                    isWallrunningRight = false;
                }
            }
            

            if (isWallrunningLeft || isWallrunningRight && !isGrounded)
            {
                playerVelocity.y = 0;
                isWallrunning = true;
                isJumping = false;
            }
            else
            {
                isWallrunning = false;
                isWallrunningLeft = false;
                isWallrunningRight = false;
            }
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
            playerStats.moveSpeed = playerStats.sprintMoveSpeed;
        }
        else if (!isJumping)
        {
            playerStats.moveSpeed = playerStats.walkMoveSpeed;
        }
    }

    private void GroundedCheck()
    {
        RaycastHit hit;

        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, characterController.radius * 0.25f, -transform.up, out hit, 0.15f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
        {
            isGrounded = true;

            isJumping = false;
            isWallrunning = false;
            isWallrunningLeft = false;
            isWallrunningRight = false;
            currentMoveVector = Vector3.zero;
        }
        else
        {
            isGrounded = false;
        }
    }

    private IEnumerator GradualJumpSpeed()
    {
        while (playerStats.moveSpeed < playerStats.sprintMoveSpeed -0.1f)
        {
            playerStats.moveSpeed = Mathf.Lerp(playerStats.moveSpeed, playerStats.sprintMoveSpeed, 0.05f);
            yield return null;
        }

        playerStats.moveSpeed = playerStats.sprintMoveSpeed;
    }
}