using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDManagerForLevel4 : MonoBehaviour
{
    public CarController car;
    public TextMeshProUGUI livesText;
    public GameObject rawImageMinimapWithVB, rawImageMinimapWithoutVB;
    
    void Update()
    {
        livesText.text = "x " + car.GetHealth();
        if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
        {
            rawImageMinimapWithVB.SetActive(false);
            rawImageMinimapWithoutVB.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
        {
            rawImageMinimapWithVB.SetActive(true);
            rawImageMinimapWithoutVB.SetActive(false);
        }
    }
}
