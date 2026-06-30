using UnityEngine;

public class EnemyHead : MonoBehaviour
{

    EnemyController enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemy.TakeDamage();
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Bounce(3f);
            }
        }
    }
}
