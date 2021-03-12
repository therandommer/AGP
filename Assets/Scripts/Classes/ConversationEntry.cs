using UnityEngine;

[System.Serializable]
public class ConversationEntry
{
    public string SpeakingCharacterName;
    public string ConversationText;
    public Sprite DisplayPic;
    public bool DecisionToBeMade;
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision1;
    [ConditionalHide("DecisionToBeMade")]
    public string Decision1Text;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Conversation Decision1Convo;  
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest Quest1ToGive;
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision2;
    [ConditionalHide("Decision2")] 
    public string Decision2Text;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Conversation Decision2Convo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Quest Quest2ToGive;
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision3;
    [ConditionalHide("Decision3")]
    public string Decision3Text;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Conversation Decision3Convo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Quest Quest3ToGive;
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision4;
    [ConditionalHide("Decision4")]
    public string Decision4Text;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Conversation Decision4Convo;    
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Quest Quest4ToGive;
}

