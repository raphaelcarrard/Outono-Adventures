using UnityEngine;
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
                   messageText.text = "Press " + key + " to enter the boss battle";
                }
                else
                {
                   messageText.text = "Press " + key + " to enter the level " + levelNumber;
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
