using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : ScriptableObject
{
    public string Name;
    public int Age;
    string Faction; //Replace to a list later 
    public string Occupation; //Same as above 
    private int level = 1;
    public int Level
    {
        get {return level; }
        set {level = value; }
    }
    private int health = 50;
    public int Health
    {
        get { return health; }
        set { health = value; }
    }
    private int maxHealth = 100;
    public int MaxHealth
    {
        get {return maxHealth; }
        set {maxHealth = value; }
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


    public void TakeDamage(int Amount)
    {
        Health -= Mathf.Clamp((Amount - Armor), 0, int.MaxValue); //Stops the values goes crazy
    }

    public void Attack(Entity Entity)
    {
        Entity.TakeDamage(Strength);
    }
}