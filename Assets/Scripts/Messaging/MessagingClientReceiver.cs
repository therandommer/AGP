using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingClientReceiver : MonoBehaviour
{
    Conversation conversation;

    public void StartConvo()
    {
        conversation = null;
        var dialog = GetComponent<ConversationComponent>();
        if (dialog != null)
        {
            if (dialog.Conversations != null && dialog.Conversations.Length > 0)
            {
                for (int i = 0; i < dialog.Conversations.Length; i++)
                {
                    if (dialog.Conversations[i].Skip == false)
                    {
                        conversation = dialog.Conversations[i];
                        ConversationManager.Instance.StartConversation(conversation);
                    }
                    else
                    {
                        Debug.Log(dialog.Conversations[i].name + " has been skipped");
                    }
                }
                if(conversation == null)
                {
                    GameState.CurrentPlayer.GetComponent<PlayerMovement>().CantMove = true;
                    return;
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
