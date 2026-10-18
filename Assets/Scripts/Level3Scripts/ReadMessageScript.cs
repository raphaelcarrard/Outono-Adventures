using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class ReadMessageScript : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public GameObject virtualControls;
    public InputActionReference interactAction;
    public PauseManager pm;
    public GameObject messageImage, objectiveText;
    public ThirdPersonCameraTwo thirdPersonScript;
    public CameraFollow cameraFollow;
    public MonoBehaviour playerController, pauseManager;
    public Button readMessageButton;

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
        readMessageButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerNear && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            CursorController.instance.ShowCursor();
            messageImage.SetActive(true);
            StartCoroutine(NGIO.UnlockMedal(91498, OnMedalUnlocked));
            //SteamAchievements.instance.UnlockAchievement("readMessage");
            if (PlayerPrefs.GetInt("CameraMode") == 0)
            {
                thirdPersonScript.enabled = false;
            }
            else if (PlayerPrefs.GetInt("CameraMode") == 1)
            {
                cameraFollow.enabled = false;
            }
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
            if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
            {
                messageText.text = "Press " + key + " to read the message.";
            }
            else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                readMessageButton.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
            {
                messageText.text = "";
            }
            else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                readMessageButton.gameObject.SetActive(false);
            }
        }
    }

    public void ReadMessage()
    {
        CursorController.instance.ShowCursor();
        messageImage.SetActive(true);
        virtualControls.SetActive(false);
        readMessageButton.gameObject.SetActive(false);
        StartCoroutine(NGIO.UnlockMedal(91498, OnMedalUnlocked));
        //SteamAchievements.instance.UnlockAchievement("readMessage");
        if (PlayerPrefs.GetInt("CameraMode") == 0)
        {
            thirdPersonScript.enabled = false;
        }
        else if (PlayerPrefs.GetInt("CameraMode") == 1)
        {
            cameraFollow.enabled = false;
        }
        playerController.enabled = false;
        pauseManager.enabled = false;
        messageText.enabled = false;
    }

    public void CloseMessage()
    {
        CursorController.instance.HideCursor();
        messageImage.SetActive(false);
        virtualControls.SetActive(true);
        readMessageButton.gameObject.SetActive(true);
        if (PlayerPrefs.GetInt("CameraMode") == 0)
        {
            thirdPersonScript.enabled = true;
        }
        else if (PlayerPrefs.GetInt("CameraMode") == 1)
        {
            cameraFollow.enabled = true;
        }
        playerController.enabled = true;
        pauseManager.enabled = true;
	    messageText.enabled = true;
        objectiveText.SetActive(true);
    }

     public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Read Message Unlocked!");
     }
}
