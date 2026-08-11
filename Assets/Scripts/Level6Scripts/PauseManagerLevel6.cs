using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManagerLevel6 : MonoBehaviour
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
        if (!PlayerControllerLevel6.instance.isDead)
        {
            CursorController.instance.ShowCursor();
            thirdPersonScript.enabled = false;
            playerController.enabled = false;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if (!PlayerControllerLevel6.instance.isDead)
        {
            CursorController.instance.HideCursor();
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
