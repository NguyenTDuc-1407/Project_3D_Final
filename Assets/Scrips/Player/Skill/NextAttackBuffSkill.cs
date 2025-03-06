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
        FindObjectOfType<WarriorAttack>().OnAttack += ApplyBuff;
        PlayerFunction = FindObjectOfType<PlayerFunction>();
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

    private void ApplyBuff(WarriorAttack attackSystem, GameObject target)
    private void ApplyBuff(Player playerAttack, GameObject target, float attackDamage)
    {
        if (!isBuffed) return;
        if (!isBuffed || target == null) return;

        float damage = attackSystem.baseDamage;
        bool isCritical = Random.value < attackSystem.critChance;
        float damage = attackDamage;
        bool isCritical = (playerAttack.critChance > 0) && (Random.value < playerAttack.critChance);

        if (isCritical)
        {
            damage *= attackBoostOnCrit;
            damage *= attackSystem.critMultiplier;
            damage *= Mathf.Max(1f, playerAttack.critMultiplier);
        }
        else
        {
            damage *= buffMultiplier; 
            damage *= buffMultiplier;
        }

        target.GetComponent<Minions>()?.DameEnemy((int)damage);
        Debug.Log("Buffed attack dealt " + damage + " damage to " + target.name);
        Minions enemy = target.GetComponent<Minions>();
        if (enemy != null)
        {
            enemy.DameEnemy((int)damage);
            Debug.Log("Buffed attack dealt " + damage + " damage to " + target.name);
        }

        isBuffed = false;
    }

    private void OnDisable()
    {
        isBuffed = false;
    }

    private void OnDestroy()
    {
        // Gỡ đăng ký khi GameObject bị hủy
        FindObjectOfType<WarriorAttack>().OnAttack -= ApplyBuff;
        if (PlayerFunction != null)
        {
            PlayerFunction.OnAttack -= ApplyBuff;
        }
    }
}
