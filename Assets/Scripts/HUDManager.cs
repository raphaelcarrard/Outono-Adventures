using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{

    public PlayerController player;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI enemyText;
    
    void Update()
    {
        livesText.text = "x " + player.GetHealth();
        enemyText.text = "Enemies: " + EnemyManager.instance.enemyCount;
    }
}
