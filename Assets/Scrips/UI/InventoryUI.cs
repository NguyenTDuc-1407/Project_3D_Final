using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParentUI;
    public GameObject inventorySlotUI;
    public void DisplayInventory()
    {
        foreach (Transform itemUI in itemsParentUI)
        {
            Destroy(itemUI.gameObject);
        }
        foreach (ItemDatas itemDatas in GameManager.instance.listItemDatas)
        {
            GameObject itemNewSlot = Instantiate(inventorySlotUI, itemsParentUI);
            var countItemUI = itemNewSlot.transform.Find("CountItem").GetComponent<TextMeshProUGUI>();
            countItemUI.text = itemDatas.amount.ToString();
            var itemImageUI = itemNewSlot.transform.Find("ImageItem").GetComponent<Image>();
            itemImageUI.sprite = ConfigManger.instance.GetItemConfigById(itemDatas.id).image;
            itemNewSlot.GetComponent<ItemUI>().SetItem(itemDatas);
        }
    }
}
