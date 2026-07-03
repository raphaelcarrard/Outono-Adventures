using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public static MainMenu instance;
    
    public GameObject mainMenuButtons, optionsPanel, preferencesPanel, controlsPanel, creditsPanel, quitPanel, screenshotObject;
    public string sceneToLoadTheGame, sceneToExitGame;
    public SceneFader fader;

    void Awake()
    {
       instance = this;
       screenshotObject = GameObject.Find("Screenshot");
    }

    public void StartGame()
    {
        mainMenuButtons.SetActive(false);
        fader.FadeToScene(sceneToLoadTheGame);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
    }

    public void OpenPreferences()
    {
        preferencesPanel.SetActive(true);
    }

    public void OpenControls()
    {
        controlsPanel.SetActive(true);
        screenshotObject.SetActive(false);
    }

    public void OpenMinigames()
    {

    }

    public void CloseMinigames()
    {

    }

    public void AccessTheCredits()
    {
        creditsPanel.SetActive(true);
    }

    public void CloseTheCredits()
    {
        creditsPanel.SetActive(false);
    }

    public void ExitGame()
    {
        quitPanel.SetActive(true);
    }

    public void NoQuit()
    {
        quitPanel.SetActive(false);
    }

    public void YesQuit()
    {
        quitPanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        fader.FadeToScene(sceneToExitGame);
    }
}
