using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryDisplay : MonoBehaviour
{

    public Button invPrefab;

    void Start()
    {

        foreach (var item in GameState.CurrentPlayer.Inventory)
        {
            Button inventoryChild = (Button)Instantiate(invPrefab, Vector3.zero, Quaternion.identity);
            inventoryChild.transform.parent = transform;
            inventoryChild.GetComponent<Image>().sprite = item.itemImage;
        }
    }
}  


