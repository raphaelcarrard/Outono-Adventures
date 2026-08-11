using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ReadMessageScript : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public InputActionReference interactAction;
    public PauseManager pm;
    public GameObject messageImage, objectiveText;
    public MonoBehaviour thirdPersonScript, playerController, pauseManager;

    private bool playerNear = false;

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
        messageText.text = "";
    }

    void Update()
    {
        if (playerNear && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            CursorController.instance.ShowCursor();
            messageImage.SetActive(true);
            //StartCoroutine(NGIO.UnlockMedal(91498, OnMedalUnlocked));
            SteamAchievements.instance.UnlockAchievement("readMessage");
            thirdPersonScript.enabled = false;
            playerController.enabled = false;
	    pauseManager.enabled = false;
            messageText.enabled = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            string key = interactAction.action.GetBindingDisplayString();
            messageText.text = "Press " + key + " to read the message.";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            messageText.text = "";
        }
    }

    public void CloseMessage()
    {
        CursorController.instance.HideCursor();
        messageImage.SetActive(false);
        thirdPersonScript.enabled = true;
        playerController.enabled = true;
        pauseManager.enabled = true;
	messageText.enabled = true;
        objectiveText.SetActive(true);
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Read Message Unlocked!");
     }*/
}
