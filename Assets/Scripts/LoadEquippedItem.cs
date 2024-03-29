﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadEquippedItem : MonoBehaviour
{
    ///Find Equipped item in slot based on item selected

    ///ItemProfile is under UiManager of each button

    public InventoryItem SelectedItem;
    [Header("Currently Equipped Item")]
    public CanvasGroup EquippedItemImageCanvas;
    public Image EquippedItemTypeImage;
    public Image EquippedItemImage;
    public TMP_Text EquippedItemName;
    public TMP_Text EquippedInitialItemValue;
    public PopulateAdditionalEffects EquippedAE;

    [Header("Currently Selected Item")]
    public CanvasGroup ItemImageCanvas;
    public Image ItemTypeImage;
    public Image ItemImage;
    public TMP_Text ItemName;
    public TMP_Text InitialItemValue;
    public PopulateAdditionalEffects AE;
    public PopulateStatList PopStat;
    public PopUpMenu Buttons;
    public PopUpMenu EquippedItems;
    public PopUpMenu EquippedInSlot;
    public PopUpMenu BuyItemCanvas;
    public Image BuyItemImage;
    public TMP_Text ItemCost;

    [Header("Stat Difference Manager")]
    public StatsDifferenceManager statsDifference;

    void HideCanvas()
    {
        ItemImageCanvas.alpha = 0;
        EquippedItemImageCanvas.alpha = 0;
    }

    public void ClearSI()
    {
        SelectedItem = null;
    }

    public void SelectItem(InventoryItem selectedItem)
    {
        SelectedItem = selectedItem;
        WipeUiInfo();//Clear previous info
        ReadItemSelected();
        ReadEquippedItemInSlot();
        statsDifference.PreviewStatChange(selectedItem);
        Buttons.EnableTheMenu();
        if(EquippedItems != null)
        {
            PopStat.populateStat();
            EquippedItems.DisableTheMenu();
            EquippedInSlot.EnableTheMenu();
        }
        if(BuyItemCanvas != null)
        {
            BuyItemImage.sprite = selectedItem.itemImage;
            BuyItemCanvas.EnableTheMenu();
            ItemCost.text = selectedItem.shopCost.ToString();
        }
    }

    public void ReadItemInSlot(ArmourItems AI)
    {
        WipeUiInfo();
        for (int i = 0; i < GameState.CurrentPlayer.EquippedItems.Count; i++)
        {
            if (GameState.CurrentPlayer.EquippedItems[i].armourItem == AI)
            {
                InventoryItem II = GameState.CurrentPlayer.EquippedItems[i];
                EquippedItemTypeImage.sprite = II.ItemUiImage;
                EquippedItemImage.sprite = II.itemImage;
                EquippedItemImageCanvas.alpha = 1;
                EquippedItemName.text = II.name;
                EquippedAE.ReadAdditionalEffectsFromItem(II);
                switch (II.InitialEffect)
                {
                    case InitialEffect.AddArmour:
                        EquippedInitialItemValue.text = "Armour: " + II.InitialEffectAmount;
                        break;
                    case InitialEffect.AddDamage:
                        EquippedInitialItemValue.text = "Attack: " + II.InitialEffectAmount;
                        break;
                }
            }
        }
    }
    public void ReadItemInSlot()
    {
        for (int i = 0; i < GameState.CurrentPlayer.EquippedItems.Count; i++)
        {
            if (GameState.CurrentPlayer.EquippedItems[i].isWeapon)
            {
                InventoryItem II = GameState.CurrentPlayer.EquippedItems[i];
                EquippedItemTypeImage.sprite = II.ItemUiImage;
                EquippedItemImage.sprite = II.itemImage;
                EquippedItemImageCanvas.alpha = 1;
                EquippedItemName.text = II.name;
                EquippedAE.ReadAdditionalEffectsFromItem(II);
                switch (II.InitialEffect)
                {
                    case InitialEffect.AddArmour:
                        EquippedInitialItemValue.text = "Armour: " + II.InitialEffectAmount;
                        break;
                    case InitialEffect.AddDamage:
                        EquippedInitialItemValue.text = "Attack: " + II.InitialEffectAmount;
                        break;
                }
            }
        }
    }
    public void ReadHelmetInSlot()
    {
        ReadItemInSlot(ArmourItems.Helmet);
    }
    public void ReadChestInSlot()
    {
        ReadItemInSlot(ArmourItems.Chest);
    }
    public void ReadArmsInSlot()
    {
        ReadItemInSlot(ArmourItems.Arms);
    }
    public void ReadBootsInSlot()
    {
        ReadItemInSlot(ArmourItems.Boots);
    }
    public void ReadWeaponInSlot()
    {
        ReadItemInSlot();
    }

    public void EquipItem()
    {
        for (int i = 0; i < GameState.CurrentPlayer.EquippedItems.Count; i++)
        {
            InventoryItem EquippedItem = GameState.CurrentPlayer.EquippedItems[i];
            if (SelectedItem.armourItem == EquippedItem.armourItem)
            {
                GameState.CurrentPlayer.Inventory.Add(EquippedItem);
                GameState.CurrentPlayer.EquippedItems.Remove(EquippedItem);
            }
        }
        GameState.CurrentPlayer.EquippedItems.Add(SelectedItem);
        GameState.CurrentPlayer.Inventory.Remove(SelectedItem);
        statsDifference.ApplyStats();
        ReadEquippedItemInSlot();
        statsDifference.PreviewStatChange(SelectedItem);
    }
    public void WipeUiInfo()
    {
        HideCanvas();
        AE.ClearEffectsText();
        EquippedAE.ClearEffectsText();
        ItemTypeImage.sprite = null;
        ItemImage.sprite = null;
        ItemName.text = null;
        InitialItemValue.text = null;

        EquippedItemTypeImage.sprite = null;
        EquippedItemImage.sprite = null;
        EquippedItemName.text = null;
        EquippedInitialItemValue.text = null;
    }

    public void ReadItemSelected()
    {
        ItemTypeImage.sprite = SelectedItem.ItemUiImage;
        ItemImage.sprite = SelectedItem.itemImage;
        ItemImageCanvas.alpha = 1;
        ItemName.text = SelectedItem.name;
        switch (SelectedItem.InitialEffect)
        {
            case InitialEffect.AddArmour:
                InitialItemValue.text = "Armour: " + SelectedItem.InitialEffectAmount;
                break;
            case InitialEffect.AddDamage:
                InitialItemValue.text = "Attack: " + SelectedItem.InitialEffectAmount;
                break;
        }
        AE.ReadAdditionalEffectsFromItem(SelectedItem);
    }

    public void ReadEquippedItemInSlot()///Read same Item equipped in same slot
    {
        if (GameState.CurrentPlayer.EquippedItems.Count <= 0)
        {
            Debug.Log("Player does not have an " + SelectedItem.armourItem + " equipped");
            EquippedItemName.text = "-----";
        }

        for (int i = 0; i < GameState.CurrentPlayer.EquippedItems.Count; i++)
        {
            InventoryItem EquippedItem = GameState.CurrentPlayer.EquippedItems[i];
            if (SelectedItem.armourItem == EquippedItem.armourItem || SelectedItem.isWeapon && EquippedItem.isWeapon)
            {
                EquippedItemTypeImage.sprite = EquippedItem.ItemUiImage;
                EquippedItemImage.sprite = EquippedItem.itemImage;
                EquippedItemImageCanvas.alpha = 1;
                EquippedItemName.text = EquippedItem.name;
                switch (SelectedItem.InitialEffect)
                {
                    case InitialEffect.AddArmour:
                        EquippedInitialItemValue.text = "Armour: " + EquippedItem.InitialEffectAmount;
                        break;
                    case InitialEffect.AddDamage:
                        EquippedInitialItemValue.text = "Attack: " + EquippedItem.InitialEffectAmount;
                        break;
                }
                EquippedAE.ReadAdditionalEffectsFromItem(EquippedItem);

            }
            else
            {
                Debug.Log("Player does not have an " + SelectedItem.armourItem + " equipped");
                EquippedItemName.text = "-----";
            }
        }
    }

}
