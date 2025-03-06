using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] private int maxHP = 100;
    [SerializeField] private int currentHP;
    [SerializeField] private int maxMana = 50;
    [SerializeField] private int currentMana;
    [SerializeField] public int attackDamage = 10;
    [SerializeField] private int defense = 5;
    [SerializeField] public float critChance = 0.2f;
    [SerializeField] public float critMultiplier = 2f;

    [Header("Level & Exp")]
    [SerializeField] private int level = 1;
    [SerializeField] private int exp = 0;
    [SerializeField] private int expToNextLevel = 100;

    [Header("Resources & Items")]
    [SerializeField] private int gold = 0;
    [SerializeField] private List<string> items = new List<string>();

    [Header("Status Effects")]
    [SerializeField] private bool isDead = false;
    [SerializeField] private bool isInvincible = false;
    [SerializeField] private List<string> statusEffects = new List<string>();

    private void Start()
    {
        currentHP = maxHP;
        currentMana = maxMana;
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible || isDead) return;

        int actualDamage = Mathf.Max(damage - defense, 1);
        currentHP -= actualDamage;

        if (currentHP <= 0)
        {
            currentHP = 0;
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHP = Mathf.Min(currentHP + amount, maxHP);
    }

    public void GainExp(int amount)
    {
        exp += amount;
        if (exp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        level++;
        exp = 0;
        expToNextLevel += level * 50;
        maxHP += 10;
        maxMana += 5;
        attackDamage += 2;
        defense += 1;
        currentHP = maxHP;
        currentMana = maxMana;
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player has died!");
        // Thực hiện logic hồi sinh hoặc game over
    }

  
    public int GetCurrentHP() => currentHP;
    public int GetMaxHP() => maxHP;
    public int GetGold() => gold;
    public int GetLevel() => level;
}
