using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicSceneController : MonoBehaviour
{

    [Header("Allowed Scenes")]
    public string[] allowedScenes;

    [Header("Destroy Scenes")]
    public string[] destroyScenes;

    private static MusicSceneController instance;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach(string sceneName in destroyScenes)
        {
            if (scene.name == sceneName)
            {
                Destroy(gameObject);
                break;
            }
        }
        bool allowed = false;
        foreach (string sceneName in allowedScenes)
        {
            if (scene.name == sceneName)
            {
                allowed = true;
                break;
            }
        }
        if (allowed)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
