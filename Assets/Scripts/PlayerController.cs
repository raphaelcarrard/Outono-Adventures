using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public CharacterController controller;
    public Animator anim;
    public AudioSource audioSource;
    public Transform cameraTransform;

    PlayerControlsManager controls;
    Vector2 moveInput;
    bool jumpPressed;

    [Header("Movement")]
    public float speed = 6f;
    public float gravity = -20f;
    public float jumpForce = 8f;
    public float rotationSpeed = 10f;

    [Header("Jump")]
    public int maxJumps = 2;
    private int jumpCount;

    [Header("Life")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Ball Settings")]
    public Transform holdPoint;
    public float kickForce = 15f;
    public CarryableBall currentBall;

    [Header("Sound Effects")]
    public AudioClip jumpSound;
    public AudioClip doubleJumpSound;
    public AudioClip walkSound;
    public AudioClip damageSound;
    public AudioClip deathSound;

    private Vector3 velocity;
    private bool isGrounded;
    public bool isDead;

    void Awake()
    {
        controls = new PlayerControlsManager();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            controls.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
        controls.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.Player.JumpDoubleJump.performed += ctx => jumpPressed = true;
        controls.Player.Kick.performed += ctx => KickBall();
        instance = this;
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }
    void Start()
    {
        currentHealth = maxHealth;
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
            if (!isDead)
            {
                Move();
                Jump();
            }
            ApplyGravity();
        }
        else
        {
            if (!isDead)
            {
                Move();
                Jump();
            }
            ApplyGravity();
        }
    }

    void Move()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            jumpCount = 0;
        }
        float h = moveInput.x;
        float v = moveInput.y;
        Vector3 move = cameraTransform.forward * v + cameraTransform.right * h;
	    move.y = 0;
        if (move.magnitude > 0.1f)
        {
            Vector3 moveDir = move.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            controller.Move(moveDir * speed * Time.deltaTime);
            if (isGrounded && move.magnitude > 0.1f)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(walkSound);
                }
            }
        }
        anim.SetFloat("Speed", move.magnitude);
    }

    void Jump()
    {
        if (jumpPressed && jumpCount < maxJumps)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
            jumpCount++;
            if (jumpCount == 1)
            {
                anim.SetTrigger("Jump");
                audioSource.PlayOneShot(jumpSound);
            }
            else if (jumpCount == 2)
            {
                anim.SetTrigger("DoubleJump");
                audioSource.PlayOneShot(doubleJumpSound);
            }
            jumpPressed = false;
        }
    }

    void KickBall()
    {
        if (currentBall == null)
        {
	        return;
        }
        anim.SetTrigger("Kick");
        currentBall.transform.forward = transform.forward;
        currentBall.Kick(kickForce);
        currentBall = null;
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        anim.SetBool("IsGrounded", isGrounded);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        audioSource.PlayOneShot(damageSound);
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        isDead = true;
        anim.SetTrigger("Die");
        audioSource.PlayOneShot(deathSound);
        controls.Disable();
        velocity.y = -5f;
        StartCoroutine(RestartStage());
    }

    public void Bounce(float bounceForce)
    {
        velocity.y = Mathf.Sqrt(bounceForce * -2f * gravity);
        anim.SetTrigger("Jump");
        audioSource.PlayOneShot(jumpSound);
        jumpCount = 0;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public IEnumerator RestartStage()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currentBall != null)
        {
	       return;
        }
        CarryableBall ball = other.GetComponent<CarryableBall>();
        if (ball == null)
        {
           return;
        }
        if (ball.IsBeingCarried)
        {
           return;
        }
        if (!ball.CanBePickedUp)
        {
           return;
        }
        currentBall = ball;
        ball.PickUp(holdPoint);
    }

    public bool IsFalling()
    {
        return velocity.y < 0f;
    }
}
