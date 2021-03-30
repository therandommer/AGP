using System.Collections.Generic;
using UnityEngine;
public class ReadEquippedAbilities : MonoBehaviour
{

    public Player Profile;

    public List<GameObject> EquipedAbilitiesButtons = new List<GameObject>();


    public void readEquippedAbilities()
    {
        if (Profile == null)
        {
            for (int i = 0; i < EquipedAbilitiesButtons.Count; i++)
            {
                UiReferences CurrentButton = EquipedAbilitiesButtons[i].GetComponent<UiReferences>();

                Debug.Log("Reading " + GameState.CurrentPlayer.Skills[i].name);
                EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackTitle.text = GameState.CurrentPlayer.EquipedSkills[i].name;
                EquipedAbilitiesButtons[i].GetComponent<UiReferences>().AttackType.text = GameState.CurrentPlayer.EquipedSkills[i].AbilityType.ToString();
                EquipedAbilitiesButtons[i].GetComponent<UiReferences>().DamageNumber.text = "DMG: " + GameState.CurrentPlayer.EquipedSkills[i].AbilityAmount.ToString();
                EquipedAbilitiesButtons[i].GetComponent<UiReferences>().StaminaCost.text = "Stamina Cost: " + GameState.CurrentPlayer.EquipedSkills[i].AbilityCost.ToString();
                switch (GameState.CurrentPlayer.EquipedSkills[i].AbilityType)
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
        else
        {
            for (int i = 0; i < EquipedAbilitiesButtons.Count; i++)
            {
                UiReferences CurrentButton = EquipedAbilitiesButtons[i].GetComponent<UiReferences>();

                Debug.Log("Reading " + Profile.StartingSkills[i].name);
                CurrentButton.AttackTitle.text = Profile.StartingSkills[i].name;
                CurrentButton.AttackType.text = Profile.StartingSkills[i].AbilityType.ToString();
                CurrentButton.DamageNumber.text = "DMG: " + Profile.StartingSkills[i].AbilityAmount.ToString();
                CurrentButton.StaminaCost.text = "Stamina Cost: " + Profile.StartingSkills[i].AbilityCost.ToString();
                switch (Profile.StartingSkills[i].AbilityType)
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

}
