using UnityEngine;

public class BallController : CarryableBall
{
    private Rigidbody rb;
    private float pickupCooldown;
    private bool isBeingCarried;

    public override bool CanBePickedUp => canBePickedUp;
    public override bool IsBeingCarried => isBeingCarried;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(pickupCooldown > 0)
        {
            pickupCooldown -= Time.deltaTime;
        }
    }

    public bool canBePickedUp => pickupCooldown <= 0f;

    public override void PickUp(Transform holdPoint)
    {
        isBeingCarried = true;
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public override void Kick(float force)
    {
        isBeingCarried = false;
        pickupCooldown = 0.3f;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }

    public override void Drop()
    {
        throw new System.NotImplementedException();
    }
}
