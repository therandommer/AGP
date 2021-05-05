using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfGameManager : MonoBehaviour
{

    public GameObject gameState;

    public GameObject selectedCharacterProfile;

    public GameObject Test;
    public void SelectCharacter(GameObject SelectCharacter)
    {
        selectedCharacterProfile = SelectCharacter;
    }

    public void StartGame()
    {
        GameObject GS = Instantiate(gameState);
        GS.GetComponent<GameState>().player = selectedCharacterProfile;
        GS.GetComponent<GameState>().SetUpGameState();
        GS.GetComponent<GameState>().SetupInitialSpawnPoints();
        GameState.AddToParty(Test);

        switch (GameState.CurrentPlayer.stats.PlayerProfile.Type)
        {
            case EntityType.Angel:
                var lastPosition = GameState.GetLastScenePosition("SouthTown");

                Debug.Log("Last know pos for " + this.tag + " is " + lastPosition);

                if (lastPosition != Vector3.zero)
                {
                    GameState.CurrentPlayer.gameObject.transform.position = lastPosition;
                }
                else
                {
                    Debug.Log("Set pos to 0");
                    GameState.CurrentPlayer.gameObject.transform.position = Vector3.zero;
                }

                NavigationManager.NavigateTo("SouthTown");
                break;
            case EntityType.Demon:
                NavigationManager.NavigateTo("NorthTown");
                break;
            case EntityType.Human:
                NavigationManager.NavigateTo("WestTown");
                break;
            case EntityType.Mage:
                NavigationManager.NavigateTo("EastTown");
                break;
        }
    }


}
