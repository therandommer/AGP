using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerLoc : MonoBehaviour
{

    public PlayerLocation PlayerLocationChange;

    void OnTriggerEnter2D(Collider2D col)
    {
        GameState.PlayerLoc = PlayerLocationChange;
    }

}
