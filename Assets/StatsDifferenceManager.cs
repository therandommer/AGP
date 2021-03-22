using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StatsDifferenceManager : MonoBehaviour
{
    public Sprite IncreaseImage;
    public Sprite DecreaseImage;

    public TMP_Text HealthStatDifferenceText;
    public int HealthStatDifference;
    public Image HealthStatDifferenceImage;
    public TMP_Text StrengthStatDifferenceText;
    public int StrengthStatDifference;
    public Image StrengthStatDifferenceImage;
    public TMP_Text MagicStatDifferenceText;
    public int MagicStatDifference;
    public Image MagicStatDifferenceImage;
    public TMP_Text DefenseStatDifferenceText;
    public int DefenseStatDifference;
    public Image DefenseStatDifferenceImage;
    public TMP_Text SpeedStatDifferenceText;
    public int SpeedStatDifference;
    public Image SpeedStatDifferenceImage;
    public TMP_Text ArmourStatDifferenceText;
    public int ArmourStatDifference;
    public Image ArmourStatDifferenceImage;
    public TMP_Text DamageStatDifferenceText;
    public int DamageStatDifference;
    public Image DamageStatDifferenceImage;

    public PopulateStatList StatList;

    public void ResetColours()
    {
        HealthStatDifferenceText.color = Color.white;
        StrengthStatDifferenceText.color = Color.white;
        MagicStatDifferenceText.color = Color.white;
        DefenseStatDifferenceText.color = Color.white;
        SpeedStatDifferenceText.color = Color.white;
        ArmourStatDifferenceText.color = Color.white;
        DamageStatDifferenceText.color = Color.white;
    }

    public void ResetInts()
    {
        HealthStatDifference = 0;
        StrengthStatDifference = 0;
        MagicStatDifference = 0;
        DefenseStatDifference = 0;
        SpeedStatDifference = 0;
        ArmourStatDifference = 0;
        DamageStatDifference = 0;
    }

    public void ApplyStats()
    {
        PlayerController player = GameState.CurrentPlayer;

        player.Health += HealthStatDifference;
        player.Strength += StrengthStatDifference;
        player.Magic += MagicStatDifference;
        player.Defense += DefenseStatDifference;
        player.Speed += SpeedStatDifference;
        player.Armor += ArmourStatDifference;
        player.Damage += DamageStatDifference;
        StatList.populateStat();
    }

    public void PreviewStatChange(InventoryItem ItemToEquip)///Look at item supplied, add in stat to chached and display difference
    {
        ResetColours();
        ResetInts();
        for (int i = 0; i < GameState.CurrentPlayer.EquippedItems.Count; i++) ///Find if the player has something similar already equipped
        {
            InventoryItem EquippedItem = GameState.CurrentPlayer.EquippedItems[i];
            if (ItemToEquip.armourItem == EquippedItem.armourItem)
            {
                UnequipCurrentItem(EquippedItem);
            }
        }
        EquipItem(ItemToEquip);

        HealthStatDifferenceText.text = HealthStatDifference.ToString();
        if(HealthStatDifference > 0)
        {
            HealthStatDifferenceImage.enabled = true;
            HealthStatDifferenceImage.sprite = IncreaseImage;
            HealthStatDifferenceText.color = Color.green;
        }
        else if (HealthStatDifference < 0)
        {
            HealthStatDifferenceImage.enabled = true;
            HealthStatDifferenceImage.sprite = DecreaseImage;
            HealthStatDifferenceText.color = Color.red;
        }
        else
            HealthStatDifferenceImage.enabled = false;

        StrengthStatDifferenceText.text = StrengthStatDifference.ToString();
        if (StrengthStatDifference > 0)
        {
            StrengthStatDifferenceImage.enabled = true;
            StrengthStatDifferenceImage.sprite = IncreaseImage;
            StrengthStatDifferenceText.color = Color.green;
        }
        else if (StrengthStatDifference < 0)
        {
            StrengthStatDifferenceImage.enabled = true;
            StrengthStatDifferenceImage.sprite = DecreaseImage;
            StrengthStatDifferenceText.color = Color.red;
        }
        else
            StrengthStatDifferenceImage.enabled = false;

        MagicStatDifferenceText.text = MagicStatDifference.ToString();
        if (MagicStatDifference > 0)
        {
            MagicStatDifferenceImage.enabled = true;
            MagicStatDifferenceImage.sprite = IncreaseImage;
            MagicStatDifferenceText.color = Color.green;
        }
        else if (MagicStatDifference < 0)
        {
            MagicStatDifferenceImage.enabled = true;
            MagicStatDifferenceImage.sprite = DecreaseImage;
            MagicStatDifferenceText.color = Color.red;
        }
        else
            MagicStatDifferenceImage.enabled = false;

        DefenseStatDifferenceText.text = DefenseStatDifference.ToString();
        if (DefenseStatDifference > 0)
        {
            DefenseStatDifferenceImage.enabled = true;
            DefenseStatDifferenceImage.sprite = IncreaseImage;
            DefenseStatDifferenceText.color = Color.green;
        }
        else if (DefenseStatDifference < 0)
        {
            DefenseStatDifferenceImage.enabled = true;
            DefenseStatDifferenceImage.sprite = DecreaseImage;
            DefenseStatDifferenceText.color = Color.red;
        }
        else
            DefenseStatDifferenceImage.enabled = false;

        SpeedStatDifferenceText.text = SpeedStatDifference.ToString();
        if (SpeedStatDifference > 0)
        {
            SpeedStatDifferenceImage.enabled = true;
            SpeedStatDifferenceImage.sprite = IncreaseImage;
            SpeedStatDifferenceText.color = Color.green;
        }
        else if (SpeedStatDifference < 0)
        {
            SpeedStatDifferenceText.color = Color.red;
            SpeedStatDifferenceImage.enabled = true;
            SpeedStatDifferenceImage.sprite = DecreaseImage;
        }
        else
            SpeedStatDifferenceImage.enabled = false;

        ArmourStatDifferenceText.text = ArmourStatDifference.ToString();
        if (ArmourStatDifference > 0)
        {
            ArmourStatDifferenceImage.enabled = true;
            ArmourStatDifferenceImage.sprite = IncreaseImage;
            ArmourStatDifferenceText.color = Color.green;
        }
        else if(ArmourStatDifference < 0)
        {
            ArmourStatDifferenceText.color = Color.red;
            ArmourStatDifferenceImage.enabled = true;
            ArmourStatDifferenceImage.sprite = DecreaseImage;
        }
        else
            ArmourStatDifferenceImage.enabled = false;

        DamageStatDifferenceText.text = DamageStatDifference.ToString();
        if (DamageStatDifference > 0)
        {
            DamageStatDifferenceImage.enabled = true;
            DamageStatDifferenceImage.sprite = IncreaseImage;
            DamageStatDifferenceText.color = Color.green;
        }
        else if (DamageStatDifference < 0)
        {
            DamageStatDifferenceText.color = Color.red;
            DamageStatDifferenceImage.enabled = true;
            DamageStatDifferenceImage.sprite = DecreaseImage;
        }
        else
            DamageStatDifferenceImage.enabled = false;

    }

    public void UnequipCurrentItem(InventoryItem ItemToUnequip)
    {
        switch (ItemToUnequip.InitialEffect)
        {
            case InitialEffect.AddArmour:
                ArmourStatDifference -= ItemToUnequip.InitialEffectAmount;
                break;
            case InitialEffect.AddDamage:
                DamageStatDifference -= ItemToUnequip.InitialEffectAmount;
                break;
        }

        for(int i = 0; i < ItemToUnequip.AdditionalItemEffects.Length; i++)
        {
            switch (ItemToUnequip.AdditionalItemEffects[i].itemEffect)
            {
                case Effect.BuffHealth:
                    HealthStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffStrength:
                    StrengthStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffMagic:
                    MagicStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffDefense:
                    DefenseStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffSpeed:
                    SpeedStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.GiveImmunity:
                    break;
                case Effect.GiveWeakness:
                    break;
                case Effect.AddArmour:
                    ArmourStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.AddDamage:
                    DamageStatDifference -= ItemToUnequip.AdditionalItemEffects[i].EffectAmount;
                    break;
            }
        }

    }

    public void EquipItem(InventoryItem ItemToEquip)
    {
        switch (ItemToEquip.InitialEffect)
        {
            case InitialEffect.AddArmour:
                ArmourStatDifference += ItemToEquip.InitialEffectAmount;
                break;
            case InitialEffect.AddDamage:
                ArmourStatDifference += ItemToEquip.InitialEffectAmount;
                break;
        }

        for (int i = 0; i < ItemToEquip.AdditionalItemEffects.Length; i++)
        {
            switch (ItemToEquip.AdditionalItemEffects[i].itemEffect)
            {
                case Effect.BuffHealth:
                    HealthStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffStrength:
                    StrengthStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffMagic:
                    MagicStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffDefense:
                    DefenseStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffSpeed:
                    SpeedStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.GiveImmunity:
                    break;
                case Effect.GiveWeakness:
                    break;
                case Effect.AddArmour:
                    ArmourStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.AddDamage:
                    DamageStatDifference += ItemToEquip.AdditionalItemEffects[i].EffectAmount;
                    break;
            }
        }
    }

}
