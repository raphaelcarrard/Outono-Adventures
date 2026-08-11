using UnityEngine;
using TMPro;

public class LevelGoal : MonoBehaviour
{
    public string sceneName;
    public TextMeshProUGUI messageText;
    public SceneFader fader;
    public MonoBehaviour thirdPersonScript, playerController;

    void OnTriggerEnter(Collider other)
    {
        messageText.text = "Level Complete!";
        //StartCoroutine(NGIO.UnlockMedal(91500, OnMedalUnlocked));
        SteamAchievements.instance.UnlockAchievement("level3");
        thirdPersonScript.enabled = false;
        playerController.enabled = false;
        LevelProgressManager.UnlockNextLevel(3);
        fader.FadeToScene(sceneName);
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Level 3 Unlocked!");
     }*/
}
