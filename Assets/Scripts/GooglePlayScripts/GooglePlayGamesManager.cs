using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using GooglePlayGames;
//using UnityEngine.SocialPlatforms;
//using GooglePlayGames.BasicApi;

public class GooglePlayGamesManager : MonoBehaviour
{

    public static GooglePlayGamesManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }   
    }

    /*void Start()
    {
        PlayGamesPlatform.Activate();
        Login();
    }

    public void Login()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            Debug.Log("Success to login Google Play Games!");
        }
        else
        {
            Debug.LogError("Fail to login Google Play Games!");
        }
    }

    public void ShowAchievements()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }*/

    public void CheckAchievements()
    {
        if (WhereAmI.instance.levelName == "Level2")
        {
            Debug.Log("Level 2 achievement unlocked");
            //PlayGamesPlatform.Instance.ReportProgress(Achievements.achievementLevel2, 100f, (bool success) => { });
        }
        if (WhereAmI.instance.levelName == "Level4")
        {
            Debug.Log("Level 4 achievement unlocked");
            //PlayGamesPlatform.Instance.ReportProgress(Achievements.achievementLevel4, 100f, (bool success) => { });
        }
        if (WhereAmI.instance.levelName == "Level6")
        {
            Debug.Log("Level 6 achievement unlocked");
            //PlayGamesPlatform.Instance.ReportProgress(Achievements.achievementLevel6, 100f, (bool success) => { });
        }
        if (WhereAmI.instance.levelName == "Level8")
        {
            Debug.Log("Level 8 achievement unlocked");
            //PlayGamesPlatform.Instance.ReportProgress(Achievements.achievementLevel8, 100f, (bool success) => { });
        }
        if (WhereAmI.instance.levelName == "Level9")
        {
            Debug.Log("Game Complete achievement unlocked");
            //PlayGamesPlatform.Instance.ReportProgress(Achievements.achievementGameComplete, 100f, (bool success) => { });
        }
    }
}
