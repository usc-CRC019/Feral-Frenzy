using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    PlayerMovement PlayerMovementScript;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        PlayerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        bool isLeft = animator.GetBool("isLeft");
        bool isRight = animator.GetBool("isRight");
        bool isFalling = animator.GetBool("isFalling");
        bool isJumping = animator.GetBool("isJumping");
        bool isWallClimbing = animator.GetBool("isWallClimbing");
        bool isWallRunning = animator.GetBool("isWallRunning");
        bool isJumpingForward = animator.GetBool("isJumpingForward");
        bool isRunning = animator.GetBool("isRunning");
        bool isWalking = animator.GetBool("isWalking");
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");
        if (!isWalking && forwardPressed)
        {
            animator.SetBool("isWalking", true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool("isWalking", false);
        }
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool("isRunning", true);
        }
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool("isRunning", false);
        }
        if (!isJumping && jumpPressed)
        {
            animator.SetBool("isJumping", true);
        }
        if (isJumping && !jumpPressed)
        {
            animator.SetBool("isJumping", false);
        }
        if (!isJumpingForward && (forwardPressed && jumpPressed))
        {
            animator.SetBool("isJumpingForward", true);
        }
        if (isJumpingForward && !jumpPressed)
        {
            animator.SetBool("isJumpingForward", false);
        }
        if (!isLeft && (leftPressed && !forwardPressed))
        {
            animator.SetBool("isLeft", true);
        }
        if (isLeft && (!leftPressed && !forwardPressed))
        {
            animator.SetBool("isLeft", false);
        }
        if (!isRight && (rightPressed && !forwardPressed))
        {
            animator.SetBool("isRight", true);
        }
        if (isRight && (rightPressed && !forwardPressed))
        {
            animator.SetBool("isRight", false);
        }
        if (PlayerMovementScript.GetComponent<PlayerMovement>().isWallrunning == true)
        {
            animator.SetBool("isWallRunning", true);
        }
        else if (PlayerMovementScript.GetComponent<PlayerMovement>().isWallrunning == false)
        {
            animator.SetBool("isWallRunning", false);
        }
        if (PlayerMovementScript.GetComponent<PlayerMovement>().isWallClimbing == true)
        {
            animator.SetBool("isWallClimbing", true);
        }
        else if (PlayerMovementScript.GetComponent<PlayerMovement>().isWallClimbing == false)
        {
            animator.SetBool("isWallClimbing", false);
        }
        if (PlayerMovementScript.GetComponent<PlayerMovement>().isFalling == true)
        {
            animator.SetBool("isFalling", true);
        }
        else if (PlayerMovementScript.GetComponent<PlayerMovement>().isFalling == false)
        {
            animator.SetBool("isFalling", false);
        }
    }
}

    