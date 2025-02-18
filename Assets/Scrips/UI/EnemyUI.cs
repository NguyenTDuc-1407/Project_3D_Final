using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public Image enBar;
    public void updateBar(int nowEn, int maxEn)
    {
        enBar.fillAmount = (float)nowEn / (float)maxEn;
    }
}
