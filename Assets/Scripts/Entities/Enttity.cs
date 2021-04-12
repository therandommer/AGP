using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum EntityType
{
    Angel,
    Demon,
    Human,
    Mage
}

public class Entity : ScriptableObject
{
    public string Name;

    public int Age;
    [Header("Used to set initial spawning lcation for player characters")]
    public EntityType Type;
    
    public string Occupation; //Same as above 
    
    public int level = 1;

    public int health = 50;

    public int maxHealth = 100;

    public int strength = 1;

    public int magic = 0;

    public int defense = 0;

    public int speed = 1;

    public int BonusDamage = 1;

    public int armor = 0;

    public int noOfAttacks = 1;
    
    public string Weapon; //Again switch, adds in bonus damage
    
    public Vector2 Position; //Spawn

}