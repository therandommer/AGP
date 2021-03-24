using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{

    public int ItemsToGenerate;

    public ItemGenerator ItemGen;

    public InventoryItem ItemTemplate;

    public List<InventoryItem> List = new List<InventoryItem>();
    string[] Names = new string[] { "Arnita", "Kristal", "Maryjane", "Minda", "Tanner", "Beaulah", "Myrtle", "Deon", "Reggie", "Jalisa", "Myong", "Denna", "Jayson", "Mafalda" };

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < ItemsToGenerate; i++)
        {
            InventoryItem newItem = Instantiate(ItemTemplate);
            newItem.name = Names[Random.Range(1, Names.Length)];
            ItemGen.Item = newItem;
            int Itemtype = Random.Range(0, 1);
            if(Itemtype == 0)
            {
                Debug.Log("Generating " + newItem.name + " as a armour");
                ItemGen.GenerateRandom(1, true, false);
            }
            else
            {
                Debug.Log("Generating " + newItem.name + " as a weapon");

                ItemGen.GenerateRandom(1, false, true);
            }
            List.Add(newItem);
        }
    }
}
