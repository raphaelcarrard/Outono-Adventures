using System.Collections;
using UnityEngine;

public class CountdownManager : MonoBehaviour
{

    public static CountdownManager instance;

    [Header("Config")]
    public float countdownTimer = 3f;
    public AudioSource audioSource;
    public AudioClip longWhistle;

    public bool CanMove { get; private set; }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CanMove = false;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        float timer = countdownTimer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }
        CanMove = true;
        audioSource.PlayOneShot(longWhistle);
        EnemySpawner.instance.spawningEnabled = true;
    }
}
