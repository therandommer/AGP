using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Awake()
    {
        if(Player == null)
        {
            Player = GameState.CurrentPlayer.gameObject;
            Player.transform.position = gameObject.transform.position;

            Player.GetComponent<PlayerMovement>().CantMove = true;
            Player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
