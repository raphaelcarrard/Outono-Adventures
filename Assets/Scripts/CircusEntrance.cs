using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class CircusEntrance : MonoBehaviour
{

    public string sceneToLoad;
    public TextMeshProUGUI circusEntranceText;
    public SceneFader fader;
    public InputActionReference interactAction;
    public PauseManager pm;

    private bool playerNear = false;

    void OnEnable()
    {
        interactAction.action.Enable();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            interactAction.action.actionMap.asset.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
    }

    void OnDisable()
    {
        interactAction.action.Disable();
    }

    void Start()
    {
        fader = FindObjectOfType<SceneFader>();
        circusEntranceText.text = "";
    }

    void Update()
    {
        if (playerNear && interactAction.action.WasPressedThisFrame() && !pm.isPaused)
        {
            //StartCoroutine(NGIO.UnlockMedal(91495, OnMedalUnlocked));
            SteamAchievements.instance.UnlockAchievement("circusEntrance");
            PlayerPrefs.SetInt("firsttimeincircus", 1);
            fader.FadeToScene(sceneToLoad);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            string key = interactAction.action.GetBindingDisplayString();
            circusEntranceText.text = "Press " + key + " to enter the circus.";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            circusEntranceText.text = "";
        }
    }

     /*public void OnMedalUnlocked(NewgroundsIO.objects.Medal medal)
     {
         Debug.Log("Medal Enter of Circus Unlocked!");
     }*/
}
