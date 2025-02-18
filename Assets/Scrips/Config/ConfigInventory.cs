using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigInventory : MonoBehaviour
{
   public static ConfigInventory instance;
    public List<ItemConfig> itemConfigs = new List<ItemConfig>();
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
