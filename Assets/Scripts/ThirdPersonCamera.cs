using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public Transform target;

    [Header("Distance")]
    public float distance = 6f;
    public float minDistance = 2f;
    public float maxDistance = 12f;

    [Header("Control")]
    public float sensitivity = 3f;
    public float zoomSpeed = 5f;

    float rotX;
    float rotY;

    void LateUpdate()
    {
        rotX += Input.GetAxis("Mouse X") * sensitivity;
        rotY -= Input.GetAxis("Mouse Y") * sensitivity;
        rotY = Mathf.Clamp(rotY, -30f, 60f);
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        Quaternion rotation = Quaternion.Euler(rotY, rotX, 0);
        Vector3 position = target.position - rotation * Vector3.forward * distance;
        transform.position = position;
        transform.LookAt(target);
    }
}
