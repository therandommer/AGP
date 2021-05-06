using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopStorageHolder : MonoBehaviour
{
    int ArmourItemsToMake = 12;
    int WeaponItemsToMake = 15;
    public ItemGenerator ItemGen;
    //public LoadEquippedItem Load;

    public InventoryItem ItemTemplate;
    public List<InventoryItem> ItemsToShow = new List<InventoryItem>();

    string[] Names = new string[] { "Arnita", "Kristal", "Maryjane", "Minda", "Tanner", "Beaulah", "Myrtle", "Deon", "Reggie", "Jalisa", "Myong", "Denna", "Jayson", "Mafalda" };

    public List<InventoryItem> NorthernShopInventoryList = new List<InventoryItem>();

    public List<InventoryItem> EasternShopInventoryList = new List<InventoryItem>();

    public List<InventoryItem> SouthernShopInventoryList = new List<InventoryItem>();

    public List<InventoryItem> WesternShopInventoryList = new List<InventoryItem>();

    public void GenerateAllShopsInv()
    {
        NorthernShopInventoryList.Clear();
        EasternShopInventoryList.Clear();
        SouthernShopInventoryList.Clear();
        WesternShopInventoryList.Clear();
        GernerateShopInv(NorthernShopInventoryList);
        GernerateShopInv(EasternShopInventoryList);
        GernerateShopInv(SouthernShopInventoryList);
        GernerateShopInv(WesternShopInventoryList);
    }

    public void GernerateShopInv(List<InventoryItem> ShopInventoryList)
    {
        for (int i = 0; i < ArmourItemsToMake; i++)
        {
            InventoryItem newItem = Instantiate(ItemTemplate);
            newItem.name = Names[Random.Range(0, Names.Length)];
            newItem.itemName = newItem.name;
            ItemGen.Item = newItem;
            ShopInventoryList.Add(newItem);
            ItemGen.GenerateRandom(i, true, false);
        }

        for (int i = 0; i < WeaponItemsToMake; i++)
        {
            InventoryItem newItem = Instantiate(ItemTemplate);
            newItem.name = Names[Random.Range(0, Names.Length)];
            newItem.itemName = newItem.name;
            ItemGen.Item = newItem;
            ShopInventoryList.Add(newItem);
            ItemGen.GenerateRandom(i, false, true);
        }

        ShopInventoryList.Sort();
    }

    public List<InventoryItem> GetItems(List<InventoryItem> ShopInventoryList,bool IncludeAllRarities = false, bool IncludeAllArmour = false, bool IncludeAllWeapons = false,
RarityOptions rarity = RarityOptions.Basic, ArmourItems Armour = ArmourItems.None, WeaponItem Weapon = WeaponItem.None)
    {
        InventoryItem[] itemArray = ShopInventoryList.ToArray();
        Debug.Log("IA " + itemArray.Length);
        var items = from item in itemArray select item;

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
        //returnList.OrderByDescending(a => a.isArmour).ThenBy(a => a.InitialEffectAmount);

        return returnList;
    }


    public List<InventoryItem> GetItemsWithRarity(List<InventoryItem> ShopInventoryList, RarityOptions rarity)
    {
        return GetItems(ShopInventoryList, false, false, false, rarity);
    }

    public List<InventoryItem> GetAllArmourItems(List<InventoryItem> ShopInventoryList)
    {
        return GetItems(ShopInventoryList, true, true);
    }
    public List<InventoryItem> GetArmourItemsOfType(List<InventoryItem> ShopInventoryList, ArmourItems ArmourPiece)
    {
        return GetItems(ShopInventoryList, true, false, false, RarityOptions.Basic, ArmourPiece);
    }
    public List<InventoryItem> GetAllWeaponItems(List<InventoryItem> ShopInventoryList)
    {
        return GetItems(ShopInventoryList,true, false, true);
    }

    public List<InventoryItem> GetWeaponItemsOfType(List<InventoryItem> ShopInventoryList, WeaponItem Weapon)
    {
        return GetItems(ShopInventoryList, true, false, false, RarityOptions.Basic, ArmourItems.None, Weapon);
    }

}
