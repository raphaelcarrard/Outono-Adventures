using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
public class KartController : MonoBehaviour
{

    public static KartController instance;

    [Header("Wheel Colliders")]
    public WheelCollider frontLeftCollider;
    public WheelCollider frontRightCollider;
    public WheelCollider rearLeftCollider;
    public WheelCollider rearRightCollider;

    [Header("Wheel Meshes")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    [Header("Car Settings")]
    public float motorTorque = 1500f;
    public float brakeForce = 3000f;
    public float maxSteerAngle = 30f;
    public float maxSpeed = 120f;

    [Header("Life")]
    public int maxHealth = 5;
    private int currentHealth;

    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip deathSound;
    
    [Header("Ball")]
    public GameObject ballPrefab;
    public Transform pointSpawn;
    public float force = 15f;
    public float interval = 1f;
    private float nextThrow = 0f;

    private Rigidbody rb;
    private PlayerControlsManager controls;
    private float moveInput;
    private bool isBraking;
    private bool throwBall;
    private float steerInput;
    public bool isDead;

    void Awake()
    {
        instance = this;
        controls = new PlayerControlsManager();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            controls.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
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
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -0.5f, 0);
	currentHealth = maxHealth;
    }

    void Update()
    {
        Vector2 input = controls.Player.Move.ReadValue<Vector2>();
        steerInput = input.x;
        moveInput = input.y;
        isBraking = controls.Player.JumpDoubleJump.IsPressed();
        throwBall = controls.Player.Kick.IsPressed();
    }


    void FixedUpdate()
    {
	if (!isDead)
        {
           ThrowingBall();
           HandleMotor();
           HandleSteering();
           HandleBraking();
           UpdateWheels();
           LimitSpeed();
        }
    }

    void ThrowingBall()
    {
        if (throwBall && Time.time >= nextThrow)
        {
            ThrowBall();
            nextThrow = Time.time + interval;
            return;
        }
    }

    void ThrowBall(){
        GameObject ball = Instantiate(ballPrefab, pointSpawn.position, pointSpawn.rotation);
	    Rigidbody rb = ball.GetComponent<Rigidbody>();
	    rb.AddForce(pointSpawn.forward * force, ForceMode.Impulse);
   }

    void HandleMotor()
    {
        rearLeftCollider.motorTorque = moveInput * motorTorque;
        rearRightCollider.motorTorque = moveInput * motorTorque;
    }

    void HandleSteering()
    {
        float steeringAngle = maxSteerAngle * steerInput;
        frontLeftCollider.steerAngle = steeringAngle;
        frontRightCollider.steerAngle = steeringAngle;
    }

    void HandleBraking()
    {
        float currentBrakeForce = isBraking ? brakeForce : 0f;
        frontLeftCollider.brakeTorque = currentBrakeForce;
        frontRightCollider.brakeTorque = currentBrakeForce;
        rearLeftCollider.brakeTorque = currentBrakeForce;
        rearRightCollider.brakeTorque = currentBrakeForce;
    }

    void LimitSpeed()
    {
        float speed = rb.linearVelocity.magnitude * 3.6f;
        if (speed > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * (maxSpeed / 3.6f);
        }
    }

    void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftCollider, frontLeftWheel);
        UpdateSingleWheel(frontRightCollider, frontRightWheel);
        UpdateSingleWheel(rearLeftCollider, rearLeftWheel);
        UpdateSingleWheel(rearRightCollider, rearRightWheel);
    }

    void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 position;
        Quaternion rotation;
        wheelCollider.GetWorldPose(out position, out rotation);
        //wheelTransform.position = position;
        wheelTransform.rotation = rotation;
    }

    public int GetHealth()
    {
        return currentHealth;
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
        audioSource.PlayOneShot(deathSound);
        controls.Disable();
        StartCoroutine(RestartStage());
    }

    public IEnumerator RestartStage()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
