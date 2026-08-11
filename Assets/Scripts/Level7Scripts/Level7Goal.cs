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
           //StartCoroutine(NGIO.UnlockMedal(91504, OnMedalUnlocked));
           SteamAchievements.instance.UnlockAchievement("level7");
           thirdPersonScript.enabled = false;
           playerController.enabled = false;
           levelMusicSource.Stop();
           audioSource.PlayOneShot(winSound);
           LevelProgressManager.UnlockNextLevel(7);
           fader.FadeToScene(sceneName);
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 7 Unlocked!");
     }*/
}
