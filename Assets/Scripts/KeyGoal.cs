using TMPro;
using UnityEngine;

public class KeyGoal : MonoBehaviour
{

    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (EnemyManager.instance.enemyCount == 0)
            {
                messageText.text = "Level Complete!";
                //StartCoroutine(NGIO.UnlockMedal(91496, OnMedalUnlocked));
                SteamAchievements.instance.UnlockAchievement("level1");
                thirdPersonScript.enabled = false;
                playerController.enabled = false;
                levelMusicSource.Stop();
                audioSource.PlayOneShot(winSound);
                LevelProgressManager.UnlockNextLevel(1);
                fader.FadeToScene(sceneName);
            }
            else
            {
                messageText.text = "You need to defeat all the enemies first to be able to advance to the next level";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "";
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 1 Unlocked!");
     }*/
}
