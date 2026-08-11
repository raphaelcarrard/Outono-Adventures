using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class StorySequence : MonoBehaviour
{

    [Header("Intro")]
    public TextMeshProUGUI introText;
    public float introDuration = 8f;

    [Header("Slides")]
    public Image slideImage;
    public TextMeshProUGUI slideText;
    public Sprite[] images;

    [TextArea]
    public string[] texts;

    public float slideDuration = 10f;

    [Header("Fade")]
    public Image fadeImage;
    public float fadeSpeed = 2f;

    [Header("Scene")]
    public string nextScene;
    public SceneFader fader;

    [Header("Buttons")]
    public GameObject skipButton;
    public Button nextButton;
    public Button previousButton;

    [Header("Auto Play")]
    public Toggle autoPlayToggle;

    private bool autoPlay = true;
    private bool skipping = false;

    private int currentSlide = -1;
    private Coroutine sequenceCoroutine;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        if (autoPlayToggle != null)
        {
            autoPlay = autoPlayToggle.isOn;
            autoPlayToggle.onValueChanged.AddListener(SetAutoPlay);
        }
        introText.gameObject.SetActive(true);
        slideImage.gameObject.SetActive(false);
        slideText.gameObject.SetActive(false);
        UpdateButtons();
        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {
        yield return StartCoroutine(FadeIn());
        currentSlide = -1;
        introText.gameObject.SetActive(true);
        slideImage.gameObject.SetActive(false);
        slideText.gameObject.SetActive(false);
        UpdateButtons();
        if (autoPlay)
        {
            float timer = 0f;
            while(timer < introDuration && !skipping)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            if (skipping)
            {
                yield break;
            }
            NextSlide();
        }
    }

    public void SetAutoPlay(bool value)
    {
        autoPlay = value;
        if (!autoPlay)
        {
            if(sequenceCoroutine != null)
            {
                StopCoroutine(sequenceCoroutine);
                sequenceCoroutine = null;
            }
            return;
        }
        if (currentSlide == -1)
        {
            if (sequenceCoroutine != null)
            {
                StopCoroutine(sequenceCoroutine);
            }
            sequenceCoroutine = StartCoroutine(AutoPlayIntro());
        }
        else if(currentSlide >= 0 && currentSlide < images.Length)
        {
            if (sequenceCoroutine != null)
            {
                StopCoroutine(sequenceCoroutine);
            }
            sequenceCoroutine = StartCoroutine(AutoAdvance());
        }
    }

    private IEnumerator AutoPlayIntro()
    {
        float timer = 0f;
        while(timer < introDuration && !skipping)
        {
            if (!autoPlay)
            {
                timer = 0f;
                sequenceCoroutine = null;
                yield break;
            }
            else
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
        if (!skipping && autoPlay)
        {
            sequenceCoroutine = null;
            NextSlide();
        }
    }

    void ShowCurrentSlide()
    {
        if(currentSlide < 0)
        {
            introText.gameObject.SetActive(true);
            slideImage.gameObject.SetActive(false);
            slideText.gameObject.SetActive(false);
            UpdateButtons();
            return;
        }
        if (currentSlide >= images.Length)
        {
            FinishSequence();
            return;
        }
        introText.gameObject.SetActive(false);
        slideImage.sprite = images[currentSlide];
        if (currentSlide < texts.Length)
        {
            slideText.text = texts[currentSlide];
        }
        else
        {
            slideText.text = "";
        }
        slideImage.gameObject.SetActive(true);
        slideText.gameObject.SetActive(true);
        UpdateButtons();
        if (autoPlay)
        {
            if(sequenceCoroutine != null)
            {
                StopCoroutine(sequenceCoroutine);
            }
            sequenceCoroutine = StartCoroutine(AutoAdvance());
        }
    }

    private IEnumerator AutoAdvance()
    {
        float timer = 0f;
        while (timer < slideDuration && !skipping)
        {
            if (!autoPlay)
            {
                timer = 0f;
                sequenceCoroutine = null;
                yield break;
            }
            else
            {
                timer += Time.deltaTime;
                yield return null;
            }
        }
        if (!skipping && autoPlay)
        {
            sequenceCoroutine = null;
            NextSlide();
        }
    }

    public void NextSlide()
    {
        if (skipping)
        {
            return;
        }
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
        currentSlide++;
        if (currentSlide >= images.Length)
        {
            FinishSequence();
            return;
        }
        ShowCurrentSlide();
    }

    public void PreviousSlide()
    {
        if (skipping)
        {
            return;
        }
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
        if (currentSlide == 0)
        {
            currentSlide = -1;
            ShowCurrentSlide();
            return;
        }
        if (currentSlide > 0)
        {
            currentSlide--;
            ShowCurrentSlide();
        }
    }

    void UpdateButtons()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (nextButton != null)
        {
            if(scene.name == "InitialStory")
            {
                nextButton.gameObject.SetActive(currentSlide < 9);
            }
            else if(scene.name == "EndStory")
            {
                nextButton.gameObject.SetActive(currentSlide < 6);
            }
        }
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(currentSlide > -1);
        }
    }

    public void SkipScene()
    {
        if (skipping)
        {
            return;
        }
        skipping = true;
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
        DisableButtons();
        StartCoroutine(SkipRoutine());
    }

    private IEnumerator SkipRoutine()
    {
        yield return StartCoroutine(FadeOut());
        fader.FadeToScene(nextScene);
    }

    void FinishSequence()
    {
        if (skipping)
        {
            return;
        }
        skipping = true;
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
        DisableButtons();
        StartCoroutine(FinishRoutine());
    }

    IEnumerator FinishRoutine()
    {
        yield return StartCoroutine(FadeOut());
        fader.FadeToScene(nextScene);
    }

    private void DisableButtons()
    {
        if (skipButton != null)
        {
            skipButton.SetActive(false);
        }
        if (nextButton != null)
        {
            nextButton.gameObject.SetActive(false);
        }
        if (previousButton != null)
        {
            previousButton.gameObject.SetActive(false);
        }
        if (autoPlayToggle != null)
        {
            autoPlayToggle.gameObject.SetActive(false);
        }
    }

    IEnumerator FadeIn()
    {
        Color c = fadeImage.color;
        while (c.a > 0)
        {
            c.a -= Time.deltaTime * fadeSpeed;
            if(c.a < 0)
            {
                c.a = 0;
            }
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
            if (c.a > 1)
            {
                c.a = 1;
            }
            fadeImage.color = c;
            yield return null;
        }
    }

    private void OnDestroy()
    {
        if (autoPlayToggle != null)
        {
            autoPlayToggle.onValueChanged.RemoveListener(SetAutoPlay);
        }
    }
}
