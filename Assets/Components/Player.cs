using Cinemachine;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int playerTunaSnackCount;
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
    public float wallclimbGravityValue;

    public bool playerAlive;


    private PlayerMovement playerMovement;

    public UI_Level playerLevelUI;

    public GameObject playerDeathUI;
    public GameObject respawnPoint;

    public GameObject mainCam;
    public Image reticle;

    public string currentLevel;

    public Timer timer;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerMovement = GetComponent<PlayerMovement>();
        playerAlive = true;
        Physics.queriesHitTriggers = false;
        playerDeathUI.GetComponent<UI_PlayerDeath>().FadeOut();

        currentLevel = SceneManager.GetActiveScene().name;
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
        playerStamina = 100f;
        transform.position = respawnPoint.transform.position;
        GetComponent<CharacterController>().enabled = true;
    }

    public void SetStamRegenTime()
    {
        playerStamRegenDelay = Time.time + 2f;
    }

    public void GiveTunaSnack()
    {
        playerHealth = 100f;
        playerStamina = 100f;
        storedXP = 50;
        playerTunaSnackCount++;
        StartCoroutine(XPTrickle());
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

        //if (bankedXP <= 49)
        //{
        //    playerLevel = 0;
        //}
        //else if (bankedXP >= 50 && bankedXP <= 99)
        //{
        //    playerLevel = 1;
        //}
        //else if (bankedXP >= 100 && bankedXP <= 149)
        //{
        //    playerLevel = 2;
        //}
        //else if (bankedXP >= 150 && bankedXP <= 199)
        //{
        //    playerLevel = 3;
        //}
        //else if (bankedXP >= 200 && bankedXP <= 249)
        //{
        //    playerLevel = 4;
        //}
        //else if (bankedXP >= 250 && bankedXP <= 299)
        //{
        //    playerLevel = 5;
        //}
        //else if (bankedXP >= 300 && bankedXP <= 349)
        //{
        //    playerLevel = 6;
        //}
        //else if (bankedXP >= 350 && bankedXP <= 399)
        //{
        //    playerLevel = 7;
        //}
        //else if (bankedXP >= 400 && bankedXP <= 449)
        //{
        //    playerLevel = 8;
        //}
        //else if (bankedXP >= 450 && bankedXP <= 499)
        //{
        //    playerLevel = 9;
        //}
        //else if (bankedXP >= 500)
        //{
        //    playerLevel = 10;
        //}

        

        playerLevelUI.SetLevelText(playerTunaSnackCount);
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

    public void RestartCurrentLevel()
    {
        playerTunaSnackCount = 0;
        bankedXP = 0;
        PlayerLevelCalc();
        playerHealth = 100;
        playerStamina = 100;
        timer.RestartTimer();
        RespawnPlayer();
    }
}
