using UnityEngine;

public class WarriorAttack : MonoBehaviour
{
    public float damage = 20f; // S�t th??ng g?c

    public void AttackEnemy(GameObject enemy)
    {
        enemy.GetComponent<Health>()?.TakeDamage(damage);
        Debug.Log("Dealt " + damage + " damage to " + enemy.name);
    }
}
