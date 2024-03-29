﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopulateStatList : MonoBehaviour
{
    public Player Profile;
    public TMP_Text Name;
    public TMP_Text Level;
    public TMP_Text Experience;
    public TMP_Text Health;
    public TMP_Text Strength;
    public TMP_Text Magic;
    public TMP_Text Defense;
    public TMP_Text Speed;
    public TMP_Text Armour;
    public TMP_Text Damage;
    public Image PlayerImage;

    public bool OnlyNumbers;

    public void populateStat()
    {
        if (Profile == null)
        {
            PlayerController PC = GameState.CurrentPlayer;
            if (!OnlyNumbers)
            {
                if (Name != null)
                    Name.text = PC.name;
                if (Level != null)
                {
                    Level.text = "Level:" + PC.stats.Level.ToString();
                }
                if(Experience != null)
                {
                    Experience.text = "Exp: " + PC.Experience + "/" + PC.ExperienceNeededToLevel;
                }
                if (Health != null)
                    Health.text = "Health:\n" + PC.stats.Health.ToString();
                if (Strength != null)
                    Strength.text = "Strength:\n" + PC.stats.Strength.ToString();
                if (Magic != null)
                    Magic.text = "Magic:\n" + PC.stats.Magic.ToString();
                if (Defense != null)
                    Defense.text = "Defense:\n" + PC.stats.Defense.ToString();
                if (Speed != null)
                    Speed.text = "Speed:\n" + PC.stats.Speed.ToString();
                if (Armour != null)
                    Armour.text = "Armour:\n" + PC.stats.Armor.ToString();
                if (PlayerImage != null)
                    PlayerImage.sprite = PC.stats.PlayerProfile.PlayerImage;
            }
            else
            {
                //Name.text = PC.name;
                //Level.text = PC.Level.ToString();
                Health.text = PC.stats.Health.ToString();
                Strength.text = PC.stats.Strength.ToString();
                Magic.text = PC.stats.Magic.ToString();
                Defense.text = PC.stats.Defense.ToString();
                Speed.text = PC.stats.Speed.ToString();
                Armour.text = PC.stats.Armor.ToString();
                Damage.text = PC.stats.Damage.ToString();
            }
        }
        else
        {
            if (!OnlyNumbers)
            {
                if (Name != null)
                    Name.text = Profile.Name;
                if (Level != null)
                {
                    Level.text = "Level:\n" + Profile.level.ToString();
                }
                if (Health != null)
                    Health.text = "Health:\n" + Profile.health.ToString();
                if (Strength != null)
                    Strength.text = "Strength:\n" + Profile.strength.ToString();
                if (Magic != null)
                    Magic.text = "Magic:\n" + Profile.magic.ToString();
                if (Defense != null)
                    Defense.text = "Defense:\n" + Profile.defense.ToString();
                if (Speed != null)
                    Speed.text = "Speed:\n" + Profile.speed.ToString();
                if (Armour != null)
                    Armour.text = "Armour:\n" + Profile.armor.ToString();
                if (PlayerImage != null)
                    PlayerImage.sprite = Profile.PlayerImage;
            }
            else
            {
                //Level.text = PC.Level.ToString();
                Health.text = Profile.health.ToString();
                Strength.text = Profile.strength.ToString();
                Magic.text = Profile.magic.ToString();
                Defense.text = Profile.defense.ToString();
                Speed.text = Profile.speed.ToString();
                Armour.text = Profile.armor.ToString();
                Damage.text = Profile.BonusDamage.ToString();
            }
        }
    }

}
