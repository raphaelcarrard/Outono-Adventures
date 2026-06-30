using UnityEngine;

public static class LevelProgressManager
{
    static string levelKey = "UnlockedLevel";

    public static int GetUnlockedLevel()
    {
        return PlayerPrefs.GetInt(levelKey, 1);
    }

    public static void UnlockNextLevel(int completedLevel)
    {
        int currentUnlocked = GetUnlockedLevel();
        if (completedLevel >= currentUnlocked)
        {
            PlayerPrefs.SetInt(levelKey, completedLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
