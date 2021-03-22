using System.Collections.Generic;
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
        Debug.Log("IA " + itemArray.Length);
        var items = from item in itemArray select item;

        //if (!ShowCardsPlayerDoesNotOwn)
            //items = items.Where(item => ItemCollection.Instance.QuantityOfEachItem[item] > 0);

        //if (!IncludeAllRarities)
            //items = items.Where(item => item.rarity == rarity);

        if (!IncludeAllArmour)
            items = items.Where(item => item.armourItem == Armour);

        if (!IncludeAllWeapons)
            items = items.Where(item => item.weaponItem == Weapon);

        if (IncludeAllArmour)
            items = items.Where(item => item.isArmour);

        if (IncludeAllWeapons)
            items = items.Where(item => item.isWeapon);

        var returnList = items.ToList<InventoryItem>();
        Debug.Log("RL " + returnList.Count);

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

    public List<InventoryItem> GetWeaponItemsOfType(WeaponItem Weapon)
    {
        return GetItems(false, true, false, false, RarityOptions.Basic, ArmourItems.None, Weapon);
    }

    public List<Abilities> Skills;

    public List<Abilities> EquipedSkills;

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

        DontDestroyOnLoad(gameObject);

        foreach (Abilities abilities in PlayerProfile.StartingSkills)
        {
            Skills.Add(abilities);
        }
        foreach (Abilities abilities in PlayerProfile.StartingSkills)
        {
            EquipedSkills.Add(abilities);
        }
        Money = PlayerProfile.StartingMoney;

        QuestLog = PlayerProfile.StartingQuestLog;
        foreach (InventoryItem item in PlayerProfile.StartingInventory)
        {
            Inventory.Add(item);
        }
        
        foreach (InventoryItem item in PlayerProfile.StartingEquipment)
        {
            EquippedItems.Add(item);
            ApplyEquipedStats(item);
        }
    }

    public void ApplyEquipedStats(InventoryItem ItemToEquip)
    {
        switch (ItemToEquip.InitialEffect)
        {
            case InitialEffect.AddArmour:
                Armor += ItemToEquip.InitialEffectAmount;
                break;
            case InitialEffect.AddDamage:
                Damage += ItemToEquip.InitialEffectAmount;
                break;
        }

        for (int i = 0; i < ItemToEquip.AdditionalItemEffects.Length; i++)
        {
            switch (ItemToEquip.AdditionalItemEffects[i].itemEffect)
            {
                case Effect.BuffHealth:
                    Health += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffStrength:
                    Strength += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffMagic:
                    Magic += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffDefense:
                    Defense += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffSpeed:
                    Speed += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.GiveImmunity:
                    break;
                case Effect.GiveWeakness:
                    break;
                case Effect.AddArmour:
                    Armor += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.AddDamage:
                    Damage += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
            }
        }
    }

    public void TakeDamage(int Amount)
    {
        Health -= Mathf.Clamp((Amount - Armor), 0, int.MaxValue); //Stops the values goes crazy
    }
}
