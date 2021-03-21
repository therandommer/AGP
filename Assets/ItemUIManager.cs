using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ItemUIManager : MonoBehaviour
{
    public InventoryItem ItemProfile;

    public TMP_Text Title;
    public TMP_Text Amount;
    public Image ItemImage;
    public Image ItemTypeImage;

    public void ReadInfoFromProfile()
    {
        Title.text = ItemProfile.itemName;
        switch (ItemProfile.InitialEffect)
        {
            case Effect.AddArmour:
                Amount.text = "Armour: " + ItemProfile.InitialEffectAmount;
                break;
            case Effect.AddDamage:
                Amount.text = "Attack: " + ItemProfile.InitialEffectAmount;
                break;
        }
        ItemImage.sprite = ItemProfile.itemImage;
    }
}
