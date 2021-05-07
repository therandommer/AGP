using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookFor : MonoBehaviour
{
    void Awake()
    {
        foreach(string Name in GameState.CurrentPlayer.EnemyToDelete)
        {
            Destroy(GameObject.Find(Name));
        }
    }
}
