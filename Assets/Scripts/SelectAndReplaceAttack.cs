using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAndReplaceAttack : MonoBehaviour
{
    public Abilities SelectedAttack;
    public void SelectAttack(AttackReference attackReference)
    {
        SelectedAttack = attackReference.AbilityReference;
    }

    public void ReplaceEquippedAttack1()
    {
        GameState.CurrentPlayer.EquipedSkills[0] = SelectedAttack;
    }
    public void ReplaceEquippedAttack2()
    {
        GameState.CurrentPlayer.EquipedSkills[1] = SelectedAttack;
    }
    public void ReplaceEquippedAttack3()
    {
        GameState.CurrentPlayer.EquipedSkills[2] = SelectedAttack;
    }
    public void ReplaceEquippedAttack4()
    {
        GameState.CurrentPlayer.EquipedSkills[3] = SelectedAttack;
    }
}
