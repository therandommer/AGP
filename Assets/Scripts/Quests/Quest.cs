﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
    public enum QuestStatus
    {
        NotAccepted,
        Accepted,
        Complete
    }

    public enum QuestType
    {
        KillQuest,
        FetchQuest,
        TalkingQuest
    }

[System.Serializable]
public class Quest : ScriptableObject , IComparable<Quest>
{
    public QuestStatus Status;
    public string QuestName;
    public QuestType questType;
    public Sprite QuestUISprite;

    public int questAmountNeeded;
    public int actualAmount;
    [Tooltip("Use only if questType is KillQuest")]
    public EnemyClass EnemyToKill;
    [Tooltip("Use only if questType is FetchQuest")]
    public InventoryItem ItemNeeded;
    public string NpcToTalkTo;
    public Enemy Target;
    public Sprite TargetSprite;

    public int ExperienceReward;//Need this for sorting purposes
    [TextArea(2,3)]
    public string QuestDescription;

    public QuestReward[] Reward;
    int Exp;
    public int ReturnAllExp()
    {
        Exp = 0;
        for (int i = 0; i < Reward.Length; i++)
        {
            if(Reward[i].questReward == global::Reward.Exp)
            {
                Exp += Reward[i].RewardAmount;
            }
        }

        return Exp;
    }
    int Money;
    public int ReturnAllMoney()
    {
        Money = 0;
        for (int i = 0; i < Reward.Length; i++)
        {
            if (Reward[i].questReward == global::Reward.Money)
            {
                Money += Reward[i].RewardAmount;
            }
        }

        return Money;
    }

    public string ReturnNameOfItem()
    {
        for (int i = 0; i < Reward.Length; i++)
        {
            if (Reward[i].questReward == global::Reward.Item)
            {
                return Reward[i].RewardItem.itemName;
            }
        }
        return "";
    }

    public string ReturnNameOfAbility()
    {
        for (int i = 0; i < Reward.Length; i++)
        {
            if (Reward[i].questReward == global::Reward.Ability)
            {
                return Reward[i].RewardAbility.name;
            }
        }
        return "";
    }

    public void Reset()
    {
        actualAmount = 0;
        Status = QuestStatus.Accepted;
    }

    public void increaseAmount()
    {
        actualAmount++;
        if(actualAmount >= questAmountNeeded && Status == QuestStatus.Accepted)
        {
            ShowMessage.Instance.StartCouroutineForMessage("Quest Completed!", this.name + " has been cpmpleted", QuestUISprite, 2f);
            Status = QuestStatus.Complete;
        }
    }
    public int CompareTo(Quest other)
    {
        if (other.ExperienceReward < this.ExperienceReward)
        {
            return -1;
        }
        else if (other.ExperienceReward > this.ExperienceReward)
        {
            return 1;
        }
        else
        {
            // if mana costs are equal sort in alphabetical order
            return name.CompareTo(other.name);
        }
    }

    // Define the is greater than operator.
    public static bool operator >(Quest operand1, Quest operand2)
    {
        return operand1.CompareTo(operand2) == 1;
    }

    // Define the is less than operator.
    public static bool operator <(Quest operand1, Quest operand2)
    {
        return operand1.CompareTo(operand2) == -1;
    }

    // Define the is greater than or equal to operator.
    public static bool operator >=(Quest operand1, Quest operand2)
    {
        return operand1.CompareTo(operand2) >= 0;
    }

    // Define the is less than or equal to operator.
    public static bool operator <=(Quest operand1, Quest operand2)
    {
        return operand1.CompareTo(operand2) <= 0;
    }
}
