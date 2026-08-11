using UnityEngine;
using Steamworks;

public class SteamAchievements : MonoBehaviour
{

    public static SteamAchievements instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UnlockAchievement(string id)
    {
        if (!SteamManager.Initialized)
        {
            return;
        }
        SteamUserStats.SetAchievement(id);
        SteamUserStats.StoreStats();
    }
}
