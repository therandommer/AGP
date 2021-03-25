using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfGameManager : MonoBehaviour
{

    public GameState gameState;

    public Player Character1Profile;
    public Player Character2Profile;
    public Player Character3Profile;
    public Player Character4Profile;

    public GameObject selectedCharacterProfile;

    public void StartGame()
    {
        GameState GS = Instantiate(gameState);
        GS.player = selectedCharacterProfile;
        GS.SetUpGameState();
    }


}
