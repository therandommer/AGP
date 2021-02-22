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
    public AbilityRange abilityRange;
    public Sprite AbilityRangeImage;
    public int AbilityCost;
    public int AbilityAmount;
    
}
