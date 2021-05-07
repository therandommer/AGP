using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagingClientReceiver : MonoBehaviour
{
    public Conversation conversation;

    public List<Conversation> tempConvo = new List<Conversation>();
    public void Check()
    {
        var dialog = GetComponent<ConversationComponent>();
        tempConvo.Clear();

        foreach (Conversation convo in dialog.Conversations)
        {
            if (convo.Skip)
            {
                Debug.Log(convo.name + " has skip");
            }
            else
            {
                tempConvo.Add(convo);
                Debug.Log("adding " + convo.name + " " + tempConvo.Count);
            }
        }
    }
    public void StartConvo()
    {
        conversation = null;
        var dialog = GetComponent<ConversationComponent>();

        if (dialog != null)
        {
            if (tempConvo != null && tempConvo.Count > 0)
            {
                for (int i = 0; i < tempConvo.Count; i++)
                {
                    if (tempConvo[i].Skip == false)
                    {
                        Debug.Log("Going through with " + tempConvo[i].name);
                        conversation = tempConvo[i];
                        ConversationManager.Instance.StartConversation(conversation);
                        tempConvo.Remove(conversation);
                        dialog.Conversations = tempConvo.ToArray();
                        break;
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

}
