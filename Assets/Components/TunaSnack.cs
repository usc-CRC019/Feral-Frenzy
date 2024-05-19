using UnityEngine;

public class TunaSnack : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            player.GetComponent<Player>().GiveTunaSnack();
            Destroy(this.gameObject);
        }
    }
}
