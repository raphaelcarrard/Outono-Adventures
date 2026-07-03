using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{

    public static EnemySpawner instance;

    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    [Header("Other Prefabs")]
    public GameObject ballPrefab;

    [Header("Spawn Points")]
    public Transform[] spawnPoints;
    public Transform ballPoint;

    [Header("Timer")]
    public float timeRemaining = 120f;
    public TextMeshProUGUI timerText;

    public bool spawningEnabled = false;
    public bool canSpawnBall = true;

    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        if (spawningEnabled)
        {
            timeRemaining -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
            if (timeRemaining <= 0)
            {
                spawningEnabled = false;
                timerText.text = "00:00";
            }
        }
        if (EnemyManager.instance.enemyCount <= 0 && !IsSpawningEnabled() && canSpawnBall)
        {
            Instantiate(ballPrefab, ballPoint.position, ballPoint.rotation);
            canSpawnBall = false;
        }
    }

    public void EnemyKilled()
    {
        if (!spawningEnabled)
        {
            return;
        }
        SpawnRandomEnemy();
    }

    void SpawnRandomEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            return;
        }
        if (enemyPrefabs.Length == 0)
        {
            return;
        }
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(prefab, point.position, point.rotation);
    }

    public bool IsSpawningEnabled()
    {
        return spawningEnabled;
    }
}
