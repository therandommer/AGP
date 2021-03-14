using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Quest : ScriptableObject
{
    public string QuestName;
    public enum QuestStatus
    {
        NotAccepted,
        Accepted,
        Complete
    }
    public QuestStatus questStatus;

    public enum QuestType
    {
        KillQuest,
        FetchQuest,
        TalkingQuest
    }
    public QuestType questType;

    public int questAmountNeeded;
    [Tooltip("Use only if questType is KillQuest")]
    public EnemyClass EnemyToKill;
    [Tooltip("Use only if questType is FetchQuest")]
    public InventoryItem ItemNeeded;

    public enum QuestReward
    {
        Money,
        Exp,
        MoneyAndExp
    }
    public QuestReward questReward;
    public int RewardAmount;
}
