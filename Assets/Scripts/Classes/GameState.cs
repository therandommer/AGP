using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static Player CurrentPlayer;
    public Player Player;
    public Player[] PlayerParty;
    public static Dictionary<string, Vector3> LastScenePositions = new Dictionary<string, Vector3>();//Save the scene and the position
    public static bool justExitedBattle;
    public static bool saveLastPosition = true;


    // Start is called before the first frame update
    void Start()
    {
        CurrentPlayer = Player;
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
