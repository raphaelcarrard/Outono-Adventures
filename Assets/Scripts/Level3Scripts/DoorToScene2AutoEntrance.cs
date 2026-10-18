using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
public class DoorToScene2AutoEntrance : MonoBehaviour
{
    public string sceneToLoad;
    public TextMeshProUGUI doorEntranceText;
    public SceneFader fader;
    public InputActionReference interactAction;
    public PauseManager pm;
    public GameObject virtualControls;
    public Button enterTheDoorButton;

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
        fader = FindObjectOfType<SceneFader>();
        doorEntranceText.text = "";
        enterTheDoorButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerNear && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            fader.FadeToScene(sceneToLoad);
        }
    }

    public void EnterTheDoor()
    {
        virtualControls.SetActive(false);
        fader.FadeToScene(sceneToLoad);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            string key = interactAction.action.GetBindingDisplayString();
            if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
            {
                doorEntranceText.text = "Press " + key + " to enter the door.";
            }
            else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                enterTheDoorButton.gameObject.SetActive(true);
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
                doorEntranceText.text = "";
            }
            else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                enterTheDoorButton.gameObject.SetActive(false);
            }
        }
    }
}
