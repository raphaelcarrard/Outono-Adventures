using System.Collections;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Start()
    {
        StartCoroutine("timeToExitGame");
    }

    IEnumerator timeToExitGame()
    {
        #if UNITY_WEBGL
        //StartCoroutine(NGIO.UnlockMedal(91494, OnMedalUnlocked));
        #endif
        SteamAchievements.instance.UnlockAchievement("exitGame");
        yield return new WaitForSeconds(5f);
        Debug.Log("Software Terminated");
        #if !UNITY_WEBGL
        Application.Quit();
        #endif
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Exit Game Unlocked!");
     }*/
}
