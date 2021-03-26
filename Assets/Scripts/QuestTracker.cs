using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{

    List<Quest> QuestList = new List<Quest>();

    void Start()
    {
        QuestList = GameState.CurrentPlayer.QuestLog;
    }


}
