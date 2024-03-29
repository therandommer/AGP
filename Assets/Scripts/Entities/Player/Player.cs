﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerClass
{
    Warrior,
    Mage,
    Rouge,
    Paladin
}
public class Player : Entity
{
    public Sprite PlayerImage;

    public PlayerClass Class;

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

    public List<AbilityTypes> Elements = new List<AbilityTypes>();
    [Header("Rewards for levels ups, -1 level = Element")]
    public Abilities[] LevelRewards;
}


