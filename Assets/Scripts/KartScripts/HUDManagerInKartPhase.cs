using UnityEngine;
using TMPro;

public class HUDManagerInKartPhase : MonoBehaviour
{
    public KartController kart;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI enemyText;
    
    void Update()
    {
        livesText.text = "x " + kart.GetHealth();
        enemyText.text = "Enemies: " + EnemyManager.instance.enemyCount;
    }
}
