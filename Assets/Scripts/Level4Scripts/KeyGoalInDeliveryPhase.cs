using UnityEngine;
using TMPro;

public class KeyGoalInDeliveryPhase : MonoBehaviour
{
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, carController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;
    public GameObject virtualControls;

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DeliveryManager.instance.money >= 250)
            {
                virtualControls.SetActive(false);
                messageText.text = "Level Complete!";
                StartCoroutine(NGIO.UnlockMedal(91501, OnMedalUnlocked));
                //SteamAchievements.instance.UnlockAchievement("level4");
                #if UNITY_ANDROID
                GooglePlayGamesManager.instance.CheckAchievements();
                #endif
                thirdPersonScript.enabled = false;
                carController.enabled = false;
                levelMusicSource.Stop();
                audioSource.PlayOneShot(winSound);
                LevelProgressManager.UnlockNextLevel(4);
                fader.FadeToScene(sceneName);
            }
            else
            {
                messageText.text = "You need to make $250 in money to be able to advance to the next level";
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

     public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 4 Unlocked!");
     }
}
