using UnityEngine;

public class EnemyBallHurt : MonoBehaviour
{

    EnemyController enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            enemy.TakeDamage();
        }
    }
}
