using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StorySequence : MonoBehaviour
{

    [Header("Intro")]
    public TextMeshProUGUI introText;
    public float introDuration = 8f;

    [Header("Slides")]
    public Image slideImage;
    public TextMeshProUGUI slideText;
    public Sprite[] images;
    [TextArea] public string[] texts;
    public float slideDuration = 10f;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    [Header("Scene")]
    public string nextScene;
    public SceneFader fader;
    public GameObject skipButton;

    bool skipping = false;

    void Start()
    {
        StartCoroutine(PlaySequence());
    }

    public void SkipScene()
    {
        skipping = true;
        skipButton.SetActive(false);
    }

    IEnumerator PlaySequence()
    {
        yield return StartCoroutine(FadeIn());
        introText.gameObject.SetActive(true);
        slideImage.gameObject.SetActive(false);
        slideText.gameObject.SetActive(false);
        float timer = 0;
        while (timer < introDuration && !skipping)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        introText.gameObject.SetActive(false);
        for (int i = 0; i < images.Length; i++)
        {
            if (skipping) break;
            slideImage.sprite = images[i];
            slideText.text = texts[i];
            slideImage.gameObject.SetActive(true);
            slideText.gameObject.SetActive(true);
            yield return StartCoroutine(FadeIn());
            timer = 0;
            while (timer < slideDuration && !skipping)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            yield return StartCoroutine(FadeOut());
        }
        fader.FadeToScene(nextScene);
    }

    IEnumerator FadeIn()
    {
        Color c = fadeImage.color;
        while (c.a > 0)
        {
            c.a -= Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        Color c = fadeImage.color;
        while (c.a < 1)
        {
            c.a += Time.deltaTime * fadeSpeed;
            fadeImage.color = c;
            yield return null;
        }
    }
}
