using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{

    public static BossController instance;

    [Header("Player")]
    public Transform player;

    [Header("Boss Stats")]
    public int maxLives = 10;
    public int currentLives;

    [Header("Movement")]
    public float moveSpeed = 3f;
    public float stopDistance = 8f;
    public float rotationSpeed = 5f;

    [Header("Attack")]
    public GameObject bigCircusBallPrefab;
    public GameObject smallCircusBallPrefab;

    [Range(0f, 1f)]
    public float chanceSmallBall = 0.4f;

    public Transform throwPoint;
    public float attackCooldown = 3f;

    [Header("Stun")]
    public float stunTime = 5f;

    [Header("Components")]
    public Animator anim;

    [Header("Effects")]
    public GameObject stunnedEffect;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip stunnedSound;

    [Header("Ball Speed")]
    public float speedPhase1 = 12f;
    public float speedPhase2 = 16f;
    public float speedPhase3 = 22f;

    [Header("Ragdoll")]
    public GameObject ragdoll;
    public Transform ragdollPoint;

    bool isDead = false;
    bool isStunned = false;
    bool canAttack = true;
    bool isThrowing = false;

    public bool fightStarted = false;
    void Awake()
    {
        instance = this;   
    }
    void Start()
    {
        currentLives = maxLives;
        stunnedEffect.SetActive(false);
    }


    void Update()
    {
        if (!fightStarted)
        {
            return;
        }
        if (isDead)
        {
            return;
        }
        if (isStunned)
        {
            return;
        }
        if (isThrowing)
        {
            return;
        }
        LookAtPlayer();
        Move();
        if (canAttack)
        {
            StartCoroutine(ThrowRoutine());
        }
    }

    void LookAtPlayer()
    {
        Vector3 lookPos = player.position - transform.position;
        lookPos.y = 0;
        if (lookPos.sqrMagnitude < 0.01f)
        {
            return;
        }
        Quaternion targetRotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void Move()
    {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > stopDistance)
        {
            anim.SetBool("isWalking", true);
            transform.position += transform.forward * moveSpeed * Time.deltaTime;
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    IEnumerator ThrowRoutine()
    {
        canAttack = false;
        isThrowing = true;
        LookAtPlayer();
        anim.SetTrigger("throw");
        yield return new WaitForSeconds(2.13f);
        isThrowing = false;
        yield return new WaitForSeconds(attackCooldown - 2.13f);
        canAttack = true;
    }

    void ThrowBall()
    {
        GameObject ball;
        float chance = chanceSmallBall;
        if (currentLives <= 10 && currentLives >= 7)
        {
            chance = 0.1f;
            Debug.Log("Low Chance");
        }
        else if (currentLives <= 6 && currentLives >= 3)
        {
            chance = 0.3f;
            Debug.Log("Medium Chance");
        }
        else if(currentLives <= 2)
        {
            chance = 0.5f;
            Debug.Log("High Chance");
        }
        if (Random.value <= chanceSmallBall)
        {
            ball = Instantiate(smallCircusBallPrefab, throwPoint.position, throwPoint.rotation);
            CircusBallSmall smallBall = ball.GetComponent<CircusBallSmall>();
            if (smallBall != null)
            {
                Vector3 direction = (player.position - throwPoint.position).normalized;
                float ballSpeed = GetCurrentBallSpeed();
                smallBall.Initialize(direction, this, ballSpeed);
            }
        }
        else
        {
            ball = Instantiate(bigCircusBallPrefab, throwPoint.position, throwPoint.rotation);
            CircusBallBoss bigBall = ball.GetComponent<CircusBallBoss>();
            if (bigBall != null)
            {
                Vector3 direction = (player.position - throwPoint.position).normalized;
                float ballSpeed = GetCurrentBallSpeed();
                bigBall.Initialize(direction, this, ballSpeed);
            }
        }
    }

    public void Stun()
    {
        if (isDead)
        {
            return;
        }
        StartCoroutine(StunRoutine());
    }

    IEnumerator StunRoutine()
    {
        isStunned = true;
        anim.SetBool("stunned", true);
        stunnedEffect.SetActive(true);
        audioSource.PlayOneShot(stunnedSound);
        yield return new WaitForSeconds(stunTime);
        stunnedEffect.SetActive(false);
        anim.SetBool("stunned", false);
        isStunned = false;
    }

    public void TakeDamage()
    {
        if (!isStunned)
        {
            return;
        }
        currentLives--;
        anim.SetTrigger("hit");
        if (currentLives <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        Instantiate(ragdoll, ragdollPoint.position, ragdollPoint.rotation);
        stunnedEffect.SetActive(false);
        anim.SetTrigger("die");
        GetComponent<Collider>().enabled = false;
        enabled = false;
        Destroy(gameObject, 5f);
    }

    public bool IsStunned()
    {
        return isStunned;
    }

    public int GetCurrentLifes()
    {
        return currentLives;
    }

    private float GetCurrentBallSpeed()
    {
        if (currentLives == maxLives)
        {
            return speedPhase1;
        }
        if (currentLives == maxLives - 5)
        {
            return speedPhase2;
        }
        return speedPhase3;
    }
}
