using UnityEngine;
using TMPro;

public class LevelGoal : MonoBehaviour
{
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;

    void OnTriggerEnter(Collider other)
    {
        messageText.text = "Level Complete!";
        thirdPersonScript.enabled = false;
        playerController.enabled = false;
        LevelProgressManager.UnlockNextLevel(3);
        fader.FadeToScene(sceneName);
    }
}
