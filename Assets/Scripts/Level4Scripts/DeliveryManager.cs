using UnityEngine;
using TMPro;

public class DeliveryManager : MonoBehaviour
{

    public static DeliveryManager instance;
    public GameObject objectiveCompletedText;
    public GameObject pickupPoint;
    public CarController player;

    [Header("Delivery Points")]
    public DeliveryPoint[] deliveryPoints;

    [Header("Money")]
    public TMP_Text moneyText;

    [Header("Timer")]
    public TMP_Text timerText;

    [Header("Audios")]
    public AudioSource audioSource;
    public AudioClip startDeliveryClip;
    public AudioClip completeDeliveryClip;

    public float deliveryTime = 60f;

    private float currentTime;
    private bool timerRunning;

    private DeliveryPoint currentDeliveryPoint;
    private bool carryingOrder;

    public int money;
    private int currentOrderReward;

    public int CurrentOrderReward => currentOrderReward;
    public bool CarryingOrder => carryingOrder;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        UpdateMoneyUI();
    }

    private void Update()
    {
        if (money >= 500)
        {
            objectiveCompletedText.SetActive(true);
        }
        if (!timerRunning)
        {
            return;
        }
        currentTime -= Time.deltaTime;
        UpdateTimerUI();
        if (currentTime <= 0)
        {
            DeliveryFailed();
        }
    }

    public void StartDelivery(int reward)
    {
        if (carryingOrder)
        {
            return;
        }
        audioSource.PlayOneShot(startDeliveryClip);
        carryingOrder = true;
        currentOrderReward = reward;
        int randomIndex = Random.Range(0, deliveryPoints.Length);
        currentDeliveryPoint = deliveryPoints[randomIndex];
        currentDeliveryPoint.gameObject.SetActive(true);
        currentDeliveryPoint.isActiveDelivery = true;
        currentTime = deliveryTime;
        timerRunning = true;
        UpdateTimerUI();
        Debug.Log($"New delivery: ${currentOrderReward}");
    }

    public void CompleteDelivery(DeliveryPoint point)
    {
        if (point != currentDeliveryPoint)
        {
            return;
        }
        audioSource.PlayOneShot(completeDeliveryClip);
        money += currentOrderReward;
        UpdateMoneyUI();
        carryingOrder = false;
        point.isActiveDelivery = false;
        point.gameObject.SetActive(false);
        currentDeliveryPoint = null;
        currentTime = 60;
        timerRunning = false;
        UpdateTimerUI();
        Debug.Log($"Delivery completed! +${currentOrderReward}");
    }

    public int GenerateReward()
    {
        return Random.Range(10, 60);
    }

    private void DeliveryFailed()
    {
        timerRunning = false;
        carryingOrder = false;
        if(currentDeliveryPoint != null)
        {
            currentDeliveryPoint.isActiveDelivery = false;
            currentDeliveryPoint.gameObject.SetActive(false);
        }
        currentDeliveryPoint = null;
        Debug.Log("Time's up! Delivery cancelled.");
        timerText.text = "TIME'S UP!";
        pickupPoint.SetActive(true);
        player.TakeDamage(1);
    }

    private void UpdateMoneyUI()
    {
        moneyText.text = $"${money} / 500";
    }

    private void UpdateTimerUI()
    {
        timerText.text = $"Time: {Mathf.CeilToInt(currentTime)}";
    }
}
