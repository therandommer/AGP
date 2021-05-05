using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSelectedCharacter : MonoBehaviour
{
    public PopulateStatList Statlist;
    public PopulateAttackTypeList StrongWith;
    public ReadEquippedAbilities readEquippedAbilities;

    public Player[] Array;
    public GameObject[] ObjectArray;
    public Player CurrentPlayerShown;

    public StartOfGameManager Start;
    public void GoToDetailed()
    {
        CurrentPlayerShown = Array[0];
        SetProfileForStatlist(CurrentPlayerShown);
        Start.SelectCharacter(ObjectArray[0]);
    }

    public void NextChar()
    {
        for (int i = 0; i < Array.Length; i++)
        {
            if (Array[i] == CurrentPlayerShown)
            {
                if ((i + 1) <= Array.Length - 1)
                {
                    CurrentPlayerShown = Array[i+1];
                    SetProfileForStatlist(Array[i + 1]);
                    Start.SelectCharacter(ObjectArray[i + 1]);
                    return;
                }
                else
                {
                    CurrentPlayerShown = Array[0];
                    SetProfileForStatlist(Array[0]);
                    Start.SelectCharacter(ObjectArray[0]);
                    return;
                }
            }
        }
    }

    public void SetProfileForStatlist(Player ProfileToSet)
    {
        Statlist.Profile = ProfileToSet;
        Statlist.populateStat();

        StrongWith.Profile = ProfileToSet;
        StrongWith.PopAttackTypeList();

        readEquippedAbilities.Profile = ProfileToSet;
        readEquippedAbilities.readEquippedAbilities();
    }

}
