using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class DeliveryPoint : MonoBehaviour
{

    public TextMeshProUGUI messageText;
    public InputActionReference interactAction;
    public PauseManagerForLevel4 pm;
    public GameObject pickupPoint;
    public CarController player;

    [HideInInspector]
    public bool isActiveDelivery = false;

    private bool playerNear = false;
    private PlayerControlsManager inputActions;

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
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (!playerNear)
        {
            return;
        }
        if (!player.isDead && inputActions.Player.Interaction.WasPressedThisFrame() && !pm.isPaused)
        {
            DeliveryManager.instance.CompleteDelivery(this);
            pickupPoint.SetActive(true);
            messageText.text = "";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isActiveDelivery)
        {
            return;
        }
        if (!other.CompareTag("Player"))
        {
            return;
        }
        playerNear = true;
        string key = inputActions.Player.Interaction.GetBindingDisplayString();
        messageText.text = "Press " + key + " to complete the delivery.";
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
