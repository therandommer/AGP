using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MapPosition : MonoBehaviour
{

    void Awake()
    {
        var lastPosition = GameState.GetLastScenePosition(SceneManager.GetActiveScene().name);

        if (lastPosition != Vector3.zero)
        {
            transform.position = lastPosition;
        }
    }

    void OnDestroy()
    {
        if (GameState.saveLastPosition)
        {
            GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, transform.position);
        }
    }


}
