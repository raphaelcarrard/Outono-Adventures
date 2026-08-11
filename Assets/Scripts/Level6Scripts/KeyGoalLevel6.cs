using TMPro;
using UnityEngine;

public class KeyGoalLevel6 : MonoBehaviour
{
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;

    void Start()
    {
        messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        fader = GameObject.Find("FadePanel").GetComponent<SceneFader>();
        playerController = GameObject.Find("Player").GetComponent<MonoBehaviour>();
        thirdPersonScript = GameObject.Find("Main Camera").GetComponent<MonoBehaviour>();
        levelMusicSource = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageText.text = "Level Complete!";
            //StartCoroutine(NGIO.UnlockMedal(91503, OnMedalUnlocked));
            SteamAchievements.instance.UnlockAchievement("level6");
            playerController.enabled = false;
            levelMusicSource.Stop();
            audioSource.PlayOneShot(winSound);
            LevelProgressManager.UnlockNextLevel(6);
            fader.FadeToScene(sceneName);
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 6 Unlocked!");
     }*/
}
