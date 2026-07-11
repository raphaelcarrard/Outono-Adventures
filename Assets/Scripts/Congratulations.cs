using UnityEngine;

public class Congratulations : MonoBehaviour
{
    public GameObject congratsButtons;
    public string sceneToBackToMenu, sceneToExitGame;
    public SceneFader fader;

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
}
