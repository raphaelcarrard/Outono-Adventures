using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class PreferencesManager : MonoBehaviour
{

    [Header("Audio")]
    public AudioMixer audioMixer;
    public Slider musicSlider;
    public Slider SFXSlider;

    [Header("Display")]
    public Toggle fullscreenToggle;
    public Toggle FPSToggle;

    [Header("Dropdowns")]
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;

    public GameObject PreferencesPanel, controlsPanel, screenshotObject;

    Resolution[] resolutions;

    void Start()
    {
        SetupResolutions();
        SetupQuality();
        LoadSettings();
    }

    void SetupResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
        }
        resolutionDropdown.AddOptions(options);
    }

    void SetupQuality()
    {
        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(new List<string>(QualitySettings.names));
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetFullScreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetInt("FullScreen", fullscreen ? 1 : 0);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("Quality", qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("Resolution", resolutionIndex);
    }

    public void SetFPSCounter(bool enabled)
    {
        PlayerPrefs.SetInt("ShowFPS", enabled ? 1 : 0);
    }

    void LoadSettings()
    {
        float music = PlayerPrefs.GetFloat("MusicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("SFXVolume", 1f);
        musicSlider.value = music;
        SFXSlider.value = sfx;
        SetMusicVolume(music);
        SetSFXVolume(sfx);
        bool fullscreen = PlayerPrefs.GetInt("FullScreen", 1) == 1;
        fullscreenToggle.isOn = fullscreen;
        Screen.fullScreen = fullscreen;
        bool showFPS = PlayerPrefs.GetInt("ShowFPS", 0) == 1;
        FPSToggle.isOn = showFPS;
        int quality = PlayerPrefs.GetInt("Quality", QualitySettings.GetQualityLevel());
        qualityDropdown.value = quality;
        int resolution = PlayerPrefs.GetInt("Resolution", 0);
        resolutionDropdown.value = resolution;
    }

    public void ClosePanel()
    {
        PreferencesPanel.SetActive(false);
    }

    public void CloseControlsPanel()
    {
        KeyRebindUI[] rebindingUI = FindObjectsOfType<KeyRebindUI>();
        foreach (var ui in rebindingUI)
        {
            ui.RefreshUI();
        }
        MainMenu.instance.screenshotObject.SetActive(true);
        controlsPanel.SetActive(false);
    }
}
