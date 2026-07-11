using UnityEngine;

public class BossHead : MonoBehaviour
{

    BossController boss;

    void Start()
    {
        boss = GetComponentInParent<BossController>();
    }


    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (!boss.IsStunned())
        {
            return;
        }
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null)
        {
            return;
        }
        player.Bounce(10f);
        boss.TakeDamage();
    }
}
