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

    public Conversation Choice1Convo;
    public Quest Choice1Quest;
    public GameObject Choice1Character;
    public InventoryItem Choice1Item;

    public Conversation Choice2Convo;
    public Quest Choice2Quest;
    public GameObject Choice2Character;
    public InventoryItem Choice2Item;

    public Conversation Choice3Convo;
    public Quest Choice3Quest;
    public GameObject Choice3Character;
    public InventoryItem Choice3Item;

    public Conversation Choice4Convo;
    public Quest Choice4Quest;
    public GameObject Choice4Character;
    public InventoryItem Choice4Item;


    public void ChangeButtonText(ConversationEntry conversationLine)
    {
        Button1Text.text = conversationLine.Decision1Text;

        if (conversationLine.Decision1Convo != null)
        {
            Choice1Convo = conversationLine.Decision1Convo;
        }

        if (conversationLine.Character1ToGive != null)
        {
            Choice1Character = conversationLine.Character1ToGive;
        }

        if (conversationLine.Item1ToGive)
        {
            Choice1Item = conversationLine.Item1ToGive;
        }

        if (conversationLine.Quest1ToGive != null)
        {
            Choice1Quest = conversationLine.Quest1ToGive;
            Button1.image.color = Color.yellow;
        }

        Button2Text.text = conversationLine.Decision2Text;
        if (conversationLine.Decision2Convo != null)
        {
            Choice2Convo = conversationLine.Decision2Convo;
        }

        if (conversationLine.Character2ToGive != null)
        {
            Choice2Character = conversationLine.Character2ToGive;
        }

        if (conversationLine.Item2ToGive)
        {
            Choice2Item = conversationLine.Item2ToGive;
        }

        if (conversationLine.Quest2ToGive != null)
        {
            Choice2Quest = conversationLine.Quest2ToGive;
            Button2.image.color = Color.yellow;
        }

        Button3Text.text = conversationLine.Decision3Text;
        if (conversationLine.Decision3Convo != null)
        {
            Choice3Convo = conversationLine.Decision3Convo;
        }

        if (conversationLine.Character3ToGive != null)
        {
            Choice3Character = conversationLine.Character3ToGive;
        }

        if (conversationLine.Item3ToGive)
        {
            Choice3Item = conversationLine.Item3ToGive;
        }

        if (conversationLine.Quest3ToGive != null)
        {
            Choice3Quest = conversationLine.Quest3ToGive;
            Button3.image.color = Color.yellow;
        }

        Button4Text.text = conversationLine.Decision4Text;
        if (conversationLine.Decision4Convo != null)
        {
            Choice4Convo = conversationLine.Decision4Convo;
        }

        if (conversationLine.Character4ToGive != null)
        {
            Choice4Character = conversationLine.Character4ToGive;
        }

        if (conversationLine.Item4ToGive)
        {
            Choice1Item = conversationLine.Item1ToGive;
        }

        if (conversationLine.Quest4ToGive != null)
        {
            Choice4Quest = conversationLine.Quest4ToGive;
            Button4.image.color = Color.yellow;
        }
    }

    public void Choice1Diagloge()
    {
        //StartCoroutine(DisplayConversation(Choice1Convo));
        //StartConversation(Choice1Convo);
        if (Choice1Character != null)
        {
            GameState.AddToParty(Choice1Character);
        }
        if (Choice1Quest != null)
        {
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
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice1Convo);
        }
        ResetChoices();
    }
    public void Choice2Diagloge()
    {
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
    }

}
