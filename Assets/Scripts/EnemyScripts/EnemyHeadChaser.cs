using UnityEngine;

public class EnemyHeadChaser : MonoBehaviour
{

    EnemyChaser enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyChaser>();
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
