using UnityEngine;
using System;

public class WarriorAttack : MonoBehaviour
{
    public float baseDamage = 20;
    public float critChance = 0.2f;
    public float critMultiplier = 2f;

    public event Action<WarriorAttack, GameObject> OnAttack;

    public void AttackEnemy(GameObject enemy)
    {
        OnAttack?.Invoke(this, enemy);
    }

   
}
