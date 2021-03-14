using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void AddInventoryItem(InventoryItem item)
    {
        Inventory.Add(item);
    }

    public Abilities[] Skills;

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
        Money = PlayerProfile.StartingMoney;
        QuestLog = PlayerProfile.StartingQuestLog;

    }

    public void TakeDamage(int Amount)
    {
        Health -= Mathf.Clamp((Amount - Armor), 0, int.MaxValue); //Stops the values goes crazy
    }
}
