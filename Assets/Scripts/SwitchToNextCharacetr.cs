using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchToNextCharacetr : MonoBehaviour
{

    public void SwitchCharacter()
    {
        GameState.ChangeCurrentPlayer();
    }


}
