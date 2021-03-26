using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Reward
{
    Money,
    Exp,
    Item,
    Ability
}

[System.Serializable]
public class QuestReward
{
    public Reward questReward;
    [Header("If Quest reward is Money or Exp use this")]
    public int RewardAmount;
    [Header("Otherwise fill these out")]
    public InventoryItem RewardItem;

    public Abilities RewardAbility;
}
