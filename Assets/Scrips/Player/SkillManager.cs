using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public SkillBase nextAttackBuffSkill;
    public SkillBase whirlwindSkill;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            whirlwindSkill?.ActivateSkill();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            nextAttackBuffSkill?.ActivateSkill();
        }
    }
}
