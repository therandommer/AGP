using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static PlayerController CurrentPlayer;
    public static GameObject PlayerObject;
    public GameObject player;
    public GameObject partymembertoSpawn;
    public Player PlayerProfile;
    public PlayerController PlayerController;
    public static List<GameObject> PlayerParty;
    public static GameObject[] PlayersToSpawn;
    public List<GameObject> playerParty;
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
        PlayersToSpawn = playerParty.ToArray();
        if (BattleSceneTest)
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
        for (int i = 0; i < PlayerParty.Count; i++)
        {
            if (PlayerParty[i] == PlayerObject)
            {
                if ((i + 1) <= PlayerParty.Count)
                {
                    Debug.Log("Go to next player");
                    Debug.Log("Player was " + CurrentPlayer.name);
                    PlayerObject = PlayerParty[i + 1];
                    CurrentPlayer = PlayerParty[i + 1].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
                else
                {
                    Debug.Log("Go back to first " + (i + 1));
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
            GameObject Player = Instantiate(PlayerObject, Vector3.zero, Quaternion.identity);
            Player.transform.position = new Vector3(0,0,1);
            Player.name = "Player";
            playerParty.Add(Player);
            PlayersToSpawn = playerParty.ToArray();
            PlayerController = Player.GetComponent<PlayerController>();
            CurrentPlayer = Player.GetComponent<PlayerController>();
            PlayerSpawned = true;
            AddToParty(partymembertoSpawn);
        }
    }

    public void AddToParty(GameObject PlayerToAdd)
    {
        GameObject Player = Instantiate(PlayerObject, Vector3.zero, Quaternion.identity);
        Player.transform.position = new Vector3(40, 0, 1);
        Player.GetComponent<PlayerMovement>().CantMove = true;
        Player.name = Player.GetComponent<PlayerController>().stats.PlayerProfile.name;
        playerParty.Add(Player);
        PlayersToSpawn = playerParty.ToArray();
        PlayerController = Player.GetComponent<PlayerController>();
        CurrentPlayer = Player.GetComponent<PlayerController>();
        PlayerSpawned = true;
    }

    public static void MovePartyMembersOffScreen()
    {
        for (int i = 0; i < PlayerParty.Count; i++)
        {
            if(PlayerParty[i].GetComponent<PlayerController>() != CurrentPlayer)
            {
                PlayerParty[i].transform.position = new Vector3(40, 0, 1);
                PlayerParty[i].GetComponent<PlayerMovement>().CantMove = true;
                PlayersToSpawn = PlayerParty.ToArray();
            }
        }
    }

    void Update()
    {
        if (PlayerObject == null)
        {
            if (player == null)
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
