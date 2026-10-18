using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseManagerInKartPhase : MonoBehaviour
{

    public GameObject pausePanel;
    public string menuScene;
    public InputActionReference pauseAction;
    public ThirdPersonCameraTwo thirdPersonScript;
    public CameraFollow cameraFollow;
    public MonoBehaviour kartController;
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
           CursorController.instance.ShowCursor();
           kartController.enabled = false;
            if (PlayerPrefs.GetInt("CameraMode") == 0)
            {
                thirdPersonScript.enabled = false;
            }
            else if (PlayerPrefs.GetInt("CameraMode") == 1)
            {
                cameraFollow.enabled = false;
            }
            pausePanel.SetActive(true);
           Time.timeScale = 0f;
           isPaused = true;
        }
    }

    public void ResumeGame()
    {
        if(!KartController.instance.isDead)
        {
           CursorController.instance.HideCursor();
           kartController.enabled = true;
            if (PlayerPrefs.GetInt("CameraMode") == 0)
            {
                thirdPersonScript.enabled = true;
            }
            else if (PlayerPrefs.GetInt("CameraMode") == 1)
            {
                cameraFollow.enabled = true;
            }
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
