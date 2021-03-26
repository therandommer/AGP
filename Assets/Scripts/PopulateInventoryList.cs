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

    public LoadEquippedItem Load;

    [Header("Colors")]
    public Color32 LegendaryColor;
    public Color32 EpicColor;
    public Color32 RareColor;
    public Color32 CommonColor;
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

            Button item = Instantiate(ArmourItemPrefab, Vector3.zero, Quaternion.identity);
            item.transform.parent = transform;
            ItemUIManager ItemUI = item.GetComponent<ItemUIManager>();
            ItemUI.ItemProfile = items;
            item.onClick.AddListener(() => Load.SelectItem(ItemUI.ItemProfile));
            switch (items.rarity)
            {
                case RarityOptions.Common:
                    ItemUI.Title.color = CommonColor;
                    break;
                case RarityOptions.Rare:
                    ItemUI.Title.color = RareColor;
                    break;
                case RarityOptions.Epic:
                    ItemUI.Title.color = EpicColor;
                    break;
                case RarityOptions.Legendary:
                    ItemUI.Title.color = LegendaryColor;
                    break;
            }
            if (items.isArmour)
            {
                switch (items.armourItem)
                {
                    case ArmourItems.Helmet:
                        ItemUI.ItemTypeImage.sprite = HelmetTypeImage;
                        items.ItemUiImage = HelmetTypeImage;
                        break;
                    case ArmourItems.Arms:
                        ItemUI.ItemTypeImage.sprite = ArmsTypeImage;
                        items.ItemUiImage = ArmsTypeImage;
                        break;
                    case ArmourItems.Chest:
                        ItemUI.ItemTypeImage.sprite = ChestTypeImage;
                        items.ItemUiImage = ChestTypeImage;
                        break;
                    case ArmourItems.Boots:
                        ItemUI.ItemTypeImage.sprite = BootsTypeImage;
                        items.ItemUiImage = BootsTypeImage;
                        break;
                }
            }
            else
            {
                switch (items.weaponItem)///Need sprites for this
                {
                    case WeaponItem.Axe:

                        break;
                    case WeaponItem.Dagger:

                        break;
                    case WeaponItem.Hammer:

                        break;
                    case WeaponItem.Staff:

                        break;
                    case WeaponItem.Sword:

                        break;
                }
            }
            ItemUI.ReadInfoFromProfile();
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

    public void ShowAllWeapons()
    {
        ItemsToShow = GameState.CurrentPlayer.GetAllWeaponItems();
        Debug.Log("System found " + ItemsToShow.Count);
        ShowItems(ItemsToShow);
    }

    public void ShowOnlyAxe()
    {
        ItemsToShow = GameState.CurrentPlayer.GetWeaponItemsOfType(WeaponItem.Axe);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyDaggers()
    {
        ItemsToShow = GameState.CurrentPlayer.GetWeaponItemsOfType(WeaponItem.Dagger);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyHammers()
    {
        ItemsToShow = GameState.CurrentPlayer.GetWeaponItemsOfType(WeaponItem.Hammer);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyStaves()
    {
        ItemsToShow = GameState.CurrentPlayer.GetWeaponItemsOfType(WeaponItem.Staff);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlySwords()
    {
        ItemsToShow = GameState.CurrentPlayer.GetWeaponItemsOfType(WeaponItem.Sword);

        ShowItems(ItemsToShow);
    }
}
