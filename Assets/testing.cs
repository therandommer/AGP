using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testing : MonoBehaviour
{

    public GameObject Test1;
    public GameObject Test2;
    public GameObject Test3;


    public void AddTo()
    {
        GameState.AddToParty(Test1);
        GameState.AddToParty(Test2);
        GameState.AddToParty(Test3);
    }
}
