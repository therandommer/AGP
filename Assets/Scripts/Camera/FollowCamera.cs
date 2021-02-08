using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour
{

    // Distance between player and camera in horizontal direction 
    public float xOffset = 0f;
    // Distance between player and camera in vertical direction 
    public float yOffset = 0f;

    // Reference to the player's transform. 
    public Transform player;

    void LateUpdate()
    {
        this.transform.position = new Vector3(player.transform.position.x + xOffset, this.transform.position.y + yOffset, this.transform.position.z);
    }

}
