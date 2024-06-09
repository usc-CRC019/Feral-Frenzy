using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    public GameObject playerPistol;
    public Transform bulletProjectile;
    public GameObject pistolBarrel;
    private Vector3 mouseWorldPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
        {
            mouseWorldPosition = raycastHit.point;
        }


        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 aimDir = (mouseWorldPosition - pistolBarrel.transform.position).normalized;
            Instantiate(bulletProjectile, pistolBarrel.transform.position, Quaternion.LookRotation(aimDir, Vector3.up));
        }



        
    }
}
