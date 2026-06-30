using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class InitialScreen : MonoBehaviour
{

    public TMP_Text textPress;
    public float lowVelocity = 1f;
    public float highVelocity = 0.1f;
    public float timeBeforeLoad = 1.5f;
    public string sceneToLoad;
    public SceneFader fader;
    private bool gameStarted = false;
    
    void Start()
    {
        StartCoroutine(FlashingText(lowVelocity));
        fader = FindObjectOfType<SceneFader>();
    }

    
    void Update()
    {
        if(!gameStarted && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            gameStarted = true;
            StopAllCoroutines();
            StartCoroutine(StartGame());
        }
    }

    IEnumerator FlashingText(float velocity)
    {
        while (true)
        {
            textPress.enabled = !textPress.enabled;
            yield return new WaitForSeconds(velocity);
        }
    }

    IEnumerator StartGame()
    {
        StartCoroutine(FlashingText(highVelocity));
        yield return new WaitForSeconds(timeBeforeLoad);
        fader.FadeToScene(sceneToLoad);
    }
}
