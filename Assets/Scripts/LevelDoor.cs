using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class LevelDoor : MonoBehaviour
{

    public int levelNumber;
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public InputActionReference interactAction;
    public PauseManager pm;
    public Button actionButton;
    public TextMeshProUGUI buttonText;
    public GameObject virtualButtons;

    bool playerNear;

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
        actionButton.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            if (IsUnlocked())
            {
                string key = interactAction.action.GetBindingDisplayString();
                if(levelNumber == 9)
                {
                    if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
                    {
                        messageText.text = "Press " + key + " to enter the boss battle";
                    }
                    else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
                    {
                        actionButton.gameObject.SetActive(true);
                        buttonText.text = "Enter the boss battle";
                        actionButton.GetComponent<Image>().color = Color.red;
                    }
                }
                else
                {
                    if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
                    {
                        messageText.text = "Press " + key + " to enter the level " + levelNumber;
                    }
                    else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
                    {
                        actionButton.gameObject.SetActive(true);
                        buttonText.text = "Enter the level " + levelNumber;
                        actionButton.GetComponent<Image>().color = Color.green;
                    }
                }
            }
            else
            {
                int requiredLevel = levelNumber - 1;
                messageText.text = "You need to complete level " + requiredLevel + " to access this door.";
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            messageText.text = "";
            actionButton.gameObject.SetActive(false);
        }
    }

    public void EnterTheLevel()
    {
        if (IsUnlocked())
        {
            fader.FadeToScene(sceneName);
            if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                virtualButtons.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (playerNear && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            if (IsUnlocked())
            {
                fader.FadeToScene(sceneName);
            }
        }   
    }

    bool IsUnlocked()
    {
        return levelNumber <= LevelProgressManager.GetUnlockedLevel();
    }
}
