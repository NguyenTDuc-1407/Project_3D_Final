using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "item", menuName = "inventory/item")]
public class ItemConfig : ScriptableObject
{
    public int id;
    public string itemName;
    public int value;
    public Itemtype itemtype;
    public Sprite image;
    public bool IsStack()
    {
        switch (itemtype)
        {
            default:
            case Itemtype.Energy:
            case Itemtype.Hp:
                return true;
        }
    }
}
public enum Itemtype
{
    Hp, Energy
}
