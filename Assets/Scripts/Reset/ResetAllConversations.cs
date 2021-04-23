using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAllConversations : MonoBehaviour
{

    Conversation[] AllConversations;
    Quest[] AllQuests;

    void Awake()
    {
        AllConversations = Resources.LoadAll<Conversation>("");

        foreach (Conversation conversation in AllConversations)
        {
            conversation.Reset();
        }

        AllQuests = Resources.LoadAll<Quest>("");

        foreach (Quest quest in AllQuests)
        {
            quest.Reset();
        }
    }


}
