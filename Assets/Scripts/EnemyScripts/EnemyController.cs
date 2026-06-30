using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [Header("Config")]
    public int maxLives = 1;
    int currentLives;
    public AudioSource audioSource;

    [Header("Components")]
    public Animator anim;

    [Header("Movement")]
    public float speed = 2f;
    public Transform[] patrolPoints;
    int currentPoint = 0;
    
    public AudioClip damageSound;
    public AudioClip deathSound;

    bool isDead = false;

    void Start()
    {
        currentLives = maxLives;
        EnemyManager.instance.RegisterEnemy();
    }

    
    void Update()
    {
        if (isDead)
        {
            return;
        }
        Patrol();
    }

    void Patrol()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }
        Transform target = patrolPoints[currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.LookAt(target);
        anim.SetBool("isWalking", true);
        if (Vector3.Distance(transform.position, target.position) < 0.2f)
        {
            currentPoint = (currentPoint + 1) % patrolPoints.Length;
        }
    }

    public void TakeDamage()
    {
        if (isDead)
        {
            return;
        }
        currentLives--;
        if (currentLives > 0)
        {
            anim.SetTrigger("hit");
            audioSource.PlayOneShot(damageSound);
        }
        else
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        anim.SetBool("isWalking", false);
        anim.SetTrigger("die");
        GetComponent<Collider>().enabled = false;
        audioSource.PlayOneShot(deathSound);
        EnemyManager.instance.EnemyDied();
        Destroy(gameObject, 2f);
    }

    public int GetCurrentLifes()
    {
        return currentLives;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                player.TakeDamage(1);
            }
        }
	if (collision.gameObject.CompareTag("Kart"))
        {
            KartController kart = collision.gameObject.GetComponent<KartController>();
            if (kart != null)
            {
                kart.TakeDamage(1);
            }
        }
    }
}
