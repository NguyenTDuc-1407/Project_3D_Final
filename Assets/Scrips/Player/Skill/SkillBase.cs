using System.Collections;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    public string skillName;
    public float cooldownTime;
    protected bool isOnCooldown;

    protected Coroutine cooldownCoroutine;

    public virtual void ActivateSkill()
    {
        if (isOnCooldown) return;

        ExecuteSkill();
        StartCooldown();
    }

    protected abstract void ExecuteSkill();

    protected void StartCooldown()
    {
        if (cooldownCoroutine != null)
            StopCoroutine(cooldownCoroutine);
        cooldownCoroutine = StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(cooldownTime);
        isOnCooldown = false;
    }
}
