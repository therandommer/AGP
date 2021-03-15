using System.Collections.Generic;
using UnityEngine;
public class ReadEquippedAbilities : MonoBehaviour
{
    public List<GameObject> EquipedAbilitiesButtons = new List<GameObject>();


    public void readEquippedAbilities()
    {
        for (int i = 0; i < EquipedAbilitiesButtons.Count; i++)
        {
            //UiReferences CurrentButton = EquipedAbilitiesButtons[i].GetComponent<UiReferences>();

            Debug.Log("Reading " + GameState.CurrentPlayer.Skills[i].name);
            EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTitle.text = GameState.CurrentPlayer.Skills[i].name;
            EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackType.text = GameState.CurrentPlayer.Skills[i].AbilityType.ToString();
            EquipedAbilitiesButtons[i].GetComponent<UiReferences>().DamageNumber.text = "DMG: " + GameState.CurrentPlayer.Skills[i].AbilityAmount.ToString();
            EquipedAbilitiesButtons[i].GetComponent<UiReferences>().StaminaCost.text = "Stamina Cost: " + GameState.CurrentPlayer.Skills[i].AbilityCost.ToString();
            switch (GameState.CurrentPlayer.Skills[i].AbilityType)
            {
                case AbilityTypes.Normal:
                    break;
                case AbilityTypes.Slashing:
                    EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTypeBackground.color = Color.gray;
                    break;
                case AbilityTypes.Blunt:
                    EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTypeBackground.color = Color.cyan;
                    break;
                case AbilityTypes.Holy:
                    EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTypeBackground.color = Color.yellow;
                    break;
                case AbilityTypes.Dark:
                    EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTypeBackground.color = Color.magenta;
                    break;
                case AbilityTypes.Fire:
                    EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTypeBackground.color = Color.red;
                    break;
                case AbilityTypes.Water:
                    EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTypeBackground.color = Color.blue;
                    break;
            }

        }
    }

}
