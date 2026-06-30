using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PickupPoint : MonoBehaviour
{
    public TextMeshProUGUI messageText;
    public PauseManagerForLevel4 pm;
    public CarController player;

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
        if (!player.isDead && inputActions.Player.Interaction.WasPressedThisFrame() && !pm.isPaused)
        {
            DeliveryManager.instance.StartDelivery(reward);
            gameObject.SetActive(false);
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
        messageText.text = "Press " + key + $" to start the delivery. Reward: ${reward}";
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        playerNear = false;
        messageText.text = "";
    }
}
