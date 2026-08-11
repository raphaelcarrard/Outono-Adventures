using UnityEngine;
using TMPro;

public class EnemySpawnerLevel7 : MonoBehaviour
{
    public PlayerController playerHealth;
    public GameObject goalTrigger, goalMessageText, enemyMessageText;

    [Header("Enemy Prefabs")]
    public GameObject[] enemyPrefabs;

    [Header("Spawn Points")]
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    [Header("Fishes")]
    public int fishNeeded = 40;
    public int fishCollected = 0;
    public TextMeshProUGUI fishText;
    public AudioSource audioSource;
    public AudioClip collectClip;

    public bool spawningEnabled = false;

    void Start()
    {
        if (spawningEnabled)
        {
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
        UpdateUI();
    }

    void Update()
    {
        if (AllFishesCollected())
        {
            spawningEnabled = false;
            enemyMessageText.SetActive(true);
            CancelInvoke(nameof(Spawn));
        }
        if (EnemyManager.instance.enemyCount <= 0 && !IsSpawningEnabled())
        {
            enemyMessageText.SetActive(false);
            goalMessageText.SetActive(true);
            goalTrigger.SetActive(true);
        }
    }

    public void CollectFish()
    {
        fishCollected++;
        audioSource.PlayOneShot(collectClip);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (fishText != null)
        {
            fishText.text = $"Fishes: {fishCollected}/{fishNeeded}";
        }
    }

    public int GetFishesCollected()
    {
        return fishCollected;
    }

    public bool AllFishesCollected()
    {
        return fishCollected >= fishNeeded;
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0)
        {
            return;
        }
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        #if UNITY_WEBGL || UNITY_ANDROID
        if (EnemyManager.instance.enemyCount <= 9)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        #else
        if (EnemyManager.instance.enemyCount <= 22)
        {
            GameObject enemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        }
        #endif
    }

    public bool IsSpawningEnabled()
    {
        return spawningEnabled;
    }
}
