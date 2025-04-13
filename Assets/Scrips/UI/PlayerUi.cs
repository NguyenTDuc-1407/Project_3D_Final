using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Player playerStats;
    public Image hpFillImage; // Thanh máu

    private void Start()
    {
        if (playerStats == null)
            Debug.LogError("playerStats chưa được gán!");
        playerStats.OnHealthChanged += UpdateHPBar;
    }

    public void UpdateHPBar(int newHealth)
    {
        float fillAmount = (float)newHealth / playerStats.maxHP;
        hpFillImage.fillAmount = fillAmount;
    }
}
