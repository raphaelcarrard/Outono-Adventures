using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;
using System.IO;

public class ScreenshotFunction : MonoBehaviour
{
    public static ScreenshotFunction instance;
    public InputActionReference screenshotAction;
    public GameObject textScreenshotCaptured;

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

    void OnEnable()
    {
        screenshotAction.action.Enable();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            screenshotAction.action.actionMap.asset.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
    }

    void OnDisable()
    {
        screenshotAction.action.Disable();
    }

    void Update()
    {
        if (screenshotAction.action.WasPressedThisFrame())
        {
            string imagePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            string completePath = Path.Combine(imagePath, Application.productName);
            DirectoryInfo folderScreenshot = Directory.CreateDirectory(completePath);
            ScreenCapture.CaptureScreenshot(Path.Combine(folderScreenshot.FullName, "print-" + DateTime.Now.Ticks + ".png"));
            StartCoroutine(TickEffect());
        }
    }

    IEnumerator TickEffect()
    {
       yield return new WaitForSeconds(0.3f);
       textScreenshotCaptured.SetActive(true);
       yield return new WaitForSeconds(0.01f);
       textScreenshotCaptured.SetActive(false);
    }
}
