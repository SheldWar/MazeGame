using UnityEngine;

public class FlagInteraction : MonoBehaviour
{
    private bool isPlayerInRange = false;
    private GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Q))
        {
            PerformAction();
        }
    }

    private void PerformAction()
    {
        player.GetComponent<FlagsController>().flagRemoved();
        Destroy(gameObject);
    }
}
