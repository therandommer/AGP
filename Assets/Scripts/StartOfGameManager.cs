using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfGameManager : MonoBehaviour
{

    public GameObject gameState;

    public GameObject selectedCharacterProfile;

    public void SelectCharacter(GameObject SelectCharacter)
    {
        selectedCharacterProfile = SelectCharacter;
    }

    public void StartGame()
    {
        GameObject GS = Instantiate(gameState);
        GS.GetComponent<GameState>().player = selectedCharacterProfile;
        GS.GetComponent<GameState>().SetUpGameState();
        switch (GameState.CurrentPlayer.stats.PlayerProfile.Type)
        {
            case EntityType.Angel:
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
