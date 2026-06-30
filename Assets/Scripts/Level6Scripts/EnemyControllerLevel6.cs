using UnityEngine;

public class EnemyControllerLevel6 : MonoBehaviour
{
    [Header("Attack")]
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 1;

    [Header("Health")]
    public int startingHealth = 10;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public AudioClip deathClip;

    [Header("Movement")]
    Transform playerTransform;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;
    GameObject player;
    PlayerControllerLevel6 playerHealth;
    bool playerInRange;
    float timer;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerControllerLevel6>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        enemyAudio = GetComponent<AudioSource>();
        hitParticles = GetComponentInChildren<ParticleSystem>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        currentHealth = startingHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    void Start()
    {
        EnemyManager.instance.RegisterEnemy();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && currentHealth > 0)
        {
            Attack();
        }
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
        if (isSinking)
        {
            transform.Translate(-Vector3.up * sinkSpeed * Time.deltaTime);
        }
        if (currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            nav.SetDestination(playerTransform.position);
        }
        else
        {
            nav.enabled = false;
        }
    }

    void Attack()
    {
        timer = 0f;
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead)
        {
            return;
        }
        enemyAudio.Play();
        currentHealth -= amount;
        hitParticles.transform.position = hitPoint;
        hitParticles.Play();
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        capsuleCollider.isTrigger = true;
        anim.SetTrigger("Dead");
        enemyAudio.clip = deathClip;
        enemyAudio.Play();
        EnemyManager.instance.EnemyDied();
        Destroy(gameObject, 2f);
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
