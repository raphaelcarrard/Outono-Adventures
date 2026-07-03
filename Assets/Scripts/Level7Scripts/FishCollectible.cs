using UnityEngine;

public class FishCollectible : MonoBehaviour
{
    public EnemySpawnerLevel7 phase;
    
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            phase.CollectFish();
            Destroy(gameObject);
        }
    }
}
