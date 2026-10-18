using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class PickupPoint : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public PauseManagerForLevel4 pm;
    public CarController player;
    public Button startDeliveryButton;
    private bool playerNear = false;
    private PlayerControlsManager inputActions;
    private int reward;

    private void Awake()
    {
        inputActions = new PlayerControlsManager();
        if (PlayerPrefs.HasKey("rebinds"))
        {
            inputActions.Player.Interaction.LoadBindingOverridesFromJson(PlayerPrefs.GetString("rebinds"));
        }
    }

    void OnEnable()
    {
        inputActions.Enable();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Start()
    {
        messageText.text = "";
        startDeliveryButton.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerNear)
        {
            return;
        }
        if (DeliveryManager.instance.CarryingOrder)
        {
            return;
        }
        if (playerNear && !player.isDead && inputActions.Player.Interaction.WasPressedThisFrame() && !pm.isPaused)
        {
            DeliveryManager.instance.StartDelivery(reward);
            gameObject.SetActive(false);
            playerNear = false;
            messageText.text = "";
        }
    }

    public void StartDelivery()
    {
        if (playerNear && !player.isDead && !pm.isPaused)
        {
            DeliveryManager.instance.StartDelivery(reward);
            gameObject.SetActive(false);
            playerNear = false;
            startDeliveryButton.gameObject.SetActive(false);
            messageText.text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (DeliveryManager.instance.CarryingOrder)
        {
            return;
        }
        playerNear = true;
        reward = DeliveryManager.instance.GenerateReward();
        string key = inputActions.Player.Interaction.GetBindingDisplayString();
        if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
        {
            messageText.text = "Press " + key + $" to start the delivery. Reward: ${reward}";
        }
        else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
        {
            startDeliveryButton.gameObject.SetActive(true);
            messageText.text = $"Reward: ${reward}";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        playerNear = false;
        if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
        {
            messageText.text = "";
        }
        else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
        {
            startDeliveryButton.gameObject.SetActive(false);
            messageText.text = "";
        }
    }
}
