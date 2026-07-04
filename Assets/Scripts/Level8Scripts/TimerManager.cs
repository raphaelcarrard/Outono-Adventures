using UnityEngine;
using TMPro;

public class TimerManager : MonoBehaviour
{
    
    public static TimerManager instance;
    
    public PlayerController player;
    public bool playerDead;
    
    [Header("Timer")]
    public float timeRemaining = 120f;
    public TextMeshProUGUI timerText;

    public bool timeEnabled;

    void Awake()
    {
       instance = this;
    }

    void Update()
    {
        if(timeEnabled)
        {
           timeRemaining -= Time.deltaTime;
           int minutes = Mathf.FloorToInt(timeRemaining / 60);
           int seconds = Mathf.FloorToInt(timeRemaining % 60);
           timerText.text = $"Time: {minutes:00}:{seconds:00}";
           if (timeRemaining <= 0 && !GoalTriggerLevel8.instance.winGame && !playerDead)
           {
              timeEnabled = false;
              timerText.text = "Time: 00:00";
              player.TakeDamage(5);
              playerDead = true;
           }
        }
    }
}
