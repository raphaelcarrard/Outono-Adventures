using UnityEngine;

public class FallResetInKartPhase : MonoBehaviour
{

    public Transform respawnPoint;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPoint.position;
        }
    }
}
