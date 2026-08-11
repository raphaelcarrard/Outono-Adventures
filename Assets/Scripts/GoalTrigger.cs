using TMPro;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{

    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;
    public AudioSource audioSource;
    public AudioClip winSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
             messageText.text = "Level Complete!";
             //StartCoroutine(NGIO.UnlockMedal(91502, OnMedalUnlocked));
             SteamAchievements.instance.UnlockAchievement("level5");
             thirdPersonScript.enabled = false;
             playerController.enabled = false;
             audioSource.PlayOneShot(winSound);
             LevelProgressManager.UnlockNextLevel(5);
             fader.FadeToScene(sceneName);
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 5 Unlocked!");
     }*/
}
