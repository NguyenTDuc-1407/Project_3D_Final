using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PosMinions : MonoBehaviour
{
    public List<GameObject> minions;
    public Transform minionsPos;
    private float timeBtwMinions;
    public float timeMinions = 0.2f;
    int Countenemy = 1;
    void Update()
    {
        timeBtwMinions -= Time.deltaTime;
        if (timeBtwMinions < 0 && Countenemy <= minions.Count)
        {
            autoEnemy();
        }
    }
    void autoEnemy()
    {
        timeBtwMinions = timeMinions;
        for (int i = 0; i < minions.Count; i++)
        {
            Instantiate(minions[i], minionsPos.position, Quaternion.identity);
            Countenemy++;
        }
    }
}
