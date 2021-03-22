using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public Sprite PlayerImage;

    public List<InventoryItem> StartingInventory = new List<InventoryItem>();
    public void AddInventoryItem(InventoryItem item)
    {
        StartingInventory.Add(item);
    }
    public List<InventoryItem> StartingEquipment = new List<InventoryItem>();

    public Abilities[] StartingSkills;

    public int StartingMoney;

    public List<Quest> StartingQuestLog = new List<Quest>();


    public List<AbilityTypes> StrongWith = new List<AbilityTypes>();

    public List<AbilityTypes> WeakAgainst = new List<AbilityTypes>();

}


