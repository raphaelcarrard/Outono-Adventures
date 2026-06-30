using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManagerInKartPhase : MonoBehaviour
{

    public GameObject pausePanel;
    public string menuScene;
    public InputActionReference pauseAction;
    public MonoBehaviour kartController, thirdPersonScript;
    public bool isPaused = false;

    void OnEnable()
    {
        pauseAction.action.Enable();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            pauseAction.action.actionMap.asset.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
    }

    void OnDisable()
    {
        pauseAction.action.Disable();
    }

    void Update()
    {
        if (pauseAction.action.WasCompletedThisFrame())
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if(!KartController.instance.isDead)
        {
	   kartController.enabled = false;
           thirdPersonScript.enabled = false;
           pausePanel.SetActive(true);
           Time.timeScale = 0f;
           isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if(!KartController.instance.isDead)
        {
	   kartController.enabled = true;
           thirdPersonScript.enabled = true;
           pausePanel.SetActive(false);
           Time.timeScale = 1f;
           isPaused = false;
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuScene);
    }
}
