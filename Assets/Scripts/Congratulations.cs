using UnityEngine;

public class Congratulations : MonoBehaviour
{
    public GameObject congratsButtons;
    public string sceneToBackToMenu, sceneToExitGame;
    public SceneFader fader;

    void Start()
    {
        //StartCoroutine(NGIO.UnlockMedal(91507, OnMedalUnlocked));
        SteamAchievements.instance.UnlockAchievement("gameCompleted");
    }

    public void BackToMenu()
    {
        congratsButtons.SetActive(false);
        fader.FadeToScene(sceneToBackToMenu);
    }

    public void ExitGame()
    {
        congratsButtons.SetActive(false);
        fader.FadeToScene(sceneToExitGame);
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Game Completed Unlocked!");
     }*/
}
