using UnityEngine;
using System.Collections;

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

    // Reference to the player's transform. 
    GameObject player;

    void LateUpdate()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
            Debug.Log("Moving Camera to " + new Vector3(player.transform.position.x, player.transform.position.y, -10));
        }
        this.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x + xOffset, MinXCamera, MaxXCamera), 
                                              Mathf.Clamp(player.transform.position.y + yOffset, MinYCamera, MaxYCamera), 
                                              transform.position.z);
    }

}
