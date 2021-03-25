using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Items : MonoBehaviour
{

    public int NumItemsToGenerate;
    int ArmourItemsToMake = 12;
    int WeaponItemsToMake = 15;
    public ItemGenerator ItemGen;
    public LoadEquippedItem Load;

    public InventoryItem ItemTemplate;
    public List<InventoryItem> ItemsToShow = new List<InventoryItem>();

    public List<InventoryItem> ShopInventoryList = new List<InventoryItem>();
    string[] Names = new string[] { "Arnita", "Kristal", "Maryjane", "Minda", "Tanner", "Beaulah", "Myrtle", "Deon", "Reggie", "Jalisa", "Myong", "Denna", "Jayson", "Mafalda" };

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < ArmourItemsToMake; i++)
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

        //ShopInventoryList.OrderByDescending(a => a.isArmour).ThenBy(a => a.InitialEffectAmount);
        ShopInventoryList.Sort();

    }

    public List<InventoryItem> GetItems(bool IncludeAllRarities = false, bool IncludeAllArmour = false, bool IncludeAllWeapons = false,
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


    public List<InventoryItem> GetItemsWithRarity(RarityOptions rarity)
    {
        return GetItems(false, false, false, rarity);
    }

    public List<InventoryItem> GetAllArmourItems()
    {
        return GetItems(true, true);
    }
    public List<InventoryItem> GetArmourItemsOfType(ArmourItems ArmourPiece)
    {
        return GetItems(true, false, false, RarityOptions.Basic, ArmourPiece);
    }
    public List<InventoryItem> GetAllWeaponItems()
    {
        return GetItems(true, false, true);
    }

    public List<InventoryItem> GetWeaponItemsOfType(WeaponItem Weapon)
    {
        return GetItems(true, false, false, RarityOptions.Basic, ArmourItems.None, Weapon);
    }

}
