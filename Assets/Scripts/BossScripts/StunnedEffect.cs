using UnityEngine;

public class StunnedEffect : MonoBehaviour
{

    public Transform target;
    public float rotationSpeed = 120f;
    public float height = 2.5f;

    void LateUpdate()
    {
        if (target == null)
        {
            return;
        }
        transform.position = target.position + Vector3.up * height;
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
