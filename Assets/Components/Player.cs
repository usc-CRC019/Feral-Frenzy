using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerLevel;
    public int reputation;

    public float playerHealth;
    public float playerStamina;

    public float walkMoveSpeed;
    public float sprintMoveSpeed;
    public float moveSpeed;


    public float jumpHeight;
    public float jumpStaminaCost;
    public float sprintStaminaCost;
    public float gravityValue;
    public float wallrunGravityValue;

    public bool playerAlive;


    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement = GetComponent<PlayerMovement>();
        playerAlive = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        StaminaChecks();
        HealthChecks();
    }

    private void StaminaChecks()
    {
        if (playerMovement.isGrounded && !playerMovement.isSprinting && playerStamina < 100f)
        {
            playerStamina += (sprintStaminaCost * 0.5f) * Time.deltaTime;

            if (playerStamina > 100f)
            {
                playerStamina = 100f;
            }
        }
    }

    private void HealthChecks()
    {
        if (playerHealth <= 0f)
        {
            KillPlayer();
        }

        if (playerHealth < 100f && playerAlive)
        {
            playerHealth += 0.01f * Time.deltaTime;
        }
    }

    public void JumpStaminaCost()
    {
        playerStamina -= jumpStaminaCost;
    }

    public void KillPlayer()
    {
        playerAlive = false;
        playerHealth = 0f;
        GetComponent<CharacterController>().enabled = false;
        
    }
}
