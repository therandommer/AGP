using System.Collections.Generic;
using UnityEngine;

public enum PlayerLocation
{
    North,
    South,
    East,
    West
}

public class GameState : MonoBehaviour
{
    public static PlayerController CurrentPlayer;
    public static GameObject PlayerObject;
    public static PlayerLocation PlayerLoc;
    [Header("Debug lines will be overset")]
    public PlayerLocation PlayerLocTest;
    public GameObject player;
    public GameObject partymembertoSpawn;
    public Player PlayerProfile;
    public PlayerController PlayerController;
    public static List<GameObject> PlayerParty = new List<GameObject>();
    public static List<GameObject> BattleParty = new List<GameObject>();
    public static GameObject[] PlayersToSpawn;
    public static List<GameObject> playerParty = new List<GameObject>();
    public static Dictionary<string, Vector3> LastScenePositions = new Dictionary<string, Vector3>();//Save the scene and the position
    public static bool justExitedBattle;
    public static bool saveLastPosition = true;

    public static TimeOfDay Time;
    [Header("Setters")]
    public TimeOfDay TimeSetter;
    public FollowCamera CameraSetter;
    public ShopStorageHolder ShopStorageSetter;

    public static FollowCamera Camera;

    public static ShopStorageHolder ShopStorage;

    public BattleEnvironmentStorage Storage;

    public static GameObject[] EnemyPrefabsForBattle;
    [Header("Debug to stop duplication")]
    public bool PlayerSpawned = false;

    public bool CanHaveConvo;
    [Header("Use this is testing battlescene")]
    public bool BattleSceneTest;
    //[Header("BattleScene Info")]
    //public static GameObject PlayerSpawn;
    // Start is called before the first frame update
    void Awake()
    {
        Camera = CameraSetter;
        ShopStorage = ShopStorageSetter;
        PlayerLoc = PlayerLocTest;
        Time = TimeSetter;
        PlayersToSpawn = playerParty.ToArray();
        PlayerParty = playerParty;

        PlayerObject = player;
        PlayerController = PlayerObject.GetComponent<PlayerController>();
        CurrentPlayer = PlayerObject.GetComponent<PlayerController>();
        SetUpGameState();
        if (BattleSceneTest)
        {
            Instantiate(Storage.CheckTimeForBackground(Time.GetTimeOfDay()._Hours));
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadEnemyPrefabs(GameObject[] EnemyArray)
    {
        EnemyPrefabsForBattle = EnemyArray;
    }

    public static void SetBattleParty()
    {
        for(int i = 0; i < PlayerParty.Count; i++)
        {
            BattleParty.Add(PlayerParty[i]);
        }
    }

    public static void ChangeCurrentPlayerBattle()
    {
        for (int i = 0; i < BattleParty.Count; i++)
        {
            if (BattleParty[i] == PlayerObject)
            {
                if ((i + 1) <= (BattleParty.Count - 1))
                {
                    Debug.Log("Go to next player");
                    Debug.Log("Player was " + CurrentPlayer.name);
                    Debug.Log((i + 1) + " " + BattleParty.Count);
                    PlayerObject = BattleParty[i + 1];
                    CurrentPlayer = BattleParty[i + 1].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
                else
                {
                    Debug.Log("Go back to first " + (i + 1));
                    Debug.Log("Player was " + CurrentPlayer.name);
                    PlayerObject = BattleParty[0];
                    CurrentPlayer = BattleParty[0].GetComponent<PlayerController>();
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
            //Camera.player = Player;
            PlayerObject = Player;
            Player.transform.position = new Vector3(-2,0,1);
            Player.name = "Player";
            playerParty.Add(Player);
            PlayersToSpawn = playerParty.ToArray();
            PlayerController = Player.GetComponent<PlayerController>();
            CurrentPlayer = Player.GetComponent<PlayerController>();
            PlayerSpawned = true;
        }
    }

    public static void AddToParty(GameObject PlayerToAdd)
    {
        GameObject Player = Instantiate(PlayerToAdd, Vector3.zero, Quaternion.identity);
        Player.transform.position = new Vector3(40, 0, 1);
        Player.GetComponent<PlayerMovement>().CantMove = true;
        Player.name = Player.GetComponent<PlayerController>().stats.PlayerProfile.name;

        playerParty.Add(Player);
        PlayersToSpawn = playerParty.ToArray();
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

    public static void ChangeCurrentPlayer()
    {
        Debug.Log(PlayerParty.Count);
        for (int i = 0; i < PlayerParty.Count; i++)
        {
            if (PlayerParty[i] == PlayerObject)
            {
                if ((i + 1) <= PlayerParty.Count)
                {
                    Debug.Log("Go to next player");
                    PlayerParty[i + 1].transform.position = PlayerParty[i].transform.position;
                    PlayerParty[i].transform.position = new Vector3(40,0,1);
                    PlayerParty[i].GetComponent<PlayerMovement>().CantMove = true;

                    PlayerParty[i + 1].GetComponent<PlayerMovement>().CantMove = false;
                    PlayerObject = PlayerParty[i + 1];
                    CurrentPlayer = PlayerParty[i + 1].GetComponent<PlayerController>();
                    return;
                }
                else
                {
                    Debug.Log("Go back to first ");
                    Debug.Log("Player was " + CurrentPlayer.name);

                    PlayerParty[0].transform.position = PlayerParty[i].transform.position;
                    PlayerParty[i].transform.position = new Vector3(40, 0, 1);
                    PlayerParty[i].GetComponent<PlayerMovement>().CantMove = true;

                    PlayerParty[0].GetComponent<PlayerMovement>().CantMove = false;

                    PlayerObject = PlayerParty[0];
                    CurrentPlayer = PlayerParty[0].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
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
            Debug.Log("Cannot find last know position on this map: " + sceneName);
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

    public void SetupInitialSpawnPoints()
    {
        ///Setting up initial spawn areas for the farm zones
        SetLastScenePosition("EastSwamp", new Vector3(-64, -16, 0));
        SetLastScenePosition("NorthCave", new Vector3(12, -24, 0));
        SetLastScenePosition("SouthForest", new Vector3(5, -29, 0));
        SetLastScenePosition("WestOasis", new Vector3(-22, 7, 0));

        ///Setting up initial town zones, if the player starts there they spawn more centrally
        switch (GameState.CurrentPlayer.stats.PlayerProfile.Type)
        {
            case EntityType.Angel:
                SetLastScenePosition("EastTown", new Vector3(-22, 1, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -26, 0));
                SetLastScenePosition("SouthTown", new Vector3(2, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(30, 7, 0));
                SetLastScenePosition("Overworld", new Vector3(-2, -4, 0));
                NavigationManager.NavigateTo("SouthTown");
                break;
            case EntityType.Demon:
                SetLastScenePosition("EastTown", new Vector3(-22, 1, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -6, 0));
                SetLastScenePosition("SouthTown", new Vector3(17, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(30, 7, 0));
                SetLastScenePosition("Overworld", new Vector3(30, 7, 0));
                NavigationManager.NavigateTo("NorthTown");
                break;
            case EntityType.Human:
                SetLastScenePosition("EastTown", new Vector3(-22, 1, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -26, 0));
                SetLastScenePosition("SouthTown", new Vector3(17, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(12, 8, 0));
                SetLastScenePosition("Overworld", new Vector3(-18, 14, 0));
                NavigationManager.NavigateTo("WestTown");
                break;
            case EntityType.Mage:
                SetLastScenePosition("EastTown", new Vector3(-9, 3, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -26, 0));
                SetLastScenePosition("SouthTown", new Vector3(17, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(30, 7, 0));
                SetLastScenePosition("Overworld", new Vector3(27, 15, 0));
                NavigationManager.NavigateTo("EastTown");
                break;
        }
    }
}
