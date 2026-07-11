using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossCutsceneManager : MonoBehaviour
{
    [Header("References")]
    public BossController boss;
    public PlayerController player;
    public AudioSource levelMusic;

    [Header("Letterbox")]
    public RectTransform topBar;
    public RectTransform bottomBar;
    public float barAnimationTime = 0.5f;

    [Header("Dialogue")]
    public GameObject dialoguePanel;
    public GameObject topLeftPlayerLifes;
    public GameObject topRightBossLifes;
    public Image leftPortrait;
    public Image rightPortrait;
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public DialogueLine[] dialogue;
    public float textSpeed = 0.03f;

    [Header("Countdown")]
    public CountdownManagerBoss countdown;

    int currentLine;
    bool isTyping;
    Coroutine typingCoroutine;

    public void Start()
    {
        levelMusic.Stop();
    }

    public void StartCutscene()
    {
        StartCoroutine(CutsceneRoutine());
    }

    IEnumerator CutsceneRoutine()
    {
        player.anim.SetFloat("Speed", 0);
        player.enabled = false;
        boss.fightStarted = false;
        topLeftPlayerLifes.SetActive(false);
        topRightBossLifes.SetActive(false);
        yield return StartCoroutine(ShowBars());
        dialoguePanel.SetActive(true);
        currentLine = 0;
        ShowLine();
        while (dialoguePanel.activeSelf)
        {
            yield return null;
        }
        yield return StartCoroutine(HideBars());
        if (countdown != null)
        {
            yield return countdown.StartCountdown();
        }
        levelMusic.Play();
        topLeftPlayerLifes.SetActive(true);
        topRightBossLifes.SetActive(true);
        boss.fightStarted = true;
        player.enabled = true;
    }

    void ShowLine()
    {
        if (currentLine >= dialogue.Length)
        {
            dialoguePanel.SetActive(false);
            return;
        }
        DialogueLine line = dialogue[currentLine];
        leftPortrait.sprite = line.leftPortrait;
        rightPortrait.sprite = line.rightPortrait;
        nameText.text = line.characterName;
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(line.text));
    }

    IEnumerator TypeText(string text)
    {
        isTyping = true;
        dialogueText.text = "";
        foreach(char c in text)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
    }

    public void NextDialogue()
    {
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = dialogue[currentLine].text;
            isTyping = false;
            return;
        }
        currentLine++;
        ShowLine();
    }

    IEnumerator ShowBars()
    {
        Vector2 topStart = topBar.anchoredPosition;
        Vector2 bottomStart = bottomBar.anchoredPosition;
        Vector2 topEnd = new Vector2(0, 324);
        Vector2 bottomEnd = new Vector2(0, -324);
        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime / barAnimationTime;
            topBar.anchoredPosition = Vector2.Lerp(topStart, topEnd, t);
            bottomBar.anchoredPosition = Vector2.Lerp(bottomStart, bottomEnd, t);
            yield return null;
        }
    }

    IEnumerator HideBars()
    {
        Vector2 topStart = topBar.anchoredPosition;
        Vector2 bottomStart = bottomBar.anchoredPosition;
        Vector2 topEnd = new Vector2(0, 491);
        Vector2 bottomEnd = new Vector2(0, -475);
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / barAnimationTime;
            topBar.anchoredPosition = Vector2.Lerp(topStart, topEnd, t);
            bottomBar.anchoredPosition = Vector2.Lerp(bottomStart, bottomEnd, t);
            yield return null;
        }
    }
}
