using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyChaser : MonoBehaviour
{

    [Header("Player")]
    public Transform player;

    [Header("Stats")]
    public int maxLives = 2;
    int currentLifes;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip hurtSound, deathSound;

    [Header("Movement")]
    public float speed = 3f;
    public float detectionRange = 10f;
    public float stopDistance = 1.5f;

    [Header("Components")]
    public Animator anim;

    bool isDead = false;

    void Start()
    {
        currentLifes = maxLives;
	    EnemyManager.instance.RegisterEnemy();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level5")
        {
            if (!CountdownManager.instance.CanMove)
            {
                return;
            }
            if (isDead)
            {
                return;
            }
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > detectionRange)
            {
                anim.SetBool("isChasing", false);
                return;
            }
            anim.SetBool("isChasing", true);
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0;
            if (lookPos != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
            if (distance > stopDistance)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
        else
        {
            if (isDead)
            {
                return;
            }
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > detectionRange)
            {
                anim.SetBool("isChasing", false);
                return;
            }
            anim.SetBool("isChasing", true);
            Vector3 lookPos = player.position - transform.position;
            lookPos.y = 0;
            if (lookPos != Vector3.zero)
            {
                Quaternion rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
            }
            if (distance > stopDistance)
            {
                transform.position += transform.forward * speed * Time.deltaTime;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player != null && !player.isDead)
            {
                player.TakeDamage(1);
            }
        }
    }

    public void TakeDamage()
    {
        if (isDead)
        {
            return;
        }
        audioSource.PlayOneShot(hurtSound);
        currentLifes--;
        anim.SetTrigger("hit");
        if (currentLifes <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        audioSource.PlayOneShot(deathSound);
        anim.SetBool("IsChasing", false);
        anim.SetTrigger("die");
        GetComponent<Collider>().enabled = false;
	    EnemyManager.instance.EnemyDied();
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Level5")
        {
            EnemySpawner.instance.EnemyKilled();
        }
        Destroy(gameObject, 2f);
    }

    public int GetCurrentLifes()
    {
        return currentLifes;
    }
}
