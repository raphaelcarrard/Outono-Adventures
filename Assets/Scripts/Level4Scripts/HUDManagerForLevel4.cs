using UnityEngine;
using TMPro;

public class HUDManagerForLevel4 : MonoBehaviour
{
    public CarController car;
    public TextMeshProUGUI livesText;
    
    void Update()
    {
        livesText.text = "x " + car.GetHealth();
    }
}
