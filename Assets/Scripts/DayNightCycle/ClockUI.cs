using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private TimeOfDay _TimeOfDay;
    [SerializeField] private GameObject _GameTime;
    [SerializeField] private GameObject _ClockBackground;
    [SerializeField] private GameObject _MinuteHand;
    [SerializeField] private GameObject _HourHand;
    [SerializeField] private GameObject _DaysCounter;
    private Text _TimeText;
    private Text _DayCounterText;

    // Start is called before the first frame update
    void Start()
    {
        _TimeText = _GameTime.GetComponent<Text>();
        _DayCounterText = _DaysCounter.GetComponent<Text>();
        UpdateClockUI();

    }

    public void UpdateClockUI()
    {
        string TimeTextHours = "";
        string TimeTextMinutes = "";

        if (_TimeOfDay.GetTimeOfDay()._Hours < 10)
            TimeTextHours = "0" + _TimeOfDay.GetTimeOfDay()._Hours.ToString();
        else
            TimeTextHours = _TimeOfDay.GetTimeOfDay()._Hours.ToString();

        if (_TimeOfDay.GetTimeOfDay()._Minutes < 10)
            TimeTextMinutes = "0" + _TimeOfDay.GetTimeOfDay()._Minutes.ToString();
        else
            TimeTextMinutes = _TimeOfDay.GetTimeOfDay()._Minutes.ToString();


        _TimeText.text = TimeTextHours + " : " + TimeTextMinutes;


        float MinuteRotation = 6 * _TimeOfDay.GetTimeOfDay()._Minutes;
        Quaternion FinalMinuteRot = Quaternion.Euler(0, 0, -MinuteRotation);
        _MinuteHand.transform.rotation = FinalMinuteRot;

        float HourRotation = _TimeOfDay.GetTimeOfDay()._Hours <= 12 ?
                            30 * _TimeOfDay.GetTimeOfDay()._Hours
                            : 30 * (_TimeOfDay.GetTimeOfDay()._Hours - 12);

        HourRotation += (MinuteRotation / 360) * 30;

        Quaternion FinalHourRot = Quaternion.Euler(0, 0, -HourRotation);
        _HourHand.transform.rotation = FinalHourRot;

        _DayCounterText.text = GetDayString();//"Day " + _TimeOfDay.GetDayNumber();
    }

    string GetDayString()
    {
        eDayOfTheWeek Day = _TimeOfDay.GetDayOfTheWeek();

        switch(Day)
        {
            case eDayOfTheWeek.EDOTW_Monday:
                return "Monday";
            case eDayOfTheWeek.EDOTW_Tuesday:
                return "Tuesday";
            case eDayOfTheWeek.EDOTW_Wednesday:
                return "Wednesday";
            case eDayOfTheWeek.EDOTW_Thirsday:
                return "Thursday";
            case eDayOfTheWeek.EDOTW_Friday:
                return "Friday";
            case eDayOfTheWeek.EDOTW_Saturday:
                return "Saturday";
            case eDayOfTheWeek.EDOTW_Sunday:
                return "Sunday";
            default:
                return "Unkown";
        }
    }
}
