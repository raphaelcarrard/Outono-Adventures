using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class CircusExit : MonoBehaviour
{

    public string sceneToLoad;
    public TextMeshProUGUI circusExitText;
    public SceneFader fader;
    public InputActionReference interactAction;
    public PauseManager pm;
    public Button exitTheCircusButton;
    public GameObject virtualButtons;

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
        circusExitText.text = "";
        exitTheCircusButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (playerNear && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            fader.FadeToScene(sceneToLoad);
        }
    }

    public void ExitTheCircus()
    {
        fader.FadeToScene(sceneToLoad);
        if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
        {
            virtualButtons.SetActive(false);
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
                circusExitText.text = "Press " + key + " to exit the circus.";
            }
            else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
            {
                exitTheCircusButton.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            circusExitText.text = "";
            exitTheCircusButton.gameObject.SetActive(false);
        }
    }
}
