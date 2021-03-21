using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateInventoryList : MonoBehaviour
{
    public Button ArmourItemPrefab;
    public Button WeaponItemPrefab;

    public Sprite HelmetTypeImage;
    public Sprite ChestTypeImage;
    public Sprite ArmsTypeImage;
    public Sprite BootsTypeImage;

    public List<InventoryItem> ItemsToShow = new List<InventoryItem>();

    void Start()
    {
        ItemsToShow = GameState.CurrentPlayer.GetAllArmourItems();

        ShowItems(ItemsToShow);
    }

    public void ShowItems(List<InventoryItem> ItemsToShowList)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (InventoryItem items in ItemsToShow)
        {
            if (items.isArmour)
            {
                Button item = Instantiate(ArmourItemPrefab, Vector3.zero, Quaternion.identity);
                item.transform.parent = transform;
                ItemUIManager ItemUI = item.GetComponent<ItemUIManager>();
                ItemUI.ItemProfile = items;
                switch (items.armourItem)
                {
                    case ArmourItems.Helmet:
                        ItemUI.ItemTypeImage.sprite = HelmetTypeImage;
                        break;
                    case ArmourItems.Arms:
                        ItemUI.ItemTypeImage.sprite = ArmsTypeImage;
                        break;
                    case ArmourItems.Chest:
                        ItemUI.ItemTypeImage.sprite = ChestTypeImage;
                        break;
                    case ArmourItems.Boots:
                        ItemUI.ItemTypeImage.sprite = BootsTypeImage;
                        break;
                }
                ItemUI.ReadInfoFromProfile();
            }
        }
    }

    public void ShowAllArmour()
    {
        ItemsToShow = GameState.CurrentPlayer.GetAllArmourItems();

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyHelmets()
    {
        ItemsToShow = GameState.CurrentPlayer.GetArmourItemsOfType(ArmourItems.Helmet);

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyChests()
    {
        ItemsToShow = GameState.CurrentPlayer.GetArmourItemsOfType(ArmourItems.Chest);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyArms()
    {
        ItemsToShow = GameState.CurrentPlayer.GetArmourItemsOfType(ArmourItems.Arms);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyBoots()
    {
        ItemsToShow = GameState.CurrentPlayer.GetArmourItemsOfType(ArmourItems.Boots);

        ShowItems(ItemsToShow);
    }
}
