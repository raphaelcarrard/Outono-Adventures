using TMPro;
using UnityEngine;

public class GoalTriggerLevel8 : MonoBehaviour
{
    public static GoalTriggerLevel8 instance;
    public bool winGame;
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;
    public AudioSource audioSource, levelMusic;
    public AudioClip winSound;

    void Awake()
    {
       instance = this;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
             TimerManager.instance.timeEnabled = false;
             winGame = true;
             messageText.text = "Level Complete!";
             //StartCoroutine(NGIO.UnlockMedal(91505, OnMedalUnlocked));
             SteamAchievements.instance.UnlockAchievement("level8");
             thirdPersonScript.enabled = false;
             playerController.enabled = false;
             levelMusic.Stop();
             audioSource.PlayOneShot(winSound);
             LevelProgressManager.UnlockNextLevel(8);
             fader.FadeToScene(sceneName);
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 8 Unlocked!");
     }*/
}
