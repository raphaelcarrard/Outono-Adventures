using UnityEngine;
using TMPro;

public class HUDManagerInThisPhase : MonoBehaviour
{
    public PlayerController player;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI messageText;
    public GameObject creepyImage;
    public MonoBehaviour playerController, cameraController, pauseManager;

    void Update()
    {
        livesText.text = "x " + player.GetHealth();
        messageText.text = "Escape of the Ballerines!";
        if (player.isDead)
        {
            creepyImage.SetActive(true);
            playerController.enabled = false;
            cameraController.enabled = false;
            pauseManager.enabled = false;
        }
    }
}
