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
                NavigationManager.NavigateTo("Village");
                break;
            case EntityType.Demon:
                NavigationManager.NavigateTo("Village");
                break;
            case EntityType.Human:
                NavigationManager.NavigateTo("Village");
                break;
            case EntityType.Mage:
                NavigationManager.NavigateTo("Village");
                break;
        }
    }


}
