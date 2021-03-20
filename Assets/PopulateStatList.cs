using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PopulateStatList : MonoBehaviour
{
    public TMP_Text Level;
    public TMP_Text Health;
    public TMP_Text Strength;
    public TMP_Text Magic;
    public TMP_Text Defense;
    public TMP_Text Speed;
    public TMP_Text Armour;
    public Image PlayerImage;

    void Start()
    {
        PlayerController PC = GameState.CurrentPlayer;
        Level.text = "Level: " + PC.Level.ToString();
        Health.text = "Health: " + PC.Health.ToString();
        Strength.text = "Strength: " + PC.Strength.ToString();
        Magic.text = "Magic: " + PC.Magic.ToString();
        Defense.text = "Defense: " + PC.Defense.ToString();
        Speed.text = "Speed: " + PC.Speed.ToString();
        Armour.text = "Armour: " + PC.Armor.ToString();
        PlayerImage.sprite = PC.PlayerProfile.PlayerImage;
    }

}
