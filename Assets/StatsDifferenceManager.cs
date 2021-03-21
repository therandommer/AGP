using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class StatsDifferenceManager : MonoBehaviour
{
    public Sprite IncreaseImage;
    public Sprite DecreaseImage;

    public int CachedHealthStat;
    public int CachedStrengthStat;
    public int CachedMagicStat;
    public int CachedDefenseStat;
    public int CachedSpeedStat;
    public int CachedArmourStat;

    public TMP_Text HealthStatDifference;
    public Image HealthStatDifferenceImage;
    public TMP_Text StrengthStatDifference;
    public Image StrengthStatDifferenceImage;
    public TMP_Text MagicStatDifference;
    public Image MagicStatDifferenceImage;
    public TMP_Text DefenseStatDifference;
    public Image DefenseStatDifferenceImage;
    public TMP_Text SpeedStatDifference;
    public Image SpeedStatDifferenceImage;
    public TMP_Text ArmourStatDifference;
    public Image ArmourStatDifferenceImage;


    public void PreviewStatChange()///Look at item supplied, add in stat to chached and display difference
    {

    }

    void Update()
    {
        
    }
}
