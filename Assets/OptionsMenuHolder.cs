using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenuHolder : MonoBehaviour
{

    List<InventoryItem> LegendaryGear = new List<InventoryItem>();

    public GameObject Angel;
    public GameObject Demon;
    public GameObject Knight;
    public GameObject Mage;

    public Sprite CheatSprite;

    public void LevelUp()
    {
        GameState.CurrentPlayer.stats.LevelUp();
    }

    public void AddMoney()
    {
        GameState.CurrentPlayer.Money += 100;
    }
    public void AddExp()
    {
        GameState.CurrentPlayer.Experience += 100;
    }

    public void Invincible()
    {
        if(GameState.CurrentPlayer.Invincible == true)
        {
            GameState.CurrentPlayer.Invincible = false;
            ShowMessage.Instance.StartCouroutineForMessage("Invincibility!","Turned Invincability Off", CheatSprite, 2f);
        }
        else
        {
            GameState.CurrentPlayer.Invincible = true;
            ShowMessage.Instance.StartCouroutineForMessage("Invincibility!","Turned Invincability On", CheatSprite, 2f);
        }

    }

    public void addLegEquipment()
    {
        foreach(InventoryItem II in LegendaryGear)
        {
            GameState.CurrentPlayer.Inventory.Add(II);
        }
    }
    public void AddAngel()
    {
        GameState.AddToParty(Angel);
    }

    public void AddDemon()
    {
        GameState.AddToParty(Demon);
    }

    public void AddKnight()
    {
        GameState.AddToParty(Knight);
    }
    public void AddMage()
    {
        GameState.AddToParty(Mage);
    }

    public void ClearParty()
    {
        GameState.ClearParty();
    }

}
