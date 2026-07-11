using TMPro;
using UnityEngine;

public class KeyGoalGameComplete : MonoBehaviour
{

    public string sceneName;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;
    public AudioSource audioSource, levelMusicSource;
    public AudioClip winSound;
    
    void Start()
    {
        fader = GameObject.Find("FadePanel").GetComponent<SceneFader>();
        playerController = GameObject.Find("Player").GetComponent<MonoBehaviour>();
        thirdPersonScript = GameObject.Find("Main Camera").GetComponent<MonoBehaviour>();
        levelMusicSource = GameObject.Find("LevelMusic").GetComponent<AudioSource>();
    }    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            thirdPersonScript.enabled = false;
            playerController.enabled = false;
            levelMusicSource.Stop();
            audioSource.PlayOneShot(winSound);
            fader.FadeToScene(sceneName);
        }
    }
}
