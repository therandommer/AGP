using System.Collections;
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

    public void ChangeButtonText(ConversationEntry conversationLine)
    {
        Button1Text.text = conversationLine.Decision1Text;
        Button2Text.text = conversationLine.Decision2Text;
        Button3Text.text = conversationLine.Decision3Text;
        Button4Text.text = conversationLine.Decision4Text;
    }
}
