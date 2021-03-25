using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateShopInventory : MonoBehaviour
{
    public Button ArmourItemPrefab;
    public Button WeaponItemPrefab;
    [Header("Armour Type Sprites")]
    public Sprite HelmetTypeImage;
    public Sprite ChestTypeImage;
    public Sprite ArmsTypeImage;
    public Sprite BootsTypeImage;
    [Header("Weapon Type Sprites")]
    public Sprite AxeTypeImage;
    public Sprite DaggerTypeImage;
    public Sprite MaceTypeImage;
    public Sprite StaffTypeImage;
    public Sprite SwordTypeImage;

    public List<InventoryItem> ItemsToShow = new List<InventoryItem>();

    public LoadEquippedItem Load;

    public Items ShopInventory;

    [Header("Colors")]
    public Color32 LegendaryColor;
    public Color32 EpicColor;
    public Color32 RareColor;
    public Color32 CommonColor;
    void Start()
    {
        ShowAllArmour();
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
                        ItemUI.ItemTypeImage.sprite = AxeTypeImage;
                        items.ItemUiImage = AxeTypeImage;
                        break;
                    case WeaponItem.Dagger:
                        ItemUI.ItemTypeImage.sprite = DaggerTypeImage;
                        items.ItemUiImage = DaggerTypeImage;
                        break;
                    case WeaponItem.Hammer:
                        ItemUI.ItemTypeImage.sprite = MaceTypeImage;
                        items.ItemUiImage = MaceTypeImage;
                        break;
                    case WeaponItem.Staff:
                        ItemUI.ItemTypeImage.sprite = StaffTypeImage;
                        items.ItemUiImage = StaffTypeImage;
                        break;
                    case WeaponItem.Sword:
                        ItemUI.ItemTypeImage.sprite = SwordTypeImage;
                        items.ItemUiImage = SwordTypeImage;
                        break;
                }
            }
            ItemUI.ReadInfoFromProfile();
        }
    }

    public void BuyAndEquipFromShop()
    {
        ShopInventory.ShopInventoryList.Remove(Load.SelectedItem);
        ItemsToShow.Remove(Load.SelectedItem);

        Load.EquipItem();
    }

    public void BuyFromShop()
    {
        ShopInventory.ShopInventoryList.Remove(Load.SelectedItem);
        ItemsToShow.Remove(Load.SelectedItem);

        GameState.CurrentPlayer.Inventory.Add(Load.SelectedItem);

    }


    public void LeaveTheShop()
    {
        NavigationManager.NavigateTo("Village");
    }


    public void ShowAllArmour()
    {
        ItemsToShow = ShopInventory.GetAllArmourItems();

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyHelmets()
    {
        ItemsToShow = ShopInventory.GetArmourItemsOfType(ArmourItems.Helmet);

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyChests()
    {
        ItemsToShow = ShopInventory.GetArmourItemsOfType(ArmourItems.Chest);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyArms()
    {
        ItemsToShow = ShopInventory.GetArmourItemsOfType(ArmourItems.Arms);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyBoots()
    {
        ItemsToShow = ShopInventory.GetArmourItemsOfType(ArmourItems.Boots);

        ShowItems(ItemsToShow);
    }

    public void ShowAllWeapons()
    {
        ItemsToShow = ShopInventory.GetAllWeaponItems();
        ShowItems(ItemsToShow);
    }

    public void ShowOnlyAxe()
    {
        ItemsToShow = ShopInventory.GetWeaponItemsOfType(WeaponItem.Axe);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyDaggers()
    {
        ItemsToShow = ShopInventory.GetWeaponItemsOfType(WeaponItem.Dagger);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyHammers()
    {
        ItemsToShow = ShopInventory.GetWeaponItemsOfType(WeaponItem.Hammer);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlyStaves()
    {
        ItemsToShow = ShopInventory.GetWeaponItemsOfType(WeaponItem.Staff);

        ShowItems(ItemsToShow);
    }
    public void ShowOnlySwords()
    {
        ItemsToShow = ShopInventory.GetWeaponItemsOfType(WeaponItem.Sword);

        ShowItems(ItemsToShow);
    }
}
