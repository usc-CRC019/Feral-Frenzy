using Cinemachine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerLevel;
    public int bankedXP;
    public int storedXP;
    public int reputation;

    public float playerHealth;
    public float playerStamina;
    public float playerStamRegenDelay;

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

    public UI_Level playerLevelUI;

    public GameObject playerDeathUI;
    public GameObject respawnPoint;

    public GameObject mainCam;
    public Image reticle;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement = GetComponent<PlayerMovement>();
        playerAlive = true;
        Physics.queriesHitTriggers = false;
        playerDeathUI.GetComponent<UI_PlayerDeath>().FadeOut();
        
    }

    // Update is called once per frame
    void Update()
    {
        StaminaChecks();
        HealthChecks();
        Aim();
    }

    private void StaminaChecks()
    {
        if (playerMovement.isGrounded && !playerMovement.isSprinting && playerStamina < 100f && Time.time >= playerStamRegenDelay)
        {
            playerStamina += (sprintStaminaCost * 0.5f) * Time.deltaTime;
        }

        if (playerStamina > 100f)
        {
            playerStamina = 100f;
        }
    }

    private void HealthChecks()
    {
        if (playerHealth <= 0f)
        {
            KillPlayer();
        }

        if (playerHealth > 100f)
        {
            playerHealth = 100f;
        }

        if (playerHealth < 100f && playerAlive)
        {
            playerHealth += 0.01f * Time.deltaTime;
        }
    }

    private void Aim()
    {
        if (!playerMovement.isWallrunning && !playerMovement.isJumping && Input.GetKey(KeyCode.Mouse1))
        {
            mainCam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.MoveTowards(mainCam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, 25f, 0.2f);
            reticle.CrossFadeAlpha(1f, 0.1f, false);
        }
        else
        {
            mainCam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView = Mathf.MoveTowards(mainCam.GetComponent<CinemachineFreeLook>().m_Lens.FieldOfView, 40f, 0.2f);
            reticle.CrossFadeAlpha(0f, 0.1f, false);
        }
    }

    public void JumpStaminaCost()
    {
        playerStamina -= jumpStaminaCost;
    }

    public void KillPlayer()
    {
        if (playerAlive)
        {
            playerAlive = false;
            playerHealth = 0f;
            GetComponent<CharacterController>().enabled = false;
            playerDeathUI.GetComponent<UI_PlayerDeath>().UI_DeathProcess();
        }
    }

    public void RespawnPlayer()
    {
        playerAlive = true;
        playerHealth = 100f;
        transform.position = respawnPoint.transform.position;
        GetComponent<CharacterController>().enabled = true;
    }

    public void SetStamRegenTime()
    {
        playerStamRegenDelay = Time.time + 2f;
    }

    public void GiveTunaSnack()
    {
        playerHealth += 30f;
        playerStamina += 100;
    }

    public void HomeVisit()
    {
        playerHealth = 100f;
        playerStamina = 100f;
        storedXP = 50;
        StartCoroutine(XPTrickle());
    }

    public void PlayerLevelCalc()
    {

        if (bankedXP <= 59)
        {
            playerLevel = 0;
        }
        else if (bankedXP >= 60 && bankedXP <= 119)
        {
            playerLevel = 1;
        }
        else if (bankedXP >= 120 && bankedXP <= 199)
        {
            playerLevel = 2;
        }
        else if (bankedXP >= 200 && bankedXP <= 299)
        {
            playerLevel = 3;
        }
        else if (bankedXP >= 300 && bankedXP <= 399)
        {
            playerLevel = 4;
        }
        else if (bankedXP >= 400 && bankedXP <= 499)
        {
            playerLevel = 5;
        }
        else if (bankedXP >= 500 && bankedXP <= 599)
        {
            playerLevel = 6;
        }
        else if (bankedXP >= 600 && bankedXP <= 699)
        {
            playerLevel = 7;
        }
        else if (bankedXP >= 700 && bankedXP <= 799)
        {
            playerLevel = 8;
        }
        else if (bankedXP >= 800 && bankedXP <= 899)
        {
            playerLevel = 9;
        }
        else if (bankedXP >= 900)
        {
            playerLevel = 10;
        }

        playerLevelUI.SetLevelText(playerLevel);
    }

    public void TeleportPlayer(GameObject endGate)
    {
        CharacterController characterController = GetComponent<CharacterController>();

        characterController.enabled = false;
        characterController.transform.position = endGate.transform.position;
        characterController.enabled = true;
    }

    private IEnumerator XPTrickle()
    {
        while (storedXP > 0)
        {
            bankedXP++;
            storedXP--;
            PlayerLevelCalc();
            yield return new WaitForSeconds(0.03f);
        }
    }
}
