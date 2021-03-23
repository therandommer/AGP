using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatsHolder : MonoBehaviour, IComparable<StatsHolder>
{
    public bool isPlayer;
    [ConditionalHide("isPlayer")]
    public Player PlayerProfile;

    public bool isEnemy;
    [ConditionalHide("isEnemy")]
    public Enemy EnemyProfile;


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

    void Awake()
    {
        if(isPlayer)
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
        }
        else if(isEnemy)
        {
            ///Copy across all details, much easier to handle plus better for saving
            Level = EnemyProfile.level;
            Health = EnemyProfile.maxHealth;
            MaxHealth = EnemyProfile.maxHealth;
            Strength = EnemyProfile.strength;
            Magic = EnemyProfile.magic;
            Defense = EnemyProfile.defense;
            Speed = EnemyProfile.speed;
            Damage = EnemyProfile.BonusDamage;
            Armor = EnemyProfile.armor;
        }
        else
        {
            //Debug.LogError(gameObject.name + " does not have their statsHolder setup correctly");
        }
    }

    public int CompareTo(StatsHolder other)
    {
        if (other.Speed < this.Speed)
        {
            return -1;
        }
        else if (other.Speed > this.Speed)
        {
            return 1;
        }
        else
        {
            // if mana costs are equal sort in alphabetical order
            return name.CompareTo(other.name);
        }
    }

    // Define the is greater than operator.
    public static bool operator >(StatsHolder operand1, StatsHolder operand2)
    {
        return operand1.CompareTo(operand2) == 1;
    }

    // Define the is less than operator.
    public static bool operator <(StatsHolder operand1, StatsHolder operand2)
    {
        return operand1.CompareTo(operand2) == -1;
    }

    // Define the is greater than or equal to operator.
    public static bool operator >=(StatsHolder operand1, StatsHolder operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    // Define the is less than or equal to operator.
    public static bool operator <=(StatsHolder operand1, StatsHolder operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }

}
