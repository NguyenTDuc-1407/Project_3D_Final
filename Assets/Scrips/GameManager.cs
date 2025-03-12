using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    public EnemyConfig enemyConfig;
    public ConfigInventory configInventory;
    ItemConfig itemConfig;
    // public ItemDatas itemData;
    public GameObject inventory;
    // // ConfigManger configManger;
    // public List<ItemDatas> listItemDatas = new List<ItemDatas>();
    InventoryUI inventoryUI;
    bool checkOpenInventory;
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
        inventory.SetActive(false);
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
    private void Update()
    {
        OpenInventory();
    }
    void OpenInventory()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            if (checkOpenInventory == true)
            {
                inventory.SetActive(true);
                checkOpenInventory = false;
            }
            else
            {
                inventory.SetActive(false);
                checkOpenInventory = true;
            }
        }
    }
    public void Add(ItemConfig itemConfig)
    {
        if (ConfigInventory.instance.itemConfigs != null)
        {
            if (itemConfig.IsStack())
            {
                bool itemAlreadyInventory = false;
                foreach (ItemConfig inventoryItem in ConfigInventory.instance.itemConfigs)
                {
                    if (inventoryItem.id == itemConfig.id)
                    {
                        itemConfig.amount += 1;
                        itemAlreadyInventory = true;
                        InventorySlotUI();
                    }
                }
                if (!itemAlreadyInventory)
                {
                    itemConfig.amount += 1;
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
    public void UseItemInventory(ItemConfig itemDatas)
    {
        
        itemDatas.amount -= 1;
        if (itemDatas.amount == 0)
        {
            RemoveItem(itemConfig);
        }
        switch (itemConfig.itemtype)
        {
            case Itemtype.Energy:
                RecoveryItem(itemConfig.value);
                break;
            case Itemtype.Hp:
                RecoveryItem(itemConfig.value);
                break;
        }
    }
    public void RemoveItem(ItemConfig itemDatas)
    {
        ConfigInventory.instance.itemConfigs.Remove(itemDatas);

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
