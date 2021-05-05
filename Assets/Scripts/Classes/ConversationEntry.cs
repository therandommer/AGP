using UnityEngine;

[System.Serializable]
public class ConversationEntry
{
    public string SpeakingCharacterName;
    public string ConversationText;
    public Sprite DisplayPic;
    public Sprite MainDisplayPic;

    public bool DecisionToBeMade;

    [Header("Decision 1")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision1;
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("DecisionToBeMade")]
    public string Decision1Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public bool Decision1Repeatable;    
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public bool Decision1Pending;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Conversation Decision1Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest Quest1ToGive;    
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Conversation Quest1PendingConvo;    
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Conversation Quest1CompleteConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public string Quest1PendingButtonText;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public InventoryItem Item1ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public GameObject Character1ToGive;
    [Header("Use this on the NPC you want to have a TalkQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest TalkQuest1Confirm;
    [Header("Use this on the NPC you want to have a FetchQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision1")]
    public Quest FetchQuest1Confirm;

    [Header("Descision 2")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision2;
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("DecisionToBeMade")]
    public string Decision2Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public bool Decision2Repeatable;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public bool Decision2Pending;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Conversation Decision2Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Quest Quest2ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Conversation Quest2PendingConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Conversation Quest2CompleteConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public string Quest2PendingButtonText;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public InventoryItem Item2ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public GameObject Character2ToGive;
    [Header("Use this on the NPC you want to have a TalkQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Quest TalkQuest2Confirm;
    [Header("Use this on the NPC you want to have a FetchQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision2")]
    public Quest FetchQuest2Confirm;

    [Header("Descision 3")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision3;
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("DecisionToBeMade")]
    public string Decision3Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public bool Decision3Repeatable;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public bool Decision3Pending;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Conversation Decision3Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Quest Quest3ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Conversation Quest3PendingConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Conversation Quest3CompleteConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public string Quest3PendingButtonText;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public InventoryItem Item3ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public GameObject Character3ToGive;
    [Header("Use this on the NPC you want to have a TalkQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Quest TalkQuest3Confirm;
    [Header("Use this on the NPC you want to have a FetchQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision3")]
    public Quest FetchQuest3Confirm;

    [Header("Descision 4")]
    [ConditionalHide("DecisionToBeMade")]
    public bool Decision4;
    [Header("What text should be disaplayed on the button")]
    [ConditionalHide("DecisionToBeMade")]
    public string Decision4Text;
    [Header("Is this a one time decision")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public bool Decision4Repeatable;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public bool Decision4Pending;
    [Header("If you want to have a convo after decision fill this in")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Conversation Decision4Convo;
    [Header("What does the button give on press")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Quest Quest4ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Conversation Quest4PendingConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Conversation Quest4CompleteConvo;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public string Quest4PendingButtonText;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public InventoryItem Item4ToGive;
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public GameObject Character4ToGive;
    [Header("Use this on the NPC you want to have a TalkQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Quest TalkQuest4Confirm;
    [Header("Use this on the NPC you want to have a FetchQuest with")]
    [ConditionalHide("DecisionToBeMade", ConditionalSourceField2 = "Decision4")]
    public Quest FetchQuest4Confirm;

    [Header("Leave blank unless you want to move to another scene")]
    public string NextSceneName;
    public void Reset()
    {
        Decision1Pending = false;
        Decision2Pending = false;
        Decision3Pending = false;
        Decision4Pending = false;
    }
}

