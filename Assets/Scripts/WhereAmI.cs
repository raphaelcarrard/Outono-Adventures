using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereAmI : MonoBehaviour
{

    public static WhereAmI instance;
    public string levelName;
    public int levelNumber = -1;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += VerifyLevel;
    }

    
    void VerifyLevel(Scene cena, LoadSceneMode mode)
    {
        levelNumber = SceneManager.GetActiveScene().buildIndex;
        levelName = SceneManager.GetActiveScene().name;
    }
}
