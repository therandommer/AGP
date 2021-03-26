using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{

    public Vector3 mousePos;

    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 7f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePos);
    }

}
