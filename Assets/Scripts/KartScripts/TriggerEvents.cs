using UnityEngine;

public class TriggerEvents : MonoBehaviour
{

    [Header("Walls")]
    public GameObject wall1;
    public GameObject wall2;

    [Header("Key Prefab")]
    public GameObject KeyPrefab;

    [Header("Key Location")]
    public Transform pointSpawn;

    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (activated)
        {
            return;
        }
        if (other.CompareTag("Kart"))
        {
            activated = true;
            wall1.SetActive(false);
            Instantiate(KeyPrefab, pointSpawn.position, pointSpawn.rotation);
            wall2.SetActive(true);
        }
    }
}
