using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class DeliveryPoint : MonoBehaviour
{

    public TextMeshProUGUI messageText;
    public InputActionReference interactAction;
    public PauseManagerForLevel4 pm;
    public GameObject pickupPoint;
    public CarController player;
    public Button completeDeliveryButton;

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
        completeDeliveryButton.gameObject.SetActive(false);
        completeDeliveryButton.onClick.AddListener(CompleteDelivery);
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

    public void CompleteDelivery()
    {
        if (!player.isDead && !pm.isPaused)
        {
            DeliveryManager.instance.CompleteDelivery(this);
            pickupPoint.SetActive(true);
            completeDeliveryButton.gameObject.SetActive(false);
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
        if (PlayerPrefs.GetInt("ShowVirtualControls") == 0)
        {
            messageText.text = "Press " + key + " to complete the delivery.";
        }
        else if (PlayerPrefs.GetInt("ShowVirtualControls") == 1)
        {
            completeDeliveryButton.gameObject.SetActive(true);
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
            completeDeliveryButton.gameObject.SetActive(false);
        }
    }
}
