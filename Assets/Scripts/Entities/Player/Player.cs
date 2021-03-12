using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public void AddInventoryItem(InventoryItem item)
    {
        Inventory.Add(item);
    }

    public string[] Skills;

    private int money;
    public int Money
    {
        get { return money; }
        set { money = value; }
    }

    public List<Quest> QuestLog = new List<Quest>();

}


