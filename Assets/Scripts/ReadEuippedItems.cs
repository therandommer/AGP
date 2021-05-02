using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ReadEuippedItems : MonoBehaviour
{
    [Header("Helmet")]
    public Image HelmetImage;
    public TMP_Text HelmetTitle;
    public TMP_Text HelmetInitalEffect;
    public TMP_Text HelmetHealth;
    public TMP_Text HelmetStrength;
    public TMP_Text HelmetMagic;
    public TMP_Text HelmetDefense;
    public TMP_Text HelmetSpeed;
    public TMP_Text HelmetDamage;
    [Header("Chest")]
    public Image ChestImage;
    public TMP_Text ChestTitle;
    public TMP_Text ChestInitalEffect;
    public TMP_Text ChestHealth;
    public TMP_Text ChestStrength;
    public TMP_Text ChestMagic;
    public TMP_Text ChestDefense;
    public TMP_Text ChestSpeed;
    public TMP_Text ChestDamage;
    [Header("Arms")]
    public Image ArmsImage;
    public TMP_Text ArmsTitle;
    public TMP_Text ArmsInitalEffect;
    public TMP_Text ArmsHealth;
    public TMP_Text ArmsStrength;
    public TMP_Text ArmsMagic;
    public TMP_Text ArmsDefense;
    public TMP_Text ArmsSpeed;
    public TMP_Text ArmsDamage;
    [Header("Boots")]
    public Image BootsImage;
    public TMP_Text BootsTitle;
    public TMP_Text BootsInitalEffect;
    public TMP_Text BootsHealth;
    public TMP_Text BootsStrength;
    public TMP_Text BootsMagic;
    public TMP_Text BootsDefense;
    public TMP_Text BootsSpeed;
    public TMP_Text BootsDamage;
    [Header("Weapon")]
    public Image WeaponImage;
    public TMP_Text WeaponTitle;
    public TMP_Text WeaponInitalEffect;
    public TMP_Text WeaponHealth;
    public TMP_Text WeaponStrength;
    public TMP_Text WeaponMagic;
    public TMP_Text WeaponDefense;
    public TMP_Text WeaponSpeed;
    public TMP_Text WeaponDamage;

    void Start()
    {
        ClearAllEquippedItems();
        PopulateEquippedItems();
    }



    public void PopulateEquippedItems()
    {
        for (int i = 0; i < GameState.CurrentPlayer.EquippedItems.Count; i++)
        {
            InventoryItem IV = GameState.CurrentPlayer.EquippedItems[i];
            if (GameState.CurrentPlayer.EquippedItems[i].isArmour)
            {
                switch (GameState.CurrentPlayer.EquippedItems[i].armourItem)
                {
                    case ArmourItems.Helmet:
                        HelmetImage.sprite = IV.ItemUiImage;
                        HelmetTitle.text = IV.itemName;
                        HelmetInitalEffect.text = "Armour: " + IV.InitialEffectAmount.ToString();
                        for (int j = 0; j < IV.AdditionalItemEffects.Length; j++)
                        {
                            switch (IV.AdditionalItemEffects[j].itemEffect)
                            {
                                case Effect.BuffHealth:
                                    HelmetHealth.text = "Health: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffStrength:
                                    HelmetStrength.text = "Strength: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffMagic:
                                    HelmetMagic.text = "Magic: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffDefense:
                                    HelmetDefense.text = "Defense: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffSpeed:
                                    HelmetSpeed.text = "Speed: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddArmour:
                                    HelmetSpeed.text = "Armour: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddDamage:
                                    HelmetDamage.text = "Damage: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                            }
                        }
                        break;
                    case ArmourItems.Arms:
                        ArmsImage.sprite = IV.ItemUiImage;
                        ArmsTitle.text = IV.itemName;
                        ArmsInitalEffect.text = "Armour: " + IV.InitialEffectAmount.ToString();
                        for (int j = 0; j < IV.AdditionalItemEffects.Length; j++)
                        {
                            switch (IV.AdditionalItemEffects[j].itemEffect)
                            {
                                case Effect.BuffHealth:
                                    ArmsHealth.text = "Health: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffStrength:
                                    ArmsStrength.text = "Strength: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffMagic:
                                    ArmsMagic.text = "Magic: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffDefense:
                                    ArmsDefense.text = "Defense: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffSpeed:
                                    ArmsSpeed.text = "Speed: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddArmour:
                                    ArmsSpeed.text = "Armour: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddDamage:
                                    ArmsDamage.text = "Damage: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                            }
                        }
                        break;
                    case ArmourItems.Chest:
                        ChestImage.sprite = IV.ItemUiImage;
                        ChestTitle.text = IV.itemName;
                        ChestInitalEffect.text = "Armour: " + IV.InitialEffectAmount.ToString();
                        for (int j = 0; j < IV.AdditionalItemEffects.Length; j++)
                        {
                            switch (IV.AdditionalItemEffects[j].itemEffect)
                            {
                                case Effect.BuffHealth:
                                    ChestHealth.text = "Health: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffStrength:
                                    ChestStrength.text = "Strength: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffMagic:
                                    ChestMagic.text = "Magic: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffDefense:
                                    ChestDefense.text = "Defense: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffSpeed:
                                    ChestSpeed.text = "Speed: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddArmour:
                                    ChestSpeed.text = "Armour: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddDamage:
                                    ChestDamage.text = "Damage: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                            }
                        }
                        break;
                    case ArmourItems.Boots:
                        BootsImage.sprite = IV.ItemUiImage;
                        BootsTitle.text = IV.itemName;
                        BootsInitalEffect.text = "Armour: " + IV.InitialEffectAmount.ToString();
                        for (int j = 0; j < IV.AdditionalItemEffects.Length; j++)
                        {
                            switch (IV.AdditionalItemEffects[j].itemEffect)
                            {
                                case Effect.BuffHealth:
                                    BootsHealth.text = "Health: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffStrength:
                                    BootsStrength.text = "Strength: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffMagic:
                                    BootsMagic.text = "Magic: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffDefense:
                                    BootsDefense.text = "Defense: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.BuffSpeed:
                                    BootsSpeed.text = "Speed: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddArmour:
                                    BootsSpeed.text = "Armour: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                                case Effect.AddDamage:
                                    BootsDamage.text = "Damage: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                                    break;
                            }
                        }
                        break;
                }
            }
            else if (GameState.CurrentPlayer.EquippedItems[i].isWeapon)
            {
                WeaponImage.sprite = IV.ItemUiImage;
                WeaponTitle.text = IV.itemName;
                WeaponInitalEffect.text = "Weapon: " + IV.InitialEffectAmount.ToString();
                for (int j = 0; j < IV.AdditionalItemEffects.Length; j++)
                {
                    switch (IV.AdditionalItemEffects[j].itemEffect)
                    {
                        case Effect.BuffHealth:
                            WeaponHealth.text = "Health: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                        case Effect.BuffStrength:
                            WeaponStrength.text = "Strength: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                        case Effect.BuffMagic:
                            WeaponMagic.text = "Magic: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                        case Effect.BuffDefense:
                            WeaponDefense.text = "Defense: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                        case Effect.BuffSpeed:
                            WeaponSpeed.text = "Speed: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                        case Effect.AddArmour:
                            WeaponSpeed.text = "Armour: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                        case Effect.AddDamage:
                            WeaponDamage.text = "Damage: " + IV.AdditionalItemEffects[j].EffectAmount.ToString();
                            break;
                    }
                }
            }
        }

        if (HelmetTitle.text == null)
        {
            HelmetTitle.text = "-----";
        }
        if (ChestTitle.text == null)
        {
            ChestTitle.text = "-----";
        }
        if (ArmsTitle.text == null)
        {
            ArmsTitle.text = "-----";
        }
        if (BootsTitle.text == null)
        {
            BootsTitle.text = "-----";
        }
        if (WeaponTitle.text == null)
        {
            WeaponTitle.text = "-----";
        }
    }

    public void ClearAllEquippedItems()
    {
        HelmetTitle.text = null;
        HelmetInitalEffect.text = null;
        HelmetHealth.text = null;
        HelmetStrength.text = null;
        HelmetMagic.text = null;
        HelmetDefense.text = null;
        HelmetSpeed.text = null;
        HelmetDamage.text = null;

        ChestTitle.text = null;
        ChestInitalEffect.text = null;
        ChestHealth.text = null;
        ChestStrength.text = null;
        ChestMagic.text = null;
        ChestDefense.text = null;
        ChestSpeed.text = null;
        ChestDamage.text = null;

        ArmsTitle.text = null;
        ArmsInitalEffect.text = null;
        ArmsHealth.text = null;
        ArmsStrength.text = null;
        ArmsMagic.text = null;
        ArmsDefense.text = null;
        ArmsSpeed.text = null;
        ArmsDamage.text = null;

        BootsTitle.text = null;
        BootsInitalEffect.text = null;
        BootsHealth.text = null;
        BootsStrength.text = null;
        BootsMagic.text = null;
        BootsDefense.text = null;
        BootsSpeed.text = null;
        BootsDamage.text = null;

        WeaponTitle.text = null;
        WeaponInitalEffect.text = null;
        WeaponHealth.text = null;
        WeaponStrength.text = null;
        WeaponMagic.text = null;
        WeaponDefense.text = null;
        WeaponSpeed.text = null;
        WeaponDamage.text = null;
    }

}
