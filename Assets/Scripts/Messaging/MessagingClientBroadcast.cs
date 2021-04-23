using UnityEngine;

public class MessagingClientBroadcast : MonoBehaviour {
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        MessagingManager.Instance.Broadcast();
    }
    */
    void OnTriggerEnter2D(Collider2D col)
    {
        foreach(Quest quest in GameState.CurrentPlayer.QuestLog)
        {
            if(quest.questType == QuestType.TalkingQuest)
            {
                if(gameObject.GetComponent<Npc>().Name == quest.NpcToTalkTo)
                {
                    quest.increaseAmount();
                }
            }
        }

        MessagingManager.Instance.Broadcast();
    }
}
