using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopulateAdditionalEffects : MonoBehaviour
{
    public TMP_Text AdditionalEffectsTextPrefab;
    public void ClearEffectsText()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
    public void ReadAdditionalEffectsFromItem(InventoryItem Item)
    {
        if (Item.AdditionalItemEffects.Length <= 0)
            return;

        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        for (int i = 0; i < Item.AdditionalItemEffects.Length; i++)
        {
            TMP_Text text = Instantiate(AdditionalEffectsTextPrefab, Vector3.zero, Quaternion.identity);
            text.transform.SetParent(transform);

            switch (Item.AdditionalItemEffects[i].itemEffect)
            {
                case Effect.BuffHealth:
                    text.text = "Health +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffStrength:
                    text.text = "Strength +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffMagic:
                    text.text = "Magic +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffDefense:
                    text.text = "Defense +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.BuffSpeed:
                    text.text = "Speed +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.AddArmour:
                    text.text = "Armour +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
                case Effect.AddDamage:
                    text.text = "Damage +" + Item.AdditionalItemEffects[i].EffectAmount;
                    break;
            }
        }
    }
}
