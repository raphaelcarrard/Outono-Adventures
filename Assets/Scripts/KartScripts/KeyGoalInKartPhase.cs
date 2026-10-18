using TMPro;
using UnityEngine;

public class KeyGoalInKartPhase : MonoBehaviour
{
    
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour kartController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;
    public GameObject virtualControls;

    void Start()
    {
        messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        fader = GameObject.Find("FadePanel").GetComponent<SceneFader>();
        kartController = GameObject.Find("Kart").GetComponent<MonoBehaviour>();
        levelMusicSource = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
        virtualControls = GameObject.Find("MobileControls");
    }

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Kart"))
        {
            if (EnemyManager.instance.enemyCount == 0)
            {
                if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
                {
                    virtualControls.SetActive(false);
                }
                messageText.text = "Level Complete!";
                StartCoroutine(NGIO.UnlockMedal(91497, OnMedalUnlocked));
                //SteamAchievements.instance.UnlockAchievement("level2");
                #if UNITY_ANDROID
                GooglePlayGamesManager.instance.CheckAchievements();
                #endif
                kartController.enabled = false;
                levelMusicSource.Stop();
                audioSource.PlayOneShot(winSound);
                LevelProgressManager.UnlockNextLevel(2);
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
        if (other.CompareTag("Kart"))
        {
            messageText.text = "";
        }
    }

     public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 2 Unlocked!");
     }
}
