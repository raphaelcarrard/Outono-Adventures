using UnityEngine;
using TMPro;

public class HUDManagerBoss : MonoBehaviour
{

    public PlayerController player;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI bossText;
    
    void Update()
    {
        livesText.text = "x " + player.GetHealth();
        bossText.text = "Boss Lifes: " + BossController.instance.currentLives;
    }
}
