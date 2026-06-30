using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{

    public GameObject pausePanel;
    public string menuScene;
    public InputActionReference pauseAction;
    public MonoBehaviour thirdPersonScript, playerController;
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
        if (!PlayerController.instance.isDead)
        {
            thirdPersonScript.enabled = false;
            playerController.enabled = false;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (!PlayerController.instance.isDead)
        {
            thirdPersonScript.enabled = true;
            playerController.enabled = true;
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
