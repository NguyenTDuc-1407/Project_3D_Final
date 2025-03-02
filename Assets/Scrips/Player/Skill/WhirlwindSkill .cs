using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WhirlwindSkill : SkillBase
{
    public float duration = 3f; // Thời gian xoay kiếm
    public float damage = 10f;
    public float radius = 5f;
    public LayerMask enemyLayer;
    private bool isSpinning = false;
    protected override void ExecuteSkill()
    {
        if (isSpinning)
        {
            return;
        }
        StartCoroutine(WhirlwindAttack());
    }
    private IEnumerator WhirlwindAttack()
    {
        isSpinning = true;
        float timer = 0f;
        while (timer < duration)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, radius, enemyLayer);
            foreach (Collider minion in enemies)
            {
                minion.GetComponent<Minions>()?.DameEnemy((int)((int)damage * Time.deltaTime));
            }
            yield return null;
            timer += Time.deltaTime;

        }
        isSpinning = false;
    }
}
