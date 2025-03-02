using UnityEngine;

public class NextAttackBuffSkill : SkillBase
{
    public float buffMultiplier = 2f;
    public float attackBoostOnCrit = 1.5f;
    private bool isBuffed = false;
    GameManager gameManager;
    private void Start()
    {
        FindObjectOfType<WarriorAttack>().OnAttack += ApplyBuff;
    }

    protected override void ExecuteSkill()
    {
        if (isBuffed) return;

        isBuffed = true;
        Debug.Log(skillName + " activated! Next attack buffed.");
    }

    private void ApplyBuff(WarriorAttack attackSystem, GameObject target)
    {
        if (!isBuffed) return;

        float damage = attackSystem.baseDamage;
        bool isCritical = Random.value < attackSystem.critChance;

        if (isCritical)
        {
            damage *= attackBoostOnCrit;
            damage *= attackSystem.critMultiplier;
        }
        else
        {
            damage *= buffMultiplier; 
        }

        target.GetComponent<Minions>()?.DameEnemy((int)damage);
        Debug.Log("Buffed attack dealt " + damage + " damage to " + target.name);

        isBuffed = false;
    }

    private void OnDestroy()
    {
        // Gỡ đăng ký khi GameObject bị hủy
        FindObjectOfType<WarriorAttack>().OnAttack -= ApplyBuff;
    }
}
