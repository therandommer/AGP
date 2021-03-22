using UnityEngine;
public enum Effect
{
    BuffHealth,
    BuffStrength,
    BuffMagic,
    BuffDefense,
    BuffSpeed,
    GiveImmunity,
    GiveWeakness,
    AddArmour,
    AddDamage
}
public enum InitialEffect
{
    AddArmour,
    AddDamage
}
[System.Serializable]
public class ItemEffect
{
    public Effect itemEffect;

    public int EffectAmount;

    [Header("Will only be used if GiveWeakness or GiveImmunity is selected")]
    public AbilityTypes AbilityImmunity;
}
