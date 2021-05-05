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
    public GameObject TestMap;
    [Header("Debug lines that will be overset")]
    public PlayerLocation PlayerLocTest;
    public GameObject player;
    public GameObject partymembertoSpawn;
    public Player PlayerProfile;
    public int NumberofBossesNeededToFightFinalBossDebug;
    public static int NumberofBossesNeededToFightFinalBoss;
    public bool CanFightFinalBossDebug = false;
    public static bool CanFightFinalBoss;
    public int NumberOfBossesDefeatedDebug;
    public static int NumberOfBossesDefeated = 0;
    public PlayerController PlayerController;

    public static List<GameObject> PartyMembers = new List<GameObject>();

    public static List<GameObject> ActiveParty = new List<GameObject>();

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
    public static bool PreSetCombat;
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
        NumberofBossesNeededToFightFinalBoss = 4;
        PlayerObject = player;
        PlayerController = PlayerObject.GetComponent<PlayerController>();
        CurrentPlayer = PlayerObject.GetComponent<PlayerController>();
        SetUpGameState();
        if (BattleSceneTest)
        {
            //Instantiate(Storage.CheckTimeForBackground(Time.GetTimeOfDay()._Hours));
            Instantiate(TestMap);
            PreSetCombat = false;
        }
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadEnemyPrefabs(GameObject[] EnemyArray)
    {
        EnemyPrefabsForBattle = EnemyArray;
    }

    public static void IncreaseNumberOfBossesDefeated()
    {
        NumberOfBossesDefeated++;
        Debug.Log("Num of Bosses: " + NumberOfBossesDefeated);
        if (NumberOfBossesDefeated >= NumberofBossesNeededToFightFinalBoss)
        {
            CanFightFinalBoss = true;
        }
    }

    public static void RemoveFromActiveParty(GameObject PlayerToRemove)
    {
        for (int i = 0; i < ActiveParty.Count; i++)
        {
            if (ActiveParty[i] == PlayerToRemove)
            {
                ActiveParty.RemoveAt(i);
            }
        }
    }

    public static void ChangeCurrentPlayerBattle()
    {
        for (int i = 0; i < ActiveParty.Count; i++)
        {
            if (ActiveParty[i] == PlayerObject)
            {
                if ((i + 1) <= (ActiveParty.Count - 1))
                {
                    Debug.Log("Go to next player");
                    Debug.Log("Player was " + CurrentPlayer.name);
                    Debug.Log((i + 1) + " " + ActiveParty.Count);
                    PlayerObject = ActiveParty[i + 1];
                    CurrentPlayer = ActiveParty[i + 1].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
                else
                {
                    Debug.Log("Go back to first " + (i + 1));
                    Debug.Log("Player was " + CurrentPlayer.name);
                    PlayerObject = ActiveParty[0];
                    CurrentPlayer = ActiveParty[0].GetComponent<PlayerController>();
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
            Player.transform.position = new Vector3(-2, 0, 1);
            Player.name = "Player";
            ActiveParty.Add(Player);
            PlayerController = Player.GetComponent<PlayerController>();
            CurrentPlayer = Player.GetComponent<PlayerController>();
            PlayerSpawned = true;
            ShopStorage.GenerateAllShopsInv();
        }
    }
    public static void ClearParty()
    {
        ActiveParty.Clear();
        ActiveParty.Add(PlayerObject);
    }
    public static void AddToParty(GameObject PlayerToAdd)
    {
        GameObject newPlayer = Instantiate(PlayerToAdd);
        if (ActiveParty.Count < 4)
        {
            ActiveParty.Add(newPlayer);
        }
        else
        {
            PartyMembers.Add(PlayerToAdd);
        }
        newPlayer.SetActive(false);
    }

    public static void MovePartyMembersOffScreen()
    {
        for (int i = 0; i < ActiveParty.Count; i++)
        {
            if (ActiveParty[i].GetComponent<PlayerController>() != CurrentPlayer)
            {
                ActiveParty[i].transform.position = new Vector3(40, 0, 1);
                ActiveParty[i].GetComponent<PlayerMovement>().CantMove = true;
            }
        }
    }

    public static void ChangeCurrentPlayer()
    {
        Debug.Log("Active: " + ActiveParty.Count);
        for (int i = 0; i < ActiveParty.Count; i++)
        {
            Debug.Log("Looking for: " + PlayerObject.name + " " + ActiveParty[i].name);
            if (ActiveParty[i].name == PlayerObject.name)
            {
                if ((i + 1) <= ActiveParty.Count - 1)
                {
                    Debug.Log("Go to next player");
                    ActiveParty[i].SetActive(false);
                    ActiveParty[i + 1].SetActive(true);
                    ActiveParty[i + 1].transform.position = ActiveParty[i].transform.position;
                    ActiveParty[i + 1].GetComponent<PlayerController>().Inventory = ActiveParty[i].GetComponent<PlayerController>().Inventory;
                    ActiveParty[i + 1].GetComponent<PlayerController>().QuestLog = ActiveParty[i].GetComponent<PlayerController>().QuestLog;
                    ActiveParty[i + 1].GetComponent<PlayerController>().Money = ActiveParty[i].GetComponent<PlayerController>().Money;

                    ActiveParty[i + 1].GetComponent<PlayerMovement>().CantMove = false;
                    PlayerObject = ActiveParty[i + 1];
                    Debug.Log(PlayerObject.name + " active");
                    CurrentPlayer = ActiveParty[i + 1].GetComponent<PlayerController>();
                    return;
                }
                else
                {
                    Debug.Log("Go back to first ");
                    Debug.Log("Player was " + CurrentPlayer.name);
                    ActiveParty[i].SetActive(false);
                    ActiveParty[0].SetActive(true);
                    ActiveParty[0].transform.position = ActiveParty[i].transform.position;

                    PlayerObject = ActiveParty[0];
                    CurrentPlayer = ActiveParty[0].GetComponent<PlayerController>();
                    Debug.Log("Player is now " + CurrentPlayer.name);
                    return;
                }
            }
        }
    }
    public static void ChangeCurrentPlayer(GameObject newPlayer)
    {
        for (int i = 0; i < ActiveParty.Count; i++) //Look for active
        {
            if (ActiveParty[i] == GameState.CurrentPlayer.gameObject)
            {
                Debug.Log("Go to next player");
                ActiveParty[i].SetActive(false);
                newPlayer.SetActive(true);
                newPlayer.transform.position = ActiveParty[i].transform.position;
                newPlayer.GetComponent<PlayerController>().Inventory = ActiveParty[i].GetComponent<PlayerController>().Inventory;
                newPlayer.GetComponent<PlayerController>().QuestLog = ActiveParty[i].GetComponent<PlayerController>().QuestLog;
                newPlayer.GetComponent<PlayerController>().Money = ActiveParty[i].GetComponent<PlayerController>().Money;

                newPlayer.GetComponent<PlayerMovement>().CantMove = false;
                PlayerObject = newPlayer;
                CurrentPlayer = newPlayer.GetComponent<PlayerController>();
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


        SetLastScenePosition("NorthBoss", new Vector3(0, -3, 0));
        SetLastScenePosition("EastBoss", new Vector3(-12, 3, 0));
        SetLastScenePosition("SouthBoss", new Vector3(-1, -8, 0));
        SetLastScenePosition("WestBoss", new Vector3(0, -2, 0));

        ///Setting up initial town zones, if the player starts there they spawn more centrally
        switch (GameState.CurrentPlayer.stats.PlayerProfile.Type)
        {
            case EntityType.Angel:
                SetLastScenePosition("EastTown", new Vector3(-22, 1, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -26, 0));
                SetLastScenePosition("SouthTown", new Vector3(2, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(30, 7, 0));
                SetLastScenePosition("Overworld", new Vector3(-2, -4, 0));
                PlayerLoc = PlayerLocation.South;
                NavigationManager.NavigateTo("SouthTown");
                break;
            case EntityType.Demon:
                SetLastScenePosition("EastTown", new Vector3(-22, 1, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -6, 0));
                SetLastScenePosition("SouthTown", new Vector3(17, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(30, 7, 0));
                SetLastScenePosition("Overworld", new Vector3(30, 7, 0));
                PlayerLoc = PlayerLocation.North;
                NavigationManager.NavigateTo("NorthTown");
                break;
            case EntityType.Human:
                SetLastScenePosition("EastTown", new Vector3(-22, 1, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -26, 0));
                SetLastScenePosition("SouthTown", new Vector3(17, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(12, 8, 0));
                SetLastScenePosition("Overworld", new Vector3(-18, 14, 0));
                PlayerLoc = PlayerLocation.West;
                NavigationManager.NavigateTo("WestTown");
                break;
            case EntityType.Mage:
                SetLastScenePosition("EastTown", new Vector3(-9, 3, 0));
                SetLastScenePosition("NorthTown", new Vector3(-2, -26, 0));
                SetLastScenePosition("SouthTown", new Vector3(17, -3, 0));
                SetLastScenePosition("WestTown", new Vector3(30, 7, 0));
                SetLastScenePosition("Overworld", new Vector3(27, 15, 0));
                PlayerLoc = PlayerLocation.East;
                NavigationManager.NavigateTo("EastTown");
                break;
        }
    }
}
