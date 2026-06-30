using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public PrisonDoor prisonDoor;

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            prisonDoor.OpenDoor();
            Destroy(gameObject);
        }
    }
}
