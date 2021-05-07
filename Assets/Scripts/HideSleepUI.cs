using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideSleepUI : MonoBehaviour
{
    public GameObject objectToHide = null;
    void Start()
    {
        if(objectToHide == null)
		{
            gameObject.SetActive(false); //deactivates this button if nothing to hide
		}
    }

    public void ToggleObject(bool _newState)
	{
        objectToHide.gameObject.SetActive(_newState);
	}
}
