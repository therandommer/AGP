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
                Level.text = "Level:\n" + PC.Level.ToString();
            }
            if (Health != null)
                Health.text = "Health:\n" + PC.Health.ToString();
            if (Strength != null)
                Strength.text = "Strength:\n" + PC.Strength.ToString();
            if (Magic != null)
                Magic.text = "Magic:\n" + PC.Magic.ToString();
            if (Defense != null)
                Defense.text = "Defense:\n" + PC.Defense.ToString();
            if (Speed != null)
                Speed.text = "Speed:\n" + PC.Speed.ToString();
            if (Armour != null)
                Armour.text = "Armour:\n" + PC.Armor.ToString();
            if (PlayerImage != null)
                PlayerImage.sprite = PC.PlayerProfile.PlayerImage;
        }
        else
        {
            //Level.text = PC.Level.ToString();
            Health.text = PC.Health.ToString();
            Strength.text = PC.Strength.ToString();
            Magic.text = PC.Magic.ToString();
            Defense.text = PC.Defense.ToString();
            Speed.text = PC.Speed.ToString();
            Armour.text = PC.Armor.ToString();
            Damage.text = PC.Damage.ToString();
        }
    }

}
