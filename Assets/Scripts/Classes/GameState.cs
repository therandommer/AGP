using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    public static PlayerController CurrentPlayer;
    public static GameObject PlayerObject;
    public GameObject player;
    public Player PlayerProfile;
    public PlayerController PlayerController;
    public static GameObject[] PlayerParty;
    public GameObject[] playerParty;
    public static Dictionary<string, Vector3> LastScenePositions = new Dictionary<string, Vector3>();//Save the scene and the position
    public static bool justExitedBattle;
    public static bool saveLastPosition = true;

    public bool PlayerSpawned = false;

    public bool CanHaveConvo;
    [Header("Use this is testing battlescene")]
    public bool BattleSceneTest;
    [Header("BattleScene Info")]
    public static GameObject PlayerSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        PlayerParty = playerParty;
        if(BattleSceneTest)
        {
            PlayerObject = player;
            PlayerController = PlayerObject.GetComponent<PlayerController>();
            CurrentPlayer = PlayerObject.GetComponent<PlayerController>();
            SetUpGameState();
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void ChangeCurrentPlayer()
    {
        for (int i = 0; i < PlayerParty.Length; i++)
        {
            if(PlayerParty[i] == PlayerObject)
            {
                if((i+1) <= PlayerParty.Length)
                {
                    Debug.Log("Go to next player");
                    Debug.Log("Player was " + CurrentPlayer.name);
                    PlayerObject = PlayerParty[i + 1];
                    CurrentPlayer = PlayerParty[i+1].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
                else
                {
                    Debug.Log("Go back to first " + (i+1));
                    Debug.Log("Player was " + CurrentPlayer.name);
                    PlayerObject = PlayerParty[0];
                    CurrentPlayer = PlayerParty[0].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
            }
        }
    }

    public void SetUpGameState()
    {
        PlayerObject = player;
        if (!PlayerSpawned)
        {
            if (SceneManager.GetActiveScene().name == "Village")
            {
                GameObject Player = Instantiate(PlayerObject, Vector3.zero, Quaternion.identity);
                Player.name = "Player";
                Player.transform.position = transform.position;
                PlayerController = Player.GetComponent<PlayerController>();
                CurrentPlayer = Player.GetComponent<PlayerController>();
            }
            PlayerSpawned = true;
        }
    }

    void Update()
    {
        if(PlayerObject == null)
        {
            if(player == null)
            {
                player = GameObject.Find("Player");
                PlayerObject = player;
            }
            else
            {
                PlayerObject = player;
            }    
        }
    }

    public static Vector3 GetLastScenePosition(string sceneName)//Check wether the the last scene position was saved
    {
        if (GameState.LastScenePositions.ContainsKey(sceneName))
        {
            var lastPos = GameState.LastScenePositions[sceneName];
            return lastPos;
        }
        else//If not return a null value, means that the player will spawn at the default location
        {
            return Vector3.zero;
        }
    }

    public static void SetLastScenePosition(string sceneName, Vector3 position)//Checks wether if the value is already saved and if not save it
    {
        if (GameState.LastScenePositions.ContainsKey(sceneName))
        {
            GameState.LastScenePositions[sceneName] = position;
        }
        else
        {
            GameState.LastScenePositions.Add(sceneName, position);
        }
    }
}
