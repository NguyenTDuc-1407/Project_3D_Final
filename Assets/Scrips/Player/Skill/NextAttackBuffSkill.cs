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

    private void ApplyBuff(Player playerAttack, GameObject target, float attackDamage)
    {
        if (!isBuffed) return;
        if (!isBuffed || target == null) return;

        float damage = playerAttack.attackDamage;
        bool isCritical = (playerAttack.critChance > 0) && (Random.value < playerAttack.critChance);

        if (isCritical)
        {
            damage *= attackBoostOnCrit;
            damage *= playerAttack.critMultiplier;
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
      
        if (PlayerFunction != null)
        {
            PlayerFunction.OnAttack -= ApplyBuff;
        }
    }
}
