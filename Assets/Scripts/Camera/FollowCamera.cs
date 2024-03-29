﻿using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    // Distance between player and camera in horizontal direction 
    public float xOffset = 0f;
    // Distance between player and camera in vertical direction 
    public float yOffset = 0f;

    public float MinXCamera;
    public float MaxXCamera;

    public float MinYCamera;
    public float MaxYCamera;

    public float Zoom = -10;

    public bool JustFollowPlayer;
    // Reference to the player's transform. 
    public GameObject player;

    void LateUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, Zoom);

        }

        if (JustFollowPlayer)
        {
            this.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + xOffset, MinXCamera, MaxXCamera),
                                      Mathf.Clamp(player.transform.position.y + yOffset, MinYCamera, MaxYCamera),
                                      0);
        }
        else
        {
            Zoom += Input.mouseScrollDelta.y * 0.1f;

            this.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + xOffset, MinXCamera, MaxXCamera),
                                                  Mathf.Clamp(player.transform.position.y + yOffset, MinYCamera, MaxYCamera),
                                                  Zoom);
        }
    }

}
