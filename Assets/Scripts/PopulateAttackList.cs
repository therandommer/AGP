using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateAttackList : MonoBehaviour
{

    public Button attackPrefab;

    [Header("Selection Script for UI")]
    public SelectAndReplaceAttack selectAndReplaceAttack;
    public void PopulateList()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (var Abilities in GameState.CurrentPlayer.Skills)//Need Image, Title, Desc and Show off range
        {
            Debug.Log("Spawn in " + Abilities.name);
            Button AttackPrefab = Instantiate(attackPrefab, Vector3.zero, Quaternion.identity);
            AttackPrefab.transform.parent = transform;
            UiReferences ButtonUI = AttackPrefab.GetComponent<UiReferences>();
            AttackPrefab.GetComponent<AttackReference>().AbilityReference = Abilities;
            AttackPrefab.onClick.AddListener(() => selectAndReplaceAttack.SelectAttack(AttackPrefab.GetComponent<AttackReference>()));
            ButtonUI.AttackTitle.text = Abilities.name;
            ButtonUI.AttackTypeImage.sprite = Abilities.AbilityImage;
            ButtonUI.AttackType.text = Abilities.AbilityType.ToString();
            switch (Abilities.AbilityType)
            {
                case AbilityTypes.Normal:
                    break;
                case AbilityTypes.Slashing:
                    ButtonUI.AttackTypeBackground.color = Color.gray;
                    break;
                case AbilityTypes.Blunt:
                    ButtonUI.AttackTypeBackground.color = Color.cyan;
                    break;
                case AbilityTypes.Holy:
                    ButtonUI.AttackTypeBackground.color = Color.yellow;
                    break;
                case AbilityTypes.Dark:
                    ButtonUI.AttackTypeBackground.color = Color.magenta;
                    break;
                case AbilityTypes.Fire:
                    ButtonUI.AttackTypeBackground.color = Color.red;
                    break;
                case AbilityTypes.Water:
                    ButtonUI.AttackTypeBackground.color = Color.blue;
                    break;
            }
            switch (Abilities.abilityRange)
            {
                case AbilityRange.OneEnemy:
                    ButtonUI.Spot5.enabled = true;
                    break;
                case AbilityRange.AllEnemies:
                    ButtonUI.Spot1.enabled = true;
                    ButtonUI.Spot2.enabled = true;
                    ButtonUI.Spot3.enabled = true;
                    ButtonUI.Spot4.enabled = true;
                    ButtonUI.Spot5.enabled = true;
                    ButtonUI.Spot6.enabled = true;
                    ButtonUI.Spot7.enabled = true;
                    ButtonUI.Spot8.enabled = true;
                    ButtonUI.Spot9.enabled = true;
                    break;
                case AbilityRange.RowOfEnemies:
                    ButtonUI.Spot2.enabled = true;
                    ButtonUI.Spot5.enabled = true;
                    ButtonUI.Spot8.enabled = true;
                    break;
                case AbilityRange.ColumnOfEnemies:
                    ButtonUI.Spot4.enabled = true;
                    ButtonUI.Spot5.enabled = true;
                    ButtonUI.Spot6.enabled = true;
                    break;
            }
        }
    }

}
