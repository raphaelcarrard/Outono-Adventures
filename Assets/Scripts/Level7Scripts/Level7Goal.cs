using UnityEngine;
using TMPro;

public class Level7Goal : MonoBehaviour
{
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           messageText.text = "Level Complete!";
           thirdPersonScript.enabled = false;
           playerController.enabled = false;
           levelMusicSource.Stop();
           audioSource.PlayOneShot(winSound);
           LevelProgressManager.UnlockNextLevel(7);
           fader.FadeToScene(sceneName);
        }
    }
}
