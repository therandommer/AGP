using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ConversationManager : Singleton<ConversationManager>
{
    //
    protected ConversationManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    //Is there a converastion going on
    bool talking = false;
    bool choice; //Is there a choice to be made
    public bool wait = false;
    //The current line of text being displayed
    ConversationEntry currentConversationLine;
    /// For the actual Dialog

    public CanvasGroup dialogBox;//the Canvas Group for the dialog box
    public Image imageHolder;//the image holder
    public Text textHolder;//the text holder
    public CanvasGroup choicesCanvas;
    public ChoiceManager choiceManager;

    public Conversation Choice1Convo;
    public Conversation Choice2Convo;
    public Conversation Choice3Convo;
    public Conversation Choice4Convo;

    public void StartConversation(Conversation conversation)
    {
        dialogBox = GameObject.Find("Dialog Box").GetComponent<CanvasGroup>();
        imageHolder = GameObject.Find("Speaker Image").GetComponent<Image>();
        textHolder = GameObject.Find("Dialog Text").GetComponent<Text>();
        choicesCanvas = GameObject.Find("Choices").GetComponent<CanvasGroup>();
        choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();

        //Start displying the supplied conversation
        if (!talking)
        {
            talking = true;
            foreach (var conversationLine in conversation.ConversationLines)
            {
                if (conversationLine.DecisionToBeMade)
                {
                    Choice1Convo = conversationLine.Decision1Convo;
                    Choice2Convo = conversationLine.Decision2Convo;
                    Choice3Convo = conversationLine.Decision3Convo;
                    Choice4Convo = conversationLine.Decision4Convo;
                }
            }
            StartCoroutine(DisplayConversation(conversation));
        }
    }

    IEnumerator DisplayConversation(Conversation conversation)
    {
        Debug.Log("Started Convo");
        foreach (var conversationLine in conversation.ConversationLines)
        {
            currentConversationLine = conversationLine;

            textHolder.text = currentConversationLine.ConversationText;
            imageHolder.sprite = currentConversationLine.DisplayPic;

            if (conversationLine.DecisionToBeMade)
            {
                //Change button text
                choiceManager.ChangeButtonText(currentConversationLine);
                choice = true;
            }
            else
            {
                //yield return new WaitForSeconds(5);
            }

                bool done = false;
                while (!done)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        Debug.Log("Clicked");

                        done = true;
                    }
                    yield return null;
                }
                //yield return StartCoroutine(Wait(KeyCode.Space));
            

        }
        talking = false;
        choice = false;
    }

    void OnGUI()
    {
        if (talking)
        {
            dialogBox.alpha = 1;
            dialogBox.blocksRaycasts = true;
            if (choice)
            {
                choicesCanvas.alpha = 1;
                choicesCanvas.blocksRaycasts = true;
            }
        }
        else
        {
            dialogBox.alpha = 0;
            dialogBox.blocksRaycasts = false;
            choicesCanvas.alpha = 0;
            choicesCanvas.blocksRaycasts = false;
        }
    }

    public void Choice1Diagloge()
    {
        StartCoroutine(DisplayConversation(Choice1Convo));
        //StartConversation(Choice1Convo);
    }
    public void Choice2Diagloge()
    {
        StartCoroutine(DisplayConversation(Choice2Convo));
    }
    public void Choice3Diagloge()
    {
        StartCoroutine(DisplayConversation(Choice3Convo));
    }
    public void Choice4Diagloge()
    {
        StartCoroutine(DisplayConversation(Choice4Convo));
    }
}

