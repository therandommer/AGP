using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conversation : ScriptableObject
{
    [Header("Is the convo repeatable or one off")]
    public bool Repeatable;
    [Header("If one off, has this convo occured, if yes skip")]
    public bool Skip;
    public bool Fight;
    public ConversationEntry[] ConversationLines;

    public GameObject[] EnemiesToFight;
    public void Reset()
    {
        Skip = false;
        foreach(ConversationEntry entry in ConversationLines)
        {
            entry.Reset();
        }
    }
}
