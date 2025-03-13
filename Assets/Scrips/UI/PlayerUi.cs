using UnityEngine;
using UnityEngine.UI;

public class PlayerUi : MonoBehaviour
{
    public Slider HealthBar;
    public Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();

        if (player == null)
        {
            Debug.LogError("Player không tồn tại");
            return;
        }

        if (HealthBar == null)
        {
            Debug.LogError("HealthBar Null");
            return;
        }

        HealthBar.maxValue = player.maxHP;
        HealthBar.value = player.currentHP;
    }

    public void UpdateUiHp()
    {
        if (HealthBar != null && player != null)
        {
            HealthBar.value = player.currentHP / player.maxHP;
        }
    }
}
