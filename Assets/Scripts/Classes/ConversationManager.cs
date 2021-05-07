using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

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
    public TMP_Text textHolder;//the text holder
    public TMP_Text nameHolder;//the text holder
    public Image mainImageHolder;//the image holder
    public CanvasGroup choicesCanvas;
    public ChoiceManager choiceManager;


    public void StartConversation(Conversation conversation)
    {
        dialogBox = GameObject.Find("Dialog Box").GetComponent<CanvasGroup>();
        imageHolder = GameObject.Find("Speaker Image").GetComponent<Image>();
        mainImageHolder = GameObject.Find("MainDisplayImage").GetComponent<Image>();
        textHolder = GameObject.Find("Dialog Text").GetComponent<TMP_Text>();
        nameHolder = GameObject.Find("Name").GetComponent<TMP_Text>();
        choicesCanvas = GameObject.Find("Choices").GetComponent<CanvasGroup>();
        choiceManager = GameObject.Find("ChoiceManager").GetComponent<ChoiceManager>();
        Debug.Log("trying to start " + conversation.name);
        GameState.DiableTime();
        //Start displying the supplied conversation
        if (!talking)
        {
            talking = true;


            StartCoroutine(DisplayConversation(conversation));
            if (conversation.Repeatable == false)
            {
                conversation.Skip = true;
            }
        }
    }

    IEnumerator DisplayConversation(Conversation conversation)
    {
        //foreach (var conversationLine in conversation.ConversationLines)
        for (int i = 0; i < conversation.ConversationLines.Length; i++)
        {
            currentConversationLine = conversation.ConversationLines[i];

            textHolder.text = currentConversationLine.ConversationText;
            imageHolder.sprite = currentConversationLine.DisplayPic;
            nameHolder.text = currentConversationLine.SpeakingCharacterName;
            if (currentConversationLine.MainDisplayPic == null)
            {
                mainImageHolder.GetComponent<CanvasGroup>().alpha = 0;
            }
            else
            {
                mainImageHolder.sprite = currentConversationLine.MainDisplayPic;
            }
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
                        if (currentConversationLine.NextSceneName != null && currentConversationLine.NextSceneName != "")
                        {
                            var lastPosition = GameState.GetLastScenePosition(currentConversationLine.NextSceneName);

                            Debug.Log("Last know pos for " + this.tag + " is " + lastPosition);

                            if (lastPosition != Vector3.zero)
                            {
                                GameState.CurrentPlayer.gameObject.transform.position = lastPosition;
                            }
                            else
                            {
                                Debug.Log("Set pos to 0");
                                GameState.CurrentPlayer.gameObject.transform.position = Vector3.zero;
                            }
                            NavigationManager.NavigateTo(currentConversationLine.NextSceneName);
                        }
                        done = true;
                    }
                    yield return null;
                }
            }

        }
        if (talking && choiceManager.Choice1Convo == null && choiceManager.Choice2Convo == null && choiceManager.Choice3Convo == null && choiceManager.Choice4Convo == null)
        {
            Debug.Log(conversation.name + " has finished it's loop");
            talking = false;
            choice = false;
            wait = false;
            if (conversation.EnemiesToFight != null && conversation.EnemiesToFight.Length > 0)
            {
                GameState.EnemyPrefabsForBattle = conversation.EnemiesToFight;

                SceneManager.LoadScene("TownBattle");
            }
        }
        choiceManager.ResetChoices();
        GameState.CurrentPlayer.GetComponent<PlayerMovement>().CantMove = false;
        GameState.EnableTime();
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

