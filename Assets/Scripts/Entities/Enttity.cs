using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : ScriptableObject
{
    public string Name;
    public int Age;
    string Faction; //Replace to a list later 
    public string Occupation; //Same as above 
    public int Level = 1;
    public int Health = 2;
    public int Strength = 1;
    public int Magic = 0;
    public int Defense = 0;
    public int Speed = 1;
    public int Damage = 1;
    public int Armor = 0;
    public int NoOfAttacks = 1;
    public string Weapon; //Again switch, adds in bonus damage
    public Vector2 Position; //Spawn


    public void TakeDamage(int Amount)
    {
        Health -= Mathf.Clamp((Amount - Armor), 0, int.MaxValue); //Stops the values goes crazy
    }

    public void Attack(Entity Entity)
    {
        Entity.TakeDamage(Strength);
    }
}