using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum RarityOptions
{
    Basic, Common, Rare, Epic, Legendary
}
public enum ArmourItems
{
    None,
    Helmet,
    Arms,
    Chest,
    Boots
}
public enum WeaponItem
{
    None,
    LongSword,
    ShortSword,
    UltraSword,
    Hammer,
    Knife,
    Wand
}
[System.Serializable]
public class InventoryItem : ScriptableObject, IComparable<InventoryItem>
{
    public int ItemLevel;

    public bool isArmour = false;
    [ConditionalHide("isArmour")]
    public ArmourItems armourItem;


    public bool isWeapon = false;
    [ConditionalHide("isWeapon")]
    public WeaponItem weaponItem;

    public RarityOptions rarity;
    public Sprite itemImage;
    public string itemName;
    public int shopCost;
    [Header("Inital effect for Armour or Weapons should be AddArmour or AddDamage respectivly")]
    public Effect InitialEffect;
    public int InitialEffectAmount;
    [Header("Additonal Effects")]
    public ItemEffect[] AdditionalItemEffects;

    public int CompareTo(InventoryItem other)
    {
        if (other.InitialEffectAmount < this.InitialEffectAmount)
        {
            return -1;
        }
        else if (other.InitialEffectAmount > this.InitialEffectAmount)
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
    public static bool operator >(InventoryItem operand1, InventoryItem operand2)
    {
        return operand1.CompareTo(operand2) == 1;
    }

    // Define the is less than operator.
    public static bool operator <(InventoryItem operand1, InventoryItem operand2)
    {
        return operand1.CompareTo(operand2) == -1;
    }

    // Define the is greater than or equal to operator.
    public static bool operator >=(InventoryItem operand1, InventoryItem operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    // Define the is less than or equal to operator.
    public static bool operator <=(InventoryItem operand1, InventoryItem operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }
}

