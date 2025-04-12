using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMage : MonoBehaviour
{
    [Header("Stats")]
    public int Hp = 100;
    public int Mana = 100;
    public float RageAttack;
    public float timecountDown;
    [Header("For Skill")]
    public int summonMax;
    public float summonRage;
    
    public enum EnemyState {idel, combat }
}
