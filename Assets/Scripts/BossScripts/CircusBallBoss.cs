using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CircusBallBoss : MonoBehaviour
{

    [Header("Movement")]
    public float speed = 10f;

    Rigidbody rb;
    bool reflected = false;
    BossController boss;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Initialize(Vector3 direction, BossController owner, float speed)
    {
        boss = owner;
        this.speed = speed;
        reflected = false;
        rb.linearVelocity = direction.normalized * speed;
    }

    public void Reflect()
    {
        if (reflected)
        {
            return;
        }
        reflected = true;
        Vector3 direction = (boss.transform.position - transform.position).normalized;
        rb.linearVelocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!reflected)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                if (player != null)
                {
                    Reflect();
                    player.Bounce(8f);
                    player.TakeDamage(1);
                    return;
                }
                Destroy(gameObject);
            }
        }
        else
        {
            BossController hitBoss = collision.gameObject.GetComponent<BossController>();
            if (hitBoss != null)
            {
                hitBoss.Stun();
                Destroy(gameObject);
            }
        }
    }
}
