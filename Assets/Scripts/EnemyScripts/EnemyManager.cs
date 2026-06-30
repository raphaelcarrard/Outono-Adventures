using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public int enemyCount;

    void Awake()
    {
        instance = this;
    }

    public void RegisterEnemy()
    {
        enemyCount++;
    }

    public void EnemyDied()
    {
        enemyCount--;
    }
}
