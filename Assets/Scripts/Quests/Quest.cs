using System.Collections;
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

    public int questAmountNeeded;
    public int actualAmount;
    [Tooltip("Use only if questType is KillQuest")]
    public EnemyClass EnemyToKill;
    [Tooltip("Use only if questType is FetchQuest")]
    public InventoryItem ItemNeeded;
    [Tooltip("NOT IMPLEMENTED YET! Use only if questType is TalkingQuest")]
    public Npc NpcToTalkTo;
    public Sprite TargetSprite;

    public int ExperienceReward;//Need this for sorting purposes
    [TextArea(2,3)]
    public string QuestDescription;

    public QuestReward[] Reward;

    public void increaseAmount()
    {
        actualAmount++;
        if(actualAmount >= questAmountNeeded)
        {
            if(Status != QuestStatus.Complete)
            {
                Status = QuestStatus.Complete;
                Debug.Log(QuestName + " has been completed");
            }
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
