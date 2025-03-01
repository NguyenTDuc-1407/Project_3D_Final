using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUI : MonoBehaviour
{
    ItemDatas itemDatas;
    public void SetItem(ItemDatas itemDatas)
    {
        this.itemDatas = itemDatas;
    }
    public void Remove()
    {
        GameManager.instance.RemoveItem(itemDatas);
        Destroy(this.gameObject);
    }
    public void UseItem()
    {
        GameManager.instance.UseItemInventory(itemDatas);
        GameManager.instance.InventorySlotUI();
    }
}
