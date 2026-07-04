using UnityEngine;
using TMPro;

public class HUDManagerLevel8 : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI livesText;
    
    void Update()
    {
        livesText.text = "x " + player.GetHealth();
    }
}
