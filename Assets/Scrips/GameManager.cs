using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState state;
    [Header("Enemy")]
    public EnemyConfig enemyConfig;

    [Header("Inentory")]
    public ConfigInventory configInventory;
    ItemConfig itemConfig;
    public Player player;
    // public ItemDatas itemData;
    public GameObject inventory;
    // // ConfigManger configManger;
    // public List<ItemDatas> listItemDatas = new List<ItemDatas>();
    InventoryUI inventoryUI;
    bool checkOpenInventory = true;

    [Header("Menu")]
    bool menuState = false;
    public GameObject Menu;
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

        if (inventory == null)
        {
            inventory = GameObject.Find("Inventory");
            inventory.SetActive(false);
        }
        if (Menu == null)
        {
            Menu = GameObject.Find("Setting Menu");
            Menu.SetActive(false);
        }
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
        MenuIsOpen();
    }
    void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            checkOpenInventory = !checkOpenInventory;
            inventory.SetActive(checkOpenInventory);
            
        }
    }
    void MenuIsOpen()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Menu != null)
            {
                menuState = !menuState;
                Menu.SetActive(menuState);
            }else
            {
                if (Menu == null)
                {
                    Menu = GameObject.Find("Setting Menu");
                }
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
    public void TakeDamagePlayer(int damage)
    {
        player.TakeDamage(damage);
    }
    public enum GameState
    {
        start,
        end
    }
}
