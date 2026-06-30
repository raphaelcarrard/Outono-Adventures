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

    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (DeliveryManager.instance.money >= 1000)
            {
                messageText.text = "Level Complete!";
                thirdPersonScript.enabled = false;
                carController.enabled = false;
                levelMusicSource.Stop();
                audioSource.PlayOneShot(winSound);
                LevelProgressManager.UnlockNextLevel(4);
                fader.FadeToScene(sceneName);
            }
            else
            {
                messageText.text = "You need to make $1000 in money to be able to advance to the next level";
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
}
