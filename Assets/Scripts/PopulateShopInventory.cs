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

    public List<InventoryItem> ShopInventory;

    public bool Shop1;
    public bool Shop2;
    public bool Shop3;
    public bool Shop4;


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
        if(Shop1)
        {
            GameState.ShopStorage.NorthernShopInventoryList.Remove(Load.SelectedItem);
        }
        if (Shop2)
        {
            GameState.ShopStorage.EasternShopInventoryList.Remove(Load.SelectedItem);
        }
        if (Shop3)
        {
            GameState.ShopStorage.SouthernShopInventoryList.Remove(Load.SelectedItem);
        }
        if (Shop4)
        {
            GameState.ShopStorage.WesternShopInventoryList.Remove(Load.SelectedItem);
        }
        ItemsToShow.Remove(Load.SelectedItem);

        Load.EquipItem();
        ShowItems(ItemsToShow);
    }

    public void BuyFromShop()
    {
        if (Shop1)
        {
            GameState.ShopStorage.NorthernShopInventoryList.Remove(Load.SelectedItem);
        }
        if (Shop2)
        {
            GameState.ShopStorage.EasternShopInventoryList.Remove(Load.SelectedItem);
        }
        if (Shop3)
        {
            GameState.ShopStorage.SouthernShopInventoryList.Remove(Load.SelectedItem);
        }
        if (Shop4)
        {
            GameState.ShopStorage.WesternShopInventoryList.Remove(Load.SelectedItem);
        }
        ItemsToShow.Remove(Load.SelectedItem);

        GameState.CurrentPlayer.Inventory.Add(Load.SelectedItem);
        ShowItems(ItemsToShow);
    }


    public void LeaveTheShop()
    {
        NavigationManager.NavigateTo("Village");
    }


    public void ShowAllArmour()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.NorthernShopInventoryList);
        }

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyHelmets()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Helmet);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Helmet);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Helmet);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Helmet);
        }

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyChests()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Chest);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Chest);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Chest);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Chest);
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyArms()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Arms);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Arms);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Arms);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Arms);
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyBoots()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Boots);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Boots);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Boots);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Boots);
        }

        ShowItems(ItemsToShow);
    }

    public void ShowAllWeapons()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.NorthernShopInventoryList);
        }
        ShowItems(ItemsToShow);
    }

    public void ShowOnlyAxe()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Axe);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Axe);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Axe);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Axe);
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyDaggers()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Dagger);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Dagger);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Dagger);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Dagger);
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyHammers()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Hammer);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Hammer);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Hammer);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Hammer);
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyStaves()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Staff);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Staff);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Staff);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Staff);
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlySwords()
    {
        if (Shop1)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Sword);
        }
        if (Shop2)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Sword);
        }
        if (Shop3)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Sword);
        }
        if (Shop4)
        {
            ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Sword);
        }
        ShowItems(ItemsToShow);
    }
}
