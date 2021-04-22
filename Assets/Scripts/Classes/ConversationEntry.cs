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
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("DecisionToBeMade")]
    public string Decision1Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public bool Decision1Repeatable;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Conversation Decision1Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest Quest1ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public InventoryItem Item1ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public GameObject Character1ToGive;
    [Header("Use this on the NPC you want to have a TalkQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest TalkQuestConfirm;
    [Header("Use this on the NPC you want to have a FetchQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest FetchQuestConfirm;

    [Header("Descision 2")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision2;
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("Decision2")] 
    public string Decision2Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public bool Decision2Repeatable;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Conversation Decision2Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Quest Quest2ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public InventoryItem Item2ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public GameObject Character2ToGive;

    [Header("Descision 3")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision3;
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("Decision3")]
    public string Decision3Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public bool Decision3Repeatable;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Conversation Decision3Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Quest Quest3ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public InventoryItem Item3ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public GameObject Character3ToGive;

    [Header("Descision 4")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision4;
    [ConditionalHide("Decision4")]
    [Header("What text should be disaplayed on the button")]
    public string Decision4Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public bool Decision4Repeatable;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Conversation Decision4Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Quest Quest4ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public InventoryItem Item4ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public GameObject Character4ToGive;
}

