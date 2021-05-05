using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ReadCharacterName : MonoBehaviour
{

    public TMP_Text CurrentCharsName;

    public void ReadName()
    {
        CurrentCharsName.text = GameState.CurrentPlayer.stats.PlayerProfile.name;
    }

}
