using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    private float pickupCooldown;

    public bool isBeingCarried { get; private set; }

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

    public bool CanBePickedUp => pickupCooldown <= 0f;

    public void PickUp(Transform holdPoint)
    {
        isBeingCarried = true;
        rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(holdPoint);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }

    public void Kick(float force)
    {
        isBeingCarried = false;
        pickupCooldown = 0.3f;
        transform.SetParent(null);
        rb.isKinematic = false;
        rb.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
