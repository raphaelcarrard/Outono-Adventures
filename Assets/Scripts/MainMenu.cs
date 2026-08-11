using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public static MainMenu instance;
    
    public GameObject continuePanel, mainMenuButtons, optionsPanel, preferencesPanel, controlsPanel, creditsPanel, quitPanel, screenshotObject, insideOfCircusButton;
    public string sceneToLoadTheGame, sceneToExitGame, sceneToOutsideOfCircus, sceneToInsideOfCircus;
    public SceneFader fader;

    void Awake()
    {
       instance = this;
       screenshotObject = GameObject.Find("Screenshot");
    }

    public void StartGame()
    {
        if(PlayerPrefs.GetInt("firsttime") == 0)
        {
           mainMenuButtons.SetActive(false);
           fader.FadeToScene(sceneToLoadTheGame);
           PlayerPrefs.SetInt("firsttime", 1);
        }
        else
        {
           continuePanel.SetActive(true);
        }
        if(PlayerPrefs.GetInt("firsttimeincircus") == 0)
        {
           insideOfCircusButton.SetActive(false);
        }
        else
        {
           insideOfCircusButton.SetActive(true);
        }
    }

    public void WatchIntro()
    {
        continuePanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        fader.FadeToScene(sceneToLoadTheGame);
    }

    public void SkipToOutsideOfCircus()
    {
        continuePanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        fader.FadeToScene(sceneToOutsideOfCircus);
    }

    public void SkipToInsideOfCircus()
    {
        continuePanel.SetActive(false);
        mainMenuButtons.SetActive(false);
        fader.FadeToScene(sceneToInsideOfCircus);
    }

    public void CloseContinuePanel()
    {
        continuePanel.SetActive(false);
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

    public void AccessTheCredits()
    {
        creditsPanel.SetActive(true);
        //StartCoroutine(NGIO.UnlockMedal(91493, OnMedalUnlocked));
        SteamAchievements.instance.UnlockAchievement("credits");
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

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Credits Unlocked!");
     }*/
}
