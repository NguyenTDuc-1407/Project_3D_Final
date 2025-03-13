using UnityEngine;

public class NextAttackBuffSkill : SkillBase
{
    public float buffMultiplier = 2f;
    public float attackBoostOnCrit = 1.5f;
    private bool isBuffed = false;
    GameManager gameManager;
    private Player player;
    private PlayerFunction PlayerFunction;

    private void Start()
    {
        PlayerFunction = GetComponent<PlayerFunction>();
        if (PlayerFunction != null)
        {
            PlayerFunction.OnAttack += ApplyBuff;
        }
    }

    protected override void ExecuteSkill()
    {
        if (isBuffed) return;

        isBuffed = true;
        Debug.Log(skillName + " activated! Next attack buffed.");
    }

    private void ApplyBuff(Player playerAttack, GameObject target, float attackDamage)
    {
        if (!isBuffed) return;

        if (target == null)
        {
            Debug.LogError("❌ ApplyBuff() bị gọi nhưng target bị null!");
            return;
        }

        Minions enemy = target.GetComponent<Minions>();
        if (enemy == null)
        {
            Debug.LogError("❌ Target không có component Minions! Target: " + target.name);
            return;
        }

        float damage = playerAttack.attackDamage;
        bool isCritical = (playerAttack.critChance > 0) && (Random.value < playerAttack.critChance);

        if (isCritical)
        {
            damage *= attackBoostOnCrit;
            damage *= playerAttack.critMultiplier;
        }
        else
        {
            damage *= buffMultiplier;
        }

        enemy.DameEnemy((int)damage);
        Debug.Log("✅ Buffed attack dealt " + damage + " damage to " + target.name);

        isBuffed = false;
    }

    private void OnDisable()
    {
        isBuffed = false;
    }

    private void OnDestroy()
    {
      
        if (PlayerFunction != null)
        {
            PlayerFunction.OnAttack -= ApplyBuff;
        }
    }
}
