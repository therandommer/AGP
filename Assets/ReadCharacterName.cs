using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadCharacterName : MonoBehaviour
{

    public TMP_Text CurrentCharsName;
    public TMP_Text Gems;
    public Image CharSprite;

    public void Start()
    {
        ReadName();
    }

    public void ReadName()
    {
        CurrentCharsName.text = GameState.CurrentPlayer.stats.PlayerProfile.name;
        CharSprite.sprite = GameState.CurrentPlayer.stats.PlayerProfile.PlayerImage;
        if(Gems != null)
        {
            Gems.text = GameState.CurrentPlayer.Money.ToString();
        }
    }

}
