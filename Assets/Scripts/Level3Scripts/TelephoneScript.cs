using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class TelephoneScript : MonoBehaviour
{
    [Header("Audios")]
    public AudioClip ringingClip;
    public AudioClip voiceClip;
    public AudioClip hangupClip;

    public TextMeshProUGUI messageText;
    public InputActionReference interactAction;
    public PauseManager pm;

    private AudioSource audioSource;
    private bool playerNear = false;
    private bool callAnswered = false;

    void OnEnable()
    {
        interactAction.action.Enable();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            interactAction.action.actionMap.asset.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
    }

    void OnDisable()
    {
        interactAction.action.Disable();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartRinging();
        messageText.text = "";
    }

    void Update()
    {
        if (playerNear && !callAnswered && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            AnswerPhone();
            //StartCoroutine(NGIO.UnlockMedal(91499, OnMedalUnlocked));
            SteamAchievements.instance.UnlockAchievement("telephone");
            messageText.enabled = false;
        }
    }

    void StartRinging()
    {
        audioSource.clip = ringingClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    void AnswerPhone()
    {
        callAnswered = true;
        audioSource.Stop();
        audioSource.clip = voiceClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    void HangUp()
    {
        audioSource.Stop();
        audioSource.clip = hangupClip;
        audioSource.loop = false;
        audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            string key = interactAction.action.GetBindingDisplayString();
            if (!callAnswered)
            {
                messageText.text = "Press " + key + " to answer the phone.";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            messageText.enabled = true;
            messageText.text = "";
            if (callAnswered)
            {
                HangUp();
            }
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Telephone Unlocked!");
     }*/
}
