using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public List<InventoryItem> StartingInventory = new List<InventoryItem>();
    public void AddInventoryItem(InventoryItem item)
    {
        StartingInventory.Add(item);
    }

    public Abilities[] StartingSkills;

    public int StartingMoney;

    public List<Quest> StartingQuestLog = new List<Quest>();

}


