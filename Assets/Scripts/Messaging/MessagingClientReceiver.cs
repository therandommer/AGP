using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingClientReceiver : MonoBehaviour
{
    Conversation conversation;
    [Header("Use this if you want a fight after a convo")]
    public List<GameObject> EnemiesToFight = new List<GameObject>();

    void Start()
    {
        MessagingManager.Instance.Subscribe(StartConvo);
    }

    void StartConvo()
    {
        Debug.Log("starting convo");
        var dialog = GetComponent<ConversationComponent>();
        if (dialog != null)
        {
            if (dialog.Conversations != null && dialog.Conversations.Length > 0)
            {
                Debug.Log("Convo list is bigger than 0");
                for (int i = 0; i < dialog.Conversations.Length; i++)
                {
                    Debug.Log("Looking for convo with no skip " + dialog.Conversations[i].Skip);
                    if (dialog.Conversations[i].Skip == false)
                    {
                        Debug.Log("Found Convo without skip");
                        conversation = dialog.Conversations[i];
                        ConversationManager.Instance.StartConversation(conversation);
                        ConversationManager.Instance.EnemiesToFight = EnemiesToFight;
                    }
                }
            }
        }
    }

    void OnDestroy()
    {
        if (MessagingManager.Instance != null)
        {
            MessagingManager.Instance.UnSubscribe(StartConvo);
        }
    }

}
