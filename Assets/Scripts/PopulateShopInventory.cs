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


    [Header("Colors")]
    public Color32 LegendaryColor;
    public Color32 EpicColor;
    public Color32 RareColor;
    public Color32 CommonColor;
    void Start()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.NorthernShopInventoryList;
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.SouthernShopInventoryList;
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.EasternShopInventoryList;
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.WesternShopInventoryList;
                break;
        }
        ShowAllArmour();
    }

    public void ShowItems(List<InventoryItem> ItemsToShowList)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (InventoryItem items in ItemsToShowList)
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
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                GameState.ShopStorage.NorthernShopInventoryList.Remove(Load.SelectedItem);
                break;
            case PlayerLocation.South:
                GameState.ShopStorage.SouthernShopInventoryList.Remove(Load.SelectedItem);
                break;
            case PlayerLocation.East:
                GameState.ShopStorage.EasternShopInventoryList.Remove(Load.SelectedItem);
                break;
            case PlayerLocation.West:
                GameState.ShopStorage.WesternShopInventoryList.Remove(Load.SelectedItem);
                break;
        }
        ItemsToShow.Remove(Load.SelectedItem);

        Load.EquipItem();
        ShowItems(ItemsToShow);
    }

    public void BuyFromShop()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                GameState.ShopStorage.NorthernShopInventoryList.Remove(Load.SelectedItem);
                break;
            case PlayerLocation.South:
                GameState.ShopStorage.SouthernShopInventoryList.Remove(Load.SelectedItem);
                break;
            case PlayerLocation.East:
                GameState.ShopStorage.EasternShopInventoryList.Remove(Load.SelectedItem);
                break;
            case PlayerLocation.West:
                GameState.ShopStorage.WesternShopInventoryList.Remove(Load.SelectedItem);
                break;
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
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.NorthernShopInventoryList);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.SouthernShopInventoryList);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.EasternShopInventoryList);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetAllArmourItems(GameState.ShopStorage.WesternShopInventoryList);
                break;
        }
        ShowItems(ItemsToShow);
    }

    public void ShowOnlyHelmets()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Helmet);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Helmet);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Helmet);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Helmet);
                break;
        }
        ShowItems(ItemsToShow);
    }

    public void ShowOnlyChests()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Chest);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Chest);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Chest);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Chest);
                break;
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyArms()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Arms);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Arms);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Arms);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Arms);
                break;
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyBoots()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, ArmourItems.Boots);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, ArmourItems.Boots);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.EasternShopInventoryList, ArmourItems.Boots);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetArmourItemsOfType(GameState.ShopStorage.WesternShopInventoryList, ArmourItems.Boots);
                break;
        }
        ShowItems(ItemsToShow);
    }

    public void ShowAllWeapons()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.NorthernShopInventoryList);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.SouthernShopInventoryList);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.EasternShopInventoryList);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetAllWeaponItems(GameState.ShopStorage.WesternShopInventoryList);
                break;
        }

        ShowItems(ItemsToShow);
    }

    public void ShowOnlyAxe()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Axe);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Axe);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Axe);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Axe);
                break;
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyDaggers()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Dagger);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Dagger);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Dagger);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Dagger);
                break;
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyHammers()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Hammer);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Hammer);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Hammer);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Hammer);
                break;
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlyStaves()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Staff);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Staff);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Staff);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Staff);
                break;
        }
        ShowItems(ItemsToShow);
    }
    public void ShowOnlySwords()
    {
        switch (GameState.PlayerLoc)
        {
            case PlayerLocation.North:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.NorthernShopInventoryList, WeaponItem.Sword);
                break;
            case PlayerLocation.South:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.SouthernShopInventoryList, WeaponItem.Sword);
                break;
            case PlayerLocation.East:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.EasternShopInventoryList, WeaponItem.Sword);
                break;
            case PlayerLocation.West:
                ItemsToShow = GameState.ShopStorage.GetWeaponItemsOfType(GameState.ShopStorage.WesternShopInventoryList, WeaponItem.Sword);
                break;
        }
        ShowItems(ItemsToShow);
    }
}
