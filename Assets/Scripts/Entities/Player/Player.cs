using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public List<InventoryItem> Inventory = new List<InventoryItem>();
    public string[] Skills;
    public int Money;

    public void AddInventoryItem(InventoryItem item)
    {
        Inventory.Add(item);
    }

}


