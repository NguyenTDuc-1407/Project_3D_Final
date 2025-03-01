using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManger : MonoBehaviour
{
    public static ConfigManger instance;
    public ConfigManger()
    {
        instance = this;
    }
    Dictionary<int, ItemConfig> dictItemConfig = new Dictionary<int, ItemConfig>();
    public ItemConfig GetItemConfigById(int id)
    {
        return dictItemConfig[id];
    }
}
