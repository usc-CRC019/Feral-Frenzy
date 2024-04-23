using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerLevel;
    public int reputation;

    public float walkMoveSpeed;
    public float sprintMoveSpeed;
    public float moveSpeed;


    public float jumpHeight;
    public float gravityValue;
    public float wallrunGravityValue;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //walkMoveSpeed = 4f;
        //sprintMoveSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
