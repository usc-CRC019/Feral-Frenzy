using System.Collections;
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
    public bool isFalling;

    private float wallrunCooldown;
    private float wallrunStartTime;
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
        wallrunStartTime = 0f;
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
        FallingCheck();

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
            //playerVelocity.y -= playerStats.wallrunGravityValue * Time.deltaTime;
        }


        characterController.Move((moveDirection + playerVelocity) * Time.deltaTime);

        //Debug.Log(isGrounded);
        //Debug.Log(characterController.velocity.magnitude);
    }


    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isGrounded && playerStats.playerStamina >= playerStats.jumpStaminaCost)
        {
            playerStats.moveSpeed = playerStats.walkMoveSpeed - 1;

            if (builtUpJumpPower <= 3f)
            {
                builtUpJumpPower += 0.01f;
            }
            else
            {
                builtUpJumpPower = 3;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && isGrounded && playerStats.playerStamina >= playerStats.jumpStaminaCost)
        {
            wallrunCooldown = Time.time + 0.35f;
            playerStats.JumpStaminaCost();
            playerStats.SetStamRegenTime();
            currentMoveVector = moveDirection;
            playerVelocity.y = Mathf.Sqrt((playerStats.jumpHeight + builtUpJumpPower) * playerStats.gravityValue);
            builtUpJumpPower = 0f;
            isJumping = true;
            StartCoroutine(GradualJumpSpeed());
        }

        if (Input.GetKeyDown(KeyCode.Space) && isWallrunning)
        {
            wallrunCooldown = Time.time + 0.35f;

            currentMoveVector = moveDirection;
            playerVelocity.y = Mathf.Sqrt((playerStats.jumpHeight * playerStats.gravityValue) + 10);
            isJumping = true;
            isWallrunning = false;
            isWallrunningLeft = false;
            isWallrunningRight = false;
        }

        //Adds movement vector the player had at beginning of jump throughout the jump
        if (isJumping)
        {
            moveDirection += (currentMoveVector * 0.75f);
        }
        else if (!isWallrunning) 
        {
            currentMoveVector = Vector3.zero;
        }


        RaycastHit hit;
        if (isJumping)
        {
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, characterController.radius * 0.25f, transform.up, out hit, 0.15f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor))
            {
                playerVelocity.y -= 0.2f;
            }
            
        }
        
    }

    private void Wallrun()
    {

        if (!isGrounded && wallrunCooldown <= Time.time)
        {
            RaycastHit hitLeft;
            RaycastHit hitRight;

            //Left wall check
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, 0.09f, -transform.right, out hitLeft, 0.19f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (Input.GetKey(KeyCode.A))
                {
                    isWallrunningLeft = true;
                    moveDirection.y = 0;
                }
            }
            else
            {
                isWallrunningLeft = false;
            }
            

            //Right wall check
            if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, 0.09f, transform.right, out hitRight, 0.19f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Both))
            {
                if (Input.GetKey(KeyCode.D))
                {
                    isWallrunningRight = true;
                    moveDirection.y = 0;
                }
            }
            else
            {
                isWallrunningRight = false;
            }


            if (isWallrunningLeft || isWallrunningRight && !isGrounded)
            {
                if (wallrunStartTime == 0)
                {
                    wallrunStartTime = Time.time + 1.5f;
                }

                if (wallrunStartTime <= Time.time)
                {
                    isWallrunning = false;
                    isWallrunningLeft = false;
                    isWallrunningRight = false;
                    return;
                }


                playerVelocity.y = 0;
                StartCoroutine(GradualJumpSpeed());
                characterController.Move((currentMoveVector * playerStats.sprintMoveSpeed) * Time.deltaTime);
                isWallrunning = true;
                isJumping = false;
            }
            else
            {
                wallrunStartTime = 0;
                isWallrunning = false;
                isWallrunningLeft = false;
                isWallrunningRight = false;
            }
        }
    }

    private void SprintCheck()
    {
        if (isGrounded && Input.GetKey(KeyCode.LeftShift) && playerStats.playerStamina >= 0 && moveDirection != Vector3.zero && builtUpJumpPower == 0f) 
        {
            isSprinting = true;
            playerStats.SetStamRegenTime();
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
            playerStats.playerStamina -= 4f * Time.deltaTime;
        }
        else if (!isJumping)
        {
            playerStats.moveSpeed = playerStats.walkMoveSpeed;
        }
    }

    private void GroundedCheck()
    {
        RaycastHit hit;

        if (RotaryHeart.Lib.PhysicsExtension.Physics.SphereCast(transform.position, characterController.radius * 0.25f, -transform.up, out hit, 0.15f, RotaryHeart.Lib.PhysicsExtension.PreviewCondition.Editor))
        {
            //If player hits ground fast enough apply fall damage
            if (playerVelocity.y < -15f)
            {
                playerStats.playerHealth += playerVelocity.y * 0.5f;
            }

            
            isGrounded = true;
            //isFalling = false;

            isJumping = false;
            isWallrunning = false;
            isWallrunningLeft = false;
            isWallrunningRight = false;
            currentMoveVector = Vector3.zero;
            playerVelocity.y = 0f;
            //StopAllCoroutines();
            playerStats.moveSpeed = playerStats.walkMoveSpeed;
        }
        else
        {
            isGrounded = false;
            //isFalling = true;
        }
    }

    private void FallingCheck()
    {
        if (!isGrounded && !isWallrunning && !isJumping)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
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