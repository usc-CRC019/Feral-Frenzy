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


    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement = GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        StaminaChecks();
        Debug.Log(playerStamina);
    }

    private void StaminaChecks()
    {
        if (playerMovement.isGrounded && !playerMovement.isSprinting && playerStamina < 100f)
        {
            playerStamina += (sprintStaminaCost - 1f) * Time.deltaTime;

            if (playerStamina > 100f)
            {
                playerStamina = 100f;
            }
        }
    }

    public void JumpStaminaCost()
    {
        playerStamina -= jumpStaminaCost;
    }

    
}
