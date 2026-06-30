using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{

    public Image fillImage;
    EnemyController enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyController>();
    }

    void Update()
    {
        float healthPercent = (float)enemy.GetCurrentLifes() / enemy.maxLives;
        fillImage.fillAmount = healthPercent;
    }
}
