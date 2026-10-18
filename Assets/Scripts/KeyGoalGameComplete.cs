using TMPro;
using UnityEngine;

public class KeyGoalGameComplete : MonoBehaviour
{

    public string sceneName;
    public SceneFader fader;
    public MonoBehaviour playerController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;
    public GameObject virtualControls;

    void Start()
    {
        fader = GameObject.Find("FadePanel").GetComponent<SceneFader>();
        playerController = GameObject.Find("Player").GetComponent<MonoBehaviour>();
        levelMusicSource = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
        virtualControls = GameObject.Find("MobileControls");
    }    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                virtualControls.SetActive(false);
            }
            StartCoroutine(NGIO.UnlockMedal(91506, OnMedalUnlocked));
            //SteamAchievements.instance.UnlockAchievement("level9");
            #if UNITY_ANDROID
            GooglePlayGamesManager.instance.CheckAchievements();
            #endif
            playerController.enabled = false;
            levelMusicSource.Stop();
            audioSource.PlayOneShot(winSound);
            fader.FadeToScene(sceneName);
        }
    }

     public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 9 Unlocked!");
     }
}
