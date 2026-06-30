using TMPro;
using UnityEngine;

public class HUDCanvasLevel6 : MonoBehaviour
{
    public PlayerControllerLevel6 player;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI enemyText;

    void Update()
    {
        livesText.text = "x " + player.GetHealth();
        enemyText.text = "Enemies: " + EnemyManager.instance.enemyCount;
    }
}
