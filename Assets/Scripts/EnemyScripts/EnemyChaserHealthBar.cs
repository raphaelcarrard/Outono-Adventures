using UnityEngine;
using UnityEngine.UI;

public class EnemyChaserHealthBar : MonoBehaviour
{

    public Image fillImage;
    EnemyChaser enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyChaser>();
    }

    void Update()
    {
        float healthPercent = (float)enemy.GetCurrentLifes() / enemy.maxLives;
        fillImage.fillAmount = healthPercent;
    }
}
