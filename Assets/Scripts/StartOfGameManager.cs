using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartOfGameManager : MonoBehaviour
{

    public GameObject gameState;

    public GameObject selectedCharacterProfile;

    public Conversation AngelIntro;

    public Conversation DemonIntro;

    public Conversation MageIntro;

    public Conversation WarriorIntro;

    public void SelectCharacter(GameObject SelectCharacter)
    {
        selectedCharacterProfile = SelectCharacter;
    }

    public void StartGame()
    {
        GameObject GS = Instantiate(gameState);
        GameState.PlayerObject = selectedCharacterProfile;
        GS.GetComponent<GameState>().SetUpGameState();
        GS.GetComponent<GameState>().SetupInitialSpawnPoints();

        Debug.Log("Current Player is a " + GameState.CurrentPlayer.stats.PlayerProfile.Type);

        switch (GameState.CurrentPlayer.stats.PlayerProfile.Type)
        {
            case EntityType.Angel:
                ConversationManager.Instance.StartConversation(AngelIntro);
                break;
            case EntityType.Demon:
                ConversationManager.Instance.StartConversation(DemonIntro);
                break;
            case EntityType.Human:
                ConversationManager.Instance.StartConversation(WarriorIntro);
                break;
            case EntityType.Mage:
                ConversationManager.Instance.StartConversation(MageIntro);
                break;
        }
    }


}
