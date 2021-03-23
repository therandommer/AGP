using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PopulateStatList : MonoBehaviour
{
    public TMP_Text Level;
    public TMP_Text Health;
    public TMP_Text Strength;
    public TMP_Text Magic;
    public TMP_Text Defense;
    public TMP_Text Speed;
    public TMP_Text Armour;
    public TMP_Text Damage;
    public Image PlayerImage;

    public bool OnlyNumbers;

    void Start()
    {
        populateStat();
    }

    public void populateStat()
    {
        PlayerController PC = GameState.CurrentPlayer;
        if (!OnlyNumbers)
        {
            if (Level != null)
            {
                Level.text = "Level:\n" + PC.stats.Level.ToString();
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

}
