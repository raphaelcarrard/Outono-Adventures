using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Rotation")]
    public float mouseSensitivity = 200f;
    public float minVerticalAngle = -25f;
    public float maxVerticalAngle = 60f;

    [Header("Distance")]
    public float distance = 6f;
    public float minDistance = 2f;
    public float maxDistance = 10f;
    public float zoomSpeed = 5f;

    [Header("Collision")]
    public LayerMask collisionMask;

    public float sphereRadius = 0.3f;
    public float collisionOffset = 0.2f;
    public float smoothSpeed = 10f;

    float yaw;
    float pitch;
    float currentDistance;

    void Start()
    {
        currentDistance = distance;
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void LateUpdate()
    {
        RotateCamera();
        Zoom();
        MoveCamera();
    }

    void RotateCamera()
    {
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);
    }

    void Zoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        currentDistance -= scroll * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
    }

    void MoveCamera()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 desiredPosition = target.position - rotation * Vector3.forward * currentDistance;
        Vector3 direction = desiredPosition - target.position;
        float targetDistance = currentDistance;
        if (Physics.SphereCast(target.position, sphereRadius, direction.normalized, out RaycastHit hit, currentDistance, collisionMask))
        {
            targetDistance = hit.distance - collisionOffset;
            targetDistance = Mathf.Max(targetDistance, minDistance);
        }
        Vector3 finalPosition = target.position - rotation * Vector3.forward * targetDistance;
        transform.position = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = rotation;
    }
}
