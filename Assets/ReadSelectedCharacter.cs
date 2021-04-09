using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadSelectedCharacter : MonoBehaviour
{
    public PopulateStatList Statlist;
    public PopulateAttackTypeList StrongWith;
    public PopulateAttackTypeList WeakAgainst;
    public ReadEquippedAbilities readEquippedAbilities;

    public void SetProfileForStatlist(Player ProfileToSet)
    {
        Statlist.Profile = ProfileToSet;
        Statlist.populateStat();

        StrongWith.Profile = ProfileToSet;
        StrongWith.PopAttackTypeList();

        WeakAgainst.Profile = ProfileToSet;
        WeakAgainst.PopAttackTypeList();

        readEquippedAbilities.Profile = ProfileToSet;
        readEquippedAbilities.readEquippedAbilities();
    }

}
