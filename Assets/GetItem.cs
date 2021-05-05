using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetItem : MonoBehaviour
{
    public 
    // Update is called once per frame
    void Update()
    {

    }
    string[] Names = new string[] { "Arnita", "Kristal", "Maryjane", "Minda", "Tanner", "Beaulah", "Myrtle", "Deon", "Reggie", "Jalisa", "Myong", "Denna", "Jayson", "Mafalda" };

    public void GenerateItem()
    {
        InventoryItem newItem = Instantiate(GameState.ShopStorage.ItemTemplate);
        newItem.itemName = Names[Random.Range(0, Names.Length-1)];
        GameState.ShopStorage.ItemGen.Item = newItem;
        int RandomInt = Random.Range(0, 1);

        if(RandomInt == 0)
        {
        GameState.ShopStorage.ItemGen.GenerateRandom(GameState.CurrentPlayer.stats.Level, true, false);
        }
        else
        {
            GameState.ShopStorage.ItemGen.GenerateRandom(GameState.CurrentPlayer.stats.Level, false, true);
        }

        GameState.CurrentPlayer.AddInventoryItem(newItem);

        if (newItem.isArmour)
        {
            ShowMessage.Instance.StartCouroutineForMessage("Gained Armour!", "You gained a " + newItem.armourItem + " piece: " + newItem.itemName, newItem.itemImage, 2f);
        }
        else if (newItem.isWeapon)
        {
            ShowMessage.Instance.StartCouroutineForMessage("Gained Weapon!", "You gained a " + newItem.weaponItem + ": " + newItem.itemName, newItem.itemImage, 2f);
        }
    }
}
