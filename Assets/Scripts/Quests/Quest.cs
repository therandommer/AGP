using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class Quest : ScriptableObject
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

}
