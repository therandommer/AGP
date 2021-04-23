using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class ConversationManager : Singleton<ConversationManager>
{
    //
    protected ConversationManager() { } // guarantee this will be always a singleton only - can't use the constructor!

    //Is there a converastion going on
    public bool talking = false;
    public bool choice; //Is there a choice to be made
    public bool wait = false;
    //The current line of text being displayed
    ConversationEntry currentConversationLine;
    /// For the actual Dialog

    public CanvasGroup dialogBox;//the Canvas Group for the dialog box
    public Image imageHolder;//the image holder
    public Text textHolder;//the text holder
    public CanvasGroup choicesCanvas;
    public ChoiceManager choiceManager;



    public void StartConversation(Conversation conversation)
    {
        dialogBox = GameObject.Find("Dialog Box").GetComponent<CanvasGroup>();
        imageHolder = GameObject.Find("Speaker Image").GetComponent<Image>();
        textHolder = GameObject.Find("Dialog Text").GetComponent<Text>();
        choicesCanvas = GameObject.Find("Choices").GetComponent<CanvasGroup>();
        choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        Debug.Log("trying to start " + conversation.name);
        //Start displying the supplied conversation
        if (!talking)
        {
            talking = true;
            Debug.Log("Starting " + conversation.name + " " + talking);

            StartCoroutine(DisplayConversation(conversation));
        }
    }

    IEnumerator DisplayConversation(Conversation conversation)
    {
        //foreach (var conversationLine in conversation.ConversationLines)
        for (int i = 0; i < conversation.ConversationLines.Length; i++)
        {
            Debug.Log("showing line " + i);
            currentConversationLine = conversation.ConversationLines[i];

            textHolder.text = currentConversationLine.ConversationText;
            imageHolder.sprite = currentConversationLine.DisplayPic;

            if (currentConversationLine.DecisionToBeMade)
            {
                //Change button text
                choiceManager.ChangeButtonText(currentConversationLine);
                choice = true;
                wait = true;
                while (wait)
                {
                    yield return null;
                }
            }
            else
            {
                //yield return new WaitForSeconds(5);
            }

            if (!wait)
            {
                bool done = false;
                while (!done)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        Debug.Log("Clicked");

                        done = true;
                    }
                    yield return null;
                }
            }

        }
        if (talking)
        {
            talking = false;
            choice = false;
            wait = false;
        }
    }

    void OnGUI()
    {
        if (dialogBox != null)
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
                else
                {
                    choicesCanvas.alpha = 0;
                    choicesCanvas.blocksRaycasts = false;
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
    }
}

