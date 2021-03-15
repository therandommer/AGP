﻿using System.Collections;
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
        }
    }

}
