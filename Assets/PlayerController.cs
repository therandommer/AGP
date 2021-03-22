﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public Player PlayerProfile;
    [Header("Stats")]
    private int level = 1;
    public int Level
    {
        get { return level; }
        set { level = value; }
    }
    private int health = 0;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    private int maxHealth = 0;
    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }
    private int strength = 1;
    public int Strength
    {
        get { return strength; }
        set { strength = value; }
    }
    private int magic = 0;
    public int Magic
    {
        get { return magic; }
        set { magic = value; }
    }
    private int defense = 0;
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
    private int speed = 1;
    public int Speed
    {
        get { return speed; }
        set { speed = value; }
    }
    private int damage = 1;
    public int Damage
    {
        get { return damage; }
        set { damage = value; }
    }
    private int armor = 0;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }
    private int noOfAttacks = 1;

    private string Weapon; //Again switch, adds in bonus damage

    private Vector2 Position; //Spawn



    [Header("Profile Details")]
    public List<InventoryItem> Inventory = new List<InventoryItem>();

    public List<InventoryItem> EquippedItems = new List<InventoryItem>();

    public void AddInventoryItem(InventoryItem item)
    {
        Inventory.Add(item);
    }


    public List<InventoryItem> GetItems(bool ShowCardsPlayerDoesNotOwn = false, bool IncludeAllRarities = false, bool IncludeAllArmour = false, bool IncludeAllWeapons = false,
        RarityOptions rarity = RarityOptions.Basic, ArmourItems Armour = ArmourItems.None, WeaponItem Weapon = WeaponItem.None)          
    {
        InventoryItem[] itemArray = Inventory.ToArray();

        Debug.Log("Debug1 " + itemArray.Length);

        var items = from item in itemArray select item;

        if (!ShowCardsPlayerDoesNotOwn)
            items = items.Where(item => ItemCollection.Instance.QuantityOfEachItem[item] > 0);

        if (!IncludeAllRarities)
            items = items.Where(item => item.rarity == rarity);

        if (!IncludeAllArmour)
            items = items.Where(item => item.armourItem == Armour);

        if (!IncludeAllWeapons)
            items = items.Where(item => item.weaponItem == Weapon);

        if (IncludeAllArmour)
            items = items.Where(item => item.armourItem != ArmourItems.None);

        if (IncludeAllWeapons)
            items = items.Where(item => item.weaponItem != WeaponItem.None);

        var returnList = items.ToList<InventoryItem>();
        Debug.Log("Debug1 " + returnList.Count);

        returnList.Sort();

        return returnList;
    }


    public List<InventoryItem> GetItemsWithRarity(RarityOptions rarity)
    {
        return GetItems(true, false, false, false, rarity);
    }

    public List<InventoryItem> GetAllArmourItems()
    {
        return GetItems(false, true, true);
    }
    public List<InventoryItem> GetArmourItemsOfType(ArmourItems ArmourPiece)
    {
        return GetItems(false, true, false, false, RarityOptions.Basic, ArmourPiece);
    }
    public List<InventoryItem> GetAllWeaponItems()
    {
        return GetItems(false, true, false, true);
    }

    public Abilities[] Skills;

    public Abilities[] EquipedSkills;

    public int Money;

    public List<Quest> QuestLog = new List<Quest>();
    void Awake()
    {
        ///Copy across all details, much easier to handle plus better for saving
        Level = PlayerProfile.level;
        Health = PlayerProfile.maxHealth;
        MaxHealth = PlayerProfile.maxHealth;
        Strength = PlayerProfile.strength;
        Magic = PlayerProfile.magic;
        Defense = PlayerProfile.defense;
        Speed = PlayerProfile.speed;
        Damage = PlayerProfile.BonusDamage;
        Armor = PlayerProfile.armor;

        Inventory = PlayerProfile.StartingInventory;
        Skills = PlayerProfile.StartingSkills;
        EquipedSkills = PlayerProfile.StartingSkills;
        Money = PlayerProfile.StartingMoney;
        QuestLog = PlayerProfile.StartingQuestLog;
        EquippedItems = PlayerProfile.StartingEquipment;
    }

    public void TakeDamage(int Amount)
    {
        Health -= Mathf.Clamp((Amount - Armor), 0, int.MaxValue); //Stops the values goes crazy
    }
}
