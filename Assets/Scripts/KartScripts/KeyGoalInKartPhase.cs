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

    void Start()
    {
        messageText = GameObject.Find("MessageText").GetComponent<TextMeshProUGUI>();
        fader = GameObject.Find("FadePanel").GetComponent<SceneFader>();
        kartController = GameObject.Find("Kart").GetComponent<MonoBehaviour>();
        levelMusicSource = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
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
                messageText.text = "Level Complete!";
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
}
