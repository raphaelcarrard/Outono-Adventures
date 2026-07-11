using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CircusBallSmall : CarryableBall
{

    [Header("Movement")]
    public float throwForce = 12f;

    [Header("Lifetime")]
    public float destroyAfter = 20f;

    private Rigidbody rb;
    private BossController boss;
    private bool isBeingCarried = false;
    private bool canBePickedUp = true;

    public override bool CanBePickedUp => canBePickedUp;
    public override bool IsBeingCarried => isBeingCarried;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

    public void Initialize(Vector3 direction, BossController owner, float speed)
    {
        boss = owner;
        this.throwForce = speed;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(direction.normalized * throwForce, ForceMode.Impulse);
    }

    public override void Kick(float force)
    {
        isBeingCarried = true;
        canBePickedUp = false;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
    }

    public override void PickUp(Transform holdPoint)
    {
        if (!canBePickedUp)
        {
            return;
        }
        rb.isKinematic = true;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void Drop()
    {
        transform.SetParent(null);
        rb.isKinematic = false;
        canBePickedUp = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isBeingCarried)
        {
            BossController hitBoss = collision.gameObject.GetComponent<BossController>();
            if (hitBoss != null)
            {
                hitBoss.Stun();
                isBeingCarried = false;
            }
        }
    }
}
