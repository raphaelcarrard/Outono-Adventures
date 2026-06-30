using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{

    public TextMeshProUGUI fpsText;
    float timer;
    int frameCount;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("ShowFPS", 0) == 0)
        {
            fpsText.gameObject.SetActive(false);
            return;
        }
        fpsText.gameObject.SetActive(true);
        frameCount++;
        timer += Time.unscaledDeltaTime;
        if (timer >= 1f)
        {
            int fps = Mathf.RoundToInt(frameCount / timer);
            fpsText.text = "FPS: " + fps;
            frameCount = 0;
            timer = 0f;
        }
    }
}
