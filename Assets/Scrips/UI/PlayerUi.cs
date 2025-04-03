using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Player playerStats;
    public Image hpFillImage; // Thanh máu

    private void Start()
    {
        playerStats.OnHealthChanged += UpdateHPBar;
    }

    private void UpdateHPBar(int newHealth)
    {
        float fillAmount = (float)newHealth / playerStats.maxHP;
        hpFillImage.fillAmount = fillAmount;
    }
}
