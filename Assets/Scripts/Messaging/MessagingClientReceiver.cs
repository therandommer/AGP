using UnityEngine;

public class MessagingClientReceiver : MonoBehaviour
{
    Conversation conversation;

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
