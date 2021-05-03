using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceManager : MonoBehaviour
{
    ///For the decisions
    public Button Button1;
    public TMP_Text Button1Text;
    public Button Button2;
    public TMP_Text Button2Text;
    public Button Button3;
    public TMP_Text Button3Text;
    public Button Button4;
    public TMP_Text Button4Text;

    public ConversationEntry CurrentConversationLine;

    public Conversation Choice1Convo;
    public Conversation Choice1PendingConvo;
    public Conversation Choice1CompleteConvo;
    public Quest Choice1Quest;
    public GameObject Choice1Character;
    public InventoryItem Choice1Item;

    public Conversation Choice2Convo;
    public Conversation Choice2PendingConvo;
    public Conversation Choice2CompleteConvo;
    public Quest Choice2Quest;
    public GameObject Choice2Character;
    public InventoryItem Choice2Item;

    public Conversation Choice3Convo;
    public Conversation Choice3PendingConvo;
    public Conversation Choice3CompleteConvo;
    public Quest Choice3Quest;
    public GameObject Choice3Character;
    public InventoryItem Choice3Item;

    public Conversation Choice4Convo;
    public Conversation Choice4PendingConvo;
    public Conversation Choice4CompleteConvo;
    public Quest Choice4Quest;
    public GameObject Choice4Character;
    public InventoryItem Choice4Item;


    public void ChangeButtonText(ConversationEntry conversationLine)
    {
        CurrentConversationLine = conversationLine;
        if(!conversationLine.Decision1Pending)
        {
            Button1Text.text = conversationLine.Decision1Text;
        }
        else
        {
            Button1Text.text = conversationLine.Quest1PendingButtonText;
        }

        if (conversationLine.Decision1Convo != null)
        {
            Choice1Convo = conversationLine.Decision1Convo;
        }

        if (conversationLine.Quest1PendingConvo != null)
        {
            Choice1PendingConvo = conversationLine.Quest1PendingConvo;
        }

        if (conversationLine.Quest1CompleteConvo != null)
        {
            Choice1CompleteConvo = conversationLine.Quest1CompleteConvo;
        }

        if (conversationLine.Quest1PendingConvo != null)
        {
            Choice1PendingConvo = conversationLine.Quest1PendingConvo;
        }

        if (conversationLine.Character1ToGive != null)
        {
            Choice1Character = conversationLine.Character1ToGive;
        }

        if (conversationLine.Item1ToGive != null)
        {
            Choice1Item = conversationLine.Item1ToGive;
        }

        if (conversationLine.Quest1ToGive != null && !conversationLine.Decision1Pending)
        {
            Choice1Quest = conversationLine.Quest1ToGive;
            Button1.image.color = Color.yellow;
        }



        ///
        Button2Text.text = conversationLine.Decision2Text;
        if (!conversationLine.Decision2Pending)
        {
            Button2Text.text = conversationLine.Decision2Text;
        }
        else
        {
            Button2Text.text = conversationLine.Quest2PendingButtonText;
        }

        if (conversationLine.Decision2Convo != null)
        {
            Choice2Convo = conversationLine.Decision2Convo;
        }

        if (conversationLine.Quest2PendingConvo != null)
        {
            Choice2PendingConvo = conversationLine.Quest2PendingConvo;
        }

        if (conversationLine.Quest2CompleteConvo != null)
        {
            Choice2CompleteConvo = conversationLine.Quest2CompleteConvo;
        }

        if (conversationLine.Quest2PendingConvo != null)
        {
            Choice2PendingConvo = conversationLine.Quest2PendingConvo;
        }

        if (conversationLine.Character2ToGive != null)
        {
            Choice2Character = conversationLine.Character2ToGive;
        }

        if (conversationLine.Item2ToGive != null)
        {
            Choice2Item = conversationLine.Item2ToGive;
        }

        if (conversationLine.Quest2ToGive != null)
        {
            Choice2Quest = conversationLine.Quest2ToGive;
            Button2.image.color = Color.yellow;
        }

        ///

        Button3Text.text = conversationLine.Decision3Text;
        if (!conversationLine.Decision2Pending)
        {
            Button3Text.text = conversationLine.Decision3Text;
        }
        else
        {
            Button3Text.text = conversationLine.Quest3PendingButtonText;
        }

        if (conversationLine.Decision3Convo != null)
        {
            Choice3Convo = conversationLine.Decision3Convo;
        }

        if (conversationLine.Quest3PendingConvo != null)
        {
            Choice3PendingConvo = conversationLine.Quest3PendingConvo;
        }

        if (conversationLine.Quest3CompleteConvo != null)
        {
            Choice3CompleteConvo = conversationLine.Quest3CompleteConvo;
        }

        if (conversationLine.Quest3PendingConvo != null)
        {
            Choice3PendingConvo = conversationLine.Quest3PendingConvo;
        }

        if (conversationLine.Character3ToGive != null)
        {
            Choice3Character = conversationLine.Character3ToGive;
        }

        if (conversationLine.Item3ToGive != null)
        {
            Choice3Item = conversationLine.Item3ToGive;
        }

        if (conversationLine.Quest3ToGive != null)
        {
            Choice3Quest = conversationLine.Quest3ToGive;
            Button3.image.color = Color.yellow;
        }

        ///

        Button4Text.text = conversationLine.Decision4Text;
        if (!conversationLine.Decision4Pending)
        {
            Button4Text.text = conversationLine.Decision4Text;
        }
        else
        {
            Button4Text.text = conversationLine.Quest4PendingButtonText;
        }

        if (conversationLine.Decision4Convo != null)
        {
            Choice4Convo = conversationLine.Decision4Convo;
        }

        if (conversationLine.Quest4PendingConvo != null)
        {
            Choice4PendingConvo = conversationLine.Quest4PendingConvo;
        }

        if (conversationLine.Quest4CompleteConvo != null)
        {
            Choice4CompleteConvo = conversationLine.Quest4CompleteConvo;
        }

        if (conversationLine.Quest4PendingConvo != null)
        {
            Choice4PendingConvo = conversationLine.Quest4PendingConvo;
        }

        if (conversationLine.Character4ToGive != null)
        {
            Choice4Character = conversationLine.Character4ToGive;
        }

        if (conversationLine.Item4ToGive != null)
        {
            Choice4Item = conversationLine.Item4ToGive;
        }

        if (conversationLine.Quest4ToGive != null)
        {
            Choice4Quest = conversationLine.Quest4ToGive;
            Button4.image.color = Color.yellow;
        }
    }

    public void Choice1Diagloge()
    {
        if(CurrentConversationLine.Decision1Pending)
        {
            foreach(Quest quest in GameState.CurrentPlayer.QuestLog)//Find quest given to player
            {
                if(quest == CurrentConversationLine.Quest1ToGive)
                {
                    if (quest.questAmountNeeded <= quest.actualAmount)
                    {
                        Debug.Log("Cliamed");
                        GameState.CurrentPlayer.ClaimQuest(quest);
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice1CompleteConvo);
                    }
                    else
                    {
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice1PendingConvo);
                    }
                    return;
                }
            }
        }

        if (Choice1Character != null)
        {
            GameState.AddToParty(Choice1Character);
        }
        if (Choice1Quest != null)
        {
            CurrentConversationLine.Decision1Pending = true;
            GameState.CurrentPlayer.QuestLog.Add(Choice1Quest);
        }
        if (Choice1Item != null)
        {
            GameState.CurrentPlayer.Inventory.Add(Choice1Item);
        }
        ConversationManager.Instance.talking = false;
        ConversationManager.Instance.wait = false;
        ConversationManager.Instance.choice = false;
        if (Choice1Convo != null)
        {
            Debug.Log("Should start anouther convo");
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice1Convo);
        }
        ResetChoices();
    }
    public void Choice2Diagloge()
    {
        if (CurrentConversationLine.Decision2Pending)
        {
            foreach (Quest quest in GameState.CurrentPlayer.QuestLog)//Find quest given to player
            {
                if (quest == CurrentConversationLine.Quest2ToGive)
                {
                    if (quest.questAmountNeeded <= quest.actualAmount)
                    {
                        Debug.Log("Cliamed");
                        GameState.CurrentPlayer.ClaimQuest(quest);
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice2CompleteConvo);
                    }
                    else
                    {
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice2PendingConvo);
                    }
                    return;
                }
            }
        }
        if (Choice2Character != null)
        {
            GameState.AddToParty(Choice2Character);
        }
        if (Choice2Quest != null)
        {
            GameState.CurrentPlayer.QuestLog.Add(Choice2Quest);
        }
        if (Choice2Item != null)
        {
            GameState.CurrentPlayer.Inventory.Add(Choice2Item);
        }
        ConversationManager.Instance.talking = false;
        ConversationManager.Instance.wait = false;
        ConversationManager.Instance.choice = false;
        if (Choice2Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice2Convo);
        }
        ResetChoices();
    }
    public void Choice3Diagloge()
    {
        if (CurrentConversationLine.Decision3Pending)
        {
            foreach (Quest quest in GameState.CurrentPlayer.QuestLog)//Find quest given to player
            {
                if (quest == CurrentConversationLine.Quest3ToGive)
                {
                    if (quest.questAmountNeeded <= quest.actualAmount)
                    {
                        Debug.Log("Cliamed");
                        GameState.CurrentPlayer.ClaimQuest(quest);
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice3CompleteConvo);
                    }
                    else
                    {
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice3PendingConvo);
                    }
                    return;
                }
            }
        }
        if (Choice3Character != null)
        {
            GameState.AddToParty(Choice3Character);
        }
        if (Choice3Quest != null)
        {
            GameState.CurrentPlayer.QuestLog.Add(Choice3Quest);
        }
        if (Choice3Item != null)
        {
            GameState.CurrentPlayer.Inventory.Add(Choice3Item);
        }
        ConversationManager.Instance.talking = false;
        ConversationManager.Instance.wait = false;
        ConversationManager.Instance.choice = false;
        if (Choice3Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice3Convo);
        }
        ResetChoices();
    }
    public void Choice4Diagloge()
    {
        if (CurrentConversationLine.Decision4Pending)
        {
            foreach (Quest quest in GameState.CurrentPlayer.QuestLog)//Find quest given to player
            {
                if (quest == CurrentConversationLine.Quest4ToGive)
                {
                    if (quest.questAmountNeeded <= quest.actualAmount)
                    {
                        Debug.Log("Cliamed");
                        GameState.CurrentPlayer.ClaimQuest(quest);
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice4CompleteConvo);
                    }
                    else
                    {
                        ConversationManager.Instance.talking = false;
                        ConversationManager.Instance.wait = false;
                        ConversationManager.Instance.choice = false;
                        ConversationManager.Instance.StartConversation(Choice4PendingConvo);
                    }
                    return;
                }
            }
        }
        if (Choice4Character != null)
        {
            GameState.AddToParty(Choice1Character);
        }
        if (Choice4Quest != null)
        {
            GameState.CurrentPlayer.QuestLog.Add(Choice4Quest);
        }
        if (Choice4Item != null)
        {
            GameState.CurrentPlayer.Inventory.Add(Choice4Item);
        }
        ConversationManager.Instance.talking = false;
        ConversationManager.Instance.wait = false;
        ConversationManager.Instance.choice = false;
        if (Choice4Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice4Convo);
        }
        ResetChoices();
    }

    public void ResetChoices()
    {
        Choice1Convo = null;
        Choice2Convo = null;
        Choice3Convo = null;
        Choice4Convo = null;

        Choice1Character = null;
        Choice2Character = null;
        Choice3Character = null;
        Choice4Character = null;

        Choice1Item = null;
        Choice2Item = null;
        Choice3Item = null;
        Choice4Item = null;

        Choice1Quest = null;
        Choice2Quest = null;
        Choice3Quest = null;
        Choice4Quest = null;

        Choice1PendingConvo = null;
        Choice2PendingConvo = null;
        Choice3PendingConvo = null;
        Choice4PendingConvo = null;

        Choice1CompleteConvo = null;
        Choice2CompleteConvo = null;
        Choice3CompleteConvo = null;
        Choice4CompleteConvo = null;

        Button1.image.color = Color.white;
        Button2.image.color = Color.white;
        Button3.image.color = Color.white;
        Button4.image.color = Color.white;
    }

}
