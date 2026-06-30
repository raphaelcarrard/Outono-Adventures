using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbarLevel6 : MonoBehaviour
{
    public Image fillImage;
    EnemyControllerLevel6 enemy;

    void Start()
    {
        enemy = GetComponentInParent<EnemyControllerLevel6>();
    }

    void Update()
    {
        float healthPercent = (float)enemy.GetCurrentHealth() / enemy.startingHealth;
        fillImage.fillAmount = healthPercent;
    }
}
