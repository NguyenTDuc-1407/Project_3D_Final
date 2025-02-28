using UnityEngine;

public class WarriorAttack : MonoBehaviour
{
    public float damage = 20f; // Sát th??ng g?c

    public void AttackEnemy(GameObject enemy)
    {
        enemy.GetComponent<Health>()?.TakeDamage(damage);
        Debug.Log("Dealt " + damage + " damage to " + enemy.name);
    }
}
