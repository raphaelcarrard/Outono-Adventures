using UnityEngine;

public class CameraModeManager : MonoBehaviour
{

    public static CameraModeManager instance;

    public enum CameraMode
    {
        Mouse = 0,
        FollowPlayer = 1
    }

    [Header("Camera Scripts")]
    public ThirdPersonCameraTwo mouseCamera;
    public CameraFollow followCamera;

    private const string CameraModeKey = "CameraMode";
    public CameraMode currentMode;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        int savedMode = PlayerPrefs.GetInt(CameraModeKey, 0);
        savedMode = Mathf.Clamp(savedMode, 0, 1);
        ApplyCameraMode((CameraMode)savedMode, false);
    }

    private void ApplyCameraMode(CameraMode mode, bool save)
    {
        if (mouseCamera != null)
        {
            mouseCamera.enabled = mode == CameraMode.Mouse;
        }
        if (followCamera != null)
        {
            followCamera.enabled = mode == CameraMode.FollowPlayer;
        }
        if (save)
        {
            PlayerPrefs.SetInt(CameraModeKey, (int)mode);
            PlayerPrefs.Save();
        }
    }

    public void UseMouseCamera()
    {
        ApplyCameraMode(CameraMode.Mouse, true);
    }

    public void UseFollowCamera()
    {
        ApplyCameraMode(CameraMode.FollowPlayer, true);
    }
}