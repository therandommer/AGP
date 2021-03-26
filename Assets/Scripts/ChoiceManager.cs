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
    public Conversation Choice2Convo;
    public Quest Choice2Quest;
    public Conversation Choice3Convo;
    public Quest Choice3Quest;
    public Conversation Choice4Convo;
    public Quest Choice4Quest;


    public void ChangeButtonText(ConversationEntry conversationLine)
    {
        Button1Text.text = conversationLine.Decision1Text;
        if (conversationLine.Quest1ToGive != null)
        {
            Choice1Quest = conversationLine.Quest1ToGive;
            Button1.image.color = Color.yellow;
        }
        else
        {
            Choice1Convo = conversationLine.Decision1Convo;
        }

        Button2Text.text = conversationLine.Decision2Text;
        if (conversationLine.Quest2ToGive != null)
        {
            Choice2Quest = conversationLine.Quest2ToGive;
            Button2.image.color = Color.yellow;
        }
        else
        {
            Choice2Convo = conversationLine.Decision2Convo;
        }

        Button3Text.text = conversationLine.Decision3Text;
        if (conversationLine.Quest3ToGive != null)
        {
            Choice3Quest = conversationLine.Quest3ToGive;
            Button3.image.color = Color.yellow;

        }
        else
        {
            Choice3Convo = conversationLine.Decision3Convo;
        }

        Button4Text.text = conversationLine.Decision4Text;
        if (conversationLine.Quest4ToGive != null)
        {
            Choice4Quest = conversationLine.Quest4ToGive;
            Button4.image.color = Color.yellow;
        }
        else
        {
            Choice4Convo = conversationLine.Decision4Convo;
        }
    }

    public void Choice1Diagloge()
    {
        //StartCoroutine(DisplayConversation(Choice1Convo));
        //StartConversation(Choice1Convo);
        if (Choice1Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice1Convo);
        }
        else
        {
            GameState.CurrentPlayer.QuestLog.Add(Choice1Quest);
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
        }
    }
    public void Choice2Diagloge()
    {
        if (Choice2Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice2Convo);
        }
        else
        {
            GameState.CurrentPlayer.QuestLog.Add(Choice2Quest);
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
        }
    }
    public void Choice3Diagloge()
    {
        if (Choice3Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice3Convo);
        }
        else
        {
            GameState.CurrentPlayer.QuestLog.Add(Choice3Quest);
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
        }
    }
    public void Choice4Diagloge()
    {
        if (Choice4Convo != null)
        {
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
            ConversationManager.Instance.StartConversation(Choice4Convo);
        }
        else
        {
            Debug.Log("Add quest");
            GameState.CurrentPlayer.QuestLog.Add(Choice4Quest);
            ConversationManager.Instance.talking = false;
            ConversationManager.Instance.wait = false;
            ConversationManager.Instance.choice = false;
        }
    }

    public void ResetChoices()
    {
        Choice1Convo = null;
        Choice2Convo = null;
        Choice3Convo = null;
        Choice4Convo = null;
        Choice1Quest = null;
        Choice2Quest = null;
        Choice3Quest = null;
        Choice4Quest = null;
    }

}
