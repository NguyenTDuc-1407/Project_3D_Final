using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public EnemyConfig enemyConfig;
    public ItemDatas itemData;
    ConfigManger configManger;
    public List<ItemDatas> listItemDatas = new List<ItemDatas>();
    public InventoryUI inventoryUI;
    public static event Action<GameState> OnGameStateChanged;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        UpdateGameState(GameState.start);
    }
    public void UpdateGameState(GameState newState)
    {
        state = newState;
        switch (newState)
        {
            case GameState.start:
                break;
            case GameState.end:
                break;

        }
        OnGameStateChanged?.Invoke(newState);
    }
    public void Add(ItemConfig itemConfig)
    {
        if (ConfigInventory.instance.itemConfigs != null)
        {
            itemData.id = 1;
            configManger.GetItemConfigById(itemData.id);
            Debug.Log(GameManager.instance.itemData.id);
            if (itemConfig.IsStack())
            {
                bool itemAlreadyInventory = false;
                foreach (ItemConfig inventoryItem in ConfigInventory.instance.itemConfigs)
                {
                    if (inventoryItem.id == itemConfig.id)
                    {
                        // GameManager.instance.itemData.amount += 1;
                        itemAlreadyInventory = true;
                        InventorySlotUI();
                    }
                }
                if (!itemAlreadyInventory)
                {
                    // GameManager.instance.itemData.amount += 1;
                    ConfigInventory.instance.itemConfigs.Add(itemConfig);
                    InventorySlotUI();
                }
            }
            else
            {
                ConfigInventory.instance.itemConfigs.Add(itemConfig);
                InventorySlotUI();
            }
        }
    }
    public void RecoveryItem(int itemRecovery)
    {
        // player.RecoveryEnergyItem(itemRecovery);
    }
    public void UseItemInventory(ItemDatas itemDatas)
    {
        itemDatas.id = configManger.GetItemConfigById(itemDatas.id).id;
        itemDatas.amount -= 1;
        if (itemDatas.amount == 0)
        {
            RemoveItem(itemDatas);
        }
        switch (configManger.GetItemConfigById(itemDatas.id).itemtype)
        {
            case Itemtype.Energy:
                RecoveryItem(configManger.GetItemConfigById(itemDatas.id).value);
                break;
            case Itemtype.Hp:
                RecoveryItem(configManger.GetItemConfigById(itemDatas.id).value);
                break;
        }
    }
    public void RemoveItem(ItemDatas itemDatas)
    {
        listItemDatas.Remove(itemDatas);

    }
    public void InventorySlotUI()
    {
        inventoryUI.DisplayInventory();
    }

    public enum GameState
    {
        start,
        end
    }
}
