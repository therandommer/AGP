﻿using UnityEngine;

public class PopulateAttackTypeList : MonoBehaviour
{
    public GameObject AttackTypePrefab;
    public Player Profile;

    public bool StrongWith;
    public bool WeakAgainst;

    public void PopAttackTypeList()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        if (Profile == null)
        {
            for (int i = 0; i < GameState.CurrentPlayer.stats.PlayerProfile.Elements.Count; i++)
            {
                GameObject attackTypePrefab = Instantiate(AttackTypePrefab, Vector3.zero, Quaternion.identity);
                attackTypePrefab.transform.SetParent(transform);
                UiReferences ButtonUI = attackTypePrefab.GetComponent<UiReferences>();
                if (StrongWith)
                {
                    ButtonUI.AttackType.text = GameState.CurrentPlayer.stats.PlayerProfile.Elements[i].ToString();
                    switch (GameState.CurrentPlayer.stats.PlayerProfile.Elements[i])
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
                else if (WeakAgainst)
                {
                    ButtonUI.AttackType.text = GameState.CurrentPlayer.stats.PlayerProfile.WeakAgainst[i].ToString();
                    switch (GameState.CurrentPlayer.stats.PlayerProfile.WeakAgainst[i])
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
                else
                {
                    Debug.LogError("Populate Attack List not setup correctly");
                }
            }
        }
        else
        {
            for (int i = 0; i < Profile.Elements.Count; i++)
            {
                GameObject attackTypePrefab = Instantiate(AttackTypePrefab, Vector3.zero, Quaternion.identity);

                attackTypePrefab.transform.SetParent(transform);

                UiReferences ButtonUI = attackTypePrefab.GetComponent<UiReferences>();

                ButtonUI.AttackType.text = Profile.Elements[i].ToString();
                switch (Profile.Elements[i])
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
}
