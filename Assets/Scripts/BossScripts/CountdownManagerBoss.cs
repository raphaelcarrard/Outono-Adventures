using System.Collections;
using UnityEngine;

public class CountdownManagerBoss : MonoBehaviour
{

    public static CountdownManagerBoss instance;

    [Header("Config")]
    public float countdownTimer = 3f;

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

    public IEnumerator StartCountdown()
    {
        float timer = countdownTimer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }
        CanMove = true;
    }
}
