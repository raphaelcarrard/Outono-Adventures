using UnityEngine;

public class DestroyBall : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyBalls"))
        {
            Destroy(gameObject);
        }
    }
}
