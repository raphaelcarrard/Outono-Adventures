using UnityEngine;

public class DestroyBall : MonoBehaviour
{

    void Update()
    {
       Destroy(gameObject, 20f);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyBalls"))
        {
            Destroy(gameObject);
        }
    }
}
