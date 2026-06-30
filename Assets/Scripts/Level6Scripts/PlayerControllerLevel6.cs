using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControllerLevel6 : MonoBehaviour
{

    public static PlayerControllerLevel6 instance;

    public CharacterController controller;
    public Animator anim;
    public AudioSource audioSource;
    public Transform cameraTransform;
    private LayerMask floorMask;

    PlayerControlsManager controls;
    Vector2 moveInput;
    bool shootPressed;

    [Header("Movement")]
    public float moveSpeed = 6f;
    public float gravity = -20f;
    public float jumpForce = 8f;
    public float rotationSpeed = 10f;

    [Header("Gun")]
    public int damagePerShot = 1;
    public float fireRate = .15f;
    public float range = 100f;
    public ParticleSystem gunParticles;
    public LineRenderer gunLine;
    public AudioSource gunAudio;
    public Light gunLight;
    public Light faceLight;
    private float shootTimer;

    [Header("Life")]
    public int maxHealth = 5;
    public int currentHealth;

    [Header("Sound Effects")]
    public AudioClip damageSound;
    public AudioClip deathSound;

    private Vector3 velocity;
    private bool isGrounded;
    private Vector2 mousePosition;
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
        controls.Player.JumpDoubleJump.performed += ctx => shootPressed = true;
        controls.Player.JumpDoubleJump.canceled += ctx => shootPressed = false;
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
        if (!isDead)
        {
            Move();
            HandleShoot();
            RotateTowardsMouse();
        }
        ApplyGravity();
    }

    void Move()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
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
            controller.Move(moveDir * moveSpeed * Time.deltaTime);
        }
        anim.SetFloat("Speed", move.magnitude);
    }

    private void HandleShoot()
    {
        shootTimer += Time.deltaTime;
        if(shootPressed && shootTimer >= fireRate && !isDead && Time.timeScale > 0)
        {
            Shoot();
        }
        if (shootTimer >= fireRate * 0.2f)
        {
            DisableGunEffects();
        }
    }

    void Shoot()
    {
        shootTimer = 0f;
        gunAudio.Play();
        gunLight.enabled = false;
        if (faceLight != null)
        {
            faceLight.enabled = false;
        }
        gunParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        gunParticles.Play();
        gunLine.enabled = true;
        gunLine.SetPosition(0, gunLine.transform.position);
        Ray ray = new Ray(gunLine.transform.position, gunLine.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            EnemyControllerLevel6 enemy = hit.collider.GetComponent<EnemyControllerLevel6>();
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerShot, hit.point);
            }
            gunLine.SetPosition(1, hit.point);
        }
        else
        {
            gunLine.SetPosition(1, ray.origin + ray.direction * range);
        }
    }

    void DisableGunEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
        if (faceLight != null)
        {
            faceLight.enabled = false;
        }
    }

    void RotateTowardsMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, floorMask))
        {
            Vector3 lookDirection = hit.point - transform.position;
            lookDirection.y = 0f;
            if (lookDirection.sqrMagnitude > .01f)
            {
                transform.rotation = Quaternion.LookRotation(lookDirection);
            }
        }
    }

    void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        anim.SetBool("isGrounded", isGrounded);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
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

    public int GetHealth()
    {
        return currentHealth;
    }

    public IEnumerator RestartStage()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
