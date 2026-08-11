using UnityEngine;
using TMPro;

public class EnemySpawnerLevel6 : MonoBehaviour
{

    public PlayerControllerLevel6 playerHealth;
    public GameObject enemy, keyGoalPrefab;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;
    public Transform keyPoint;

    [Header("Timer")]
    public float timeRemaining = 120f;
    public TextMeshProUGUI timerText;

    [Header("Objective")]
    public TextMeshProUGUI objectiveText;

    public bool spawningEnabled = true;
    public bool canSpawnKey = true;

    void Start()
    {
        if (spawningEnabled)
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
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
                CancelInvoke(nameof(Spawn));
                objectiveText.text = "Defeat all the enemies";
            }
        }
        if (EnemyManager.instance.enemyCount <= 0 && !IsSpawningEnabled() && canSpawnKey)
        {
            Instantiate(keyGoalPrefab, keyPoint.position, keyPoint.rotation);
            canSpawnKey = false;
            objectiveText.text = "Grab the key near the orange pawn to beat the level.";
        }
    }


    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        #if UNITY_WEBGL || UNITY_ANDROID
        if(EnemyManager.instance.enemyCount <= 9)
        {
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        #else
        if(EnemyManager.instance.enemyCount <= 22)
        {
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        #endif
    }

    public bool IsSpawningEnabled()
    {
        return spawningEnabled;
    }
}
