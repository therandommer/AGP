using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Abilities : ScriptableObject
{
    public bool Passive;
    public bool Targetable;
    public AbilityTypes AbilityType;
    public string AbilityDescription;
    public Sprite AbilityImage;
    [Tooltip("Animation and SFX attached here")]
    public GameObject CastEffect;
    [Tooltip("If player travels or not")]
    public bool isMelee; //if player travels or not
    public AbilityRange abilityRange;
    public int AbilityCost;
    public int AbilityAmount;
}
