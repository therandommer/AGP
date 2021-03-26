using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemCollection : MonoBehaviour
{
    public int DefaultNumberOfBasicItems = 3; // how many cards of basic rarity should a character have by default;

    public Dictionary<InventoryItem, int> QuantityOfEachItem = new Dictionary<InventoryItem, int>();

    private Dictionary<string, InventoryItem> allItemsDictionary = new Dictionary<string, InventoryItem>();
    private InventoryItem[] allItemsArray;

    public static ItemCollection Instance;

    void Awake()
    {
        Instance = this;

        allItemsArray = Resources.LoadAll<InventoryItem>("");

        foreach (InventoryItem II in allItemsArray)
        {
            if (!allItemsDictionary.ContainsKey(II.name))
                allItemsDictionary.Add(II.name, II);
        }
        LoadQuantityOfItems();
    }

    void LoadQuantityOfItems()///Need this for saving as well as tracking whether an item 
    {
        foreach (InventoryItem II in allItemsArray)
        {
            // quantity of basic items should not be affected:
            if (II.rarity == RarityOptions.Basic)
                QuantityOfEachItem.Add(II, DefaultNumberOfBasicItems);
            else if (PlayerPrefs.HasKey("NumberOf" + II.name))
                QuantityOfEachItem.Add(II, PlayerPrefs.GetInt("NumberOf" + II.name));
            else
                QuantityOfEachItem.Add(II, 0);
        }
    }

    public List<InventoryItem> ShowItems(bool ShowCardsPlayerDoesNotOwn = false, bool IncludeAllRarities = false, RarityOptions rarity = RarityOptions.Basic)
    {
        List<InventoryItem> returnList = new List<InventoryItem>();

        return returnList;
    }

    public List<InventoryItem> GetItems(bool ShowCardsPlayerDoesNotOwn = false, bool IncludeAllRarities = false, RarityOptions rarity = RarityOptions.Basic)
    {
        var items = from item in allItemsArray select item;

        if (!ShowCardsPlayerDoesNotOwn)
            items = items.Where(item => QuantityOfEachItem[item] > 0);

        if (!IncludeAllRarities)
            items = items.Where(item => item.rarity == rarity);

        var returnList = items.ToList<InventoryItem>();
        returnList.Sort();
        return returnList;
    }

    public List<InventoryItem> GetItemsWithRarity(RarityOptions rarity)
    {
        return GetItems(false, false, rarity);
    }
}
