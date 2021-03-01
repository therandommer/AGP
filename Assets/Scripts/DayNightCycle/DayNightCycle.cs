using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    /*====
     Private Editable Variables
     =====*/
    [Header("Time Settings")]
    [SerializeField] private TimeOfDay _TimeOfDay;
    [SerializeField] GameObject _NightTimeCanvas;
    [SerializeField] private ClockUI _ClockUI;

    /*===
      Private 
      ===*/
    private sTime _InGameTime;
    private uint _CurrentDay = 0;
    private float _SecondsPerDay = 0;
    private float _SecondsTimer = 0;
    private float _CurrentNightTimeAlpha = 0;
    private float TimerCheck = 0;
    private bool _bSkippingTime = false;
    private sTime _TimeSkipped;
    private int _LastHour;
    private uint _DayNumber = 1;

    /*===
    Time Skip Variables 
    ===*/
    private bool _bTimeSkipping = false;
    private float _CurrentSkipTimeInMins = 0;
    private float _SkipTimeToInMins = 0;
    //=====

    // Start is called before the first frame update
    void Start()
    {
        _SecondsPerDay = (60 * 60 * 24) / _TimeOfDay._PerDayLength.ConvertTimeToSeconds();
        _InGameTime = _TimeOfDay._StartingTime;

        SpriteRenderer MatRen = _NightTimeCanvas.GetComponent<SpriteRenderer>();
        MatRen.color = new Color(0, 0, 0, 0);

        _TimeOfDay.SetInGameTime(_TimeOfDay._StartingTime);
        _LastHour = _TimeOfDay.GetTimeOfDay()._Hours;
        _TimeOfDay.SetDayNumber(_DayNumber);
        _TimeOfDay.PauseTime(false);
    }

    // Update is called once per frame
    void Update()
    {
        _InGameTime = _TimeOfDay.GetTimeOfDay();

        if(_TimeOfDay.NeedToSkipTime())
        {
            _bTimeSkipping = true;

            sTime SkipTo = _TimeOfDay.GetSkipTillTime();

            if (SkipTo.ConvertTimeToSeconds() > _TimeOfDay.GetTimeOfDay().ConvertTimeToSeconds())
                _SkipTimeToInMins = SkipTo.ConvertTimeToSeconds() / 60;
            else
                _SkipTimeToInMins = (SkipTo.ConvertTimeToSeconds() + (86400)) / 60;

            _CurrentSkipTimeInMins = _TimeOfDay.GetTimeOfDay().ConvertTimeToSeconds() / 60;
        }

        if (!_bTimeSkipping)
            UpdateTime();
        else
            TimeSkip();

        UpdateNightFade();

        //check if a day has past
        if (_TimeOfDay.GetTimeOfDay()._Hours < _LastHour)
        {
            _DayNumber++;
            _TimeOfDay.SetDayNumber(_DayNumber);
            _LastHour = _TimeOfDay.GetTimeOfDay()._Hours;
        }
        else
            _LastHour = _TimeOfDay.GetTimeOfDay()._Hours;
    }

    void UpdateTime()
    {
        if (!_TimeOfDay.IsTimePaused())
        {
            _SecondsTimer += _SecondsPerDay * Time.deltaTime * _TimeOfDay.GetTimeMultiplier();

            if (!_TimeOfDay._bOnlyUpdateHandsPerMinute)
                _ClockUI.UpdateClockUI();

            if (_SecondsTimer >= 1)
            {
                float Remainder = _SecondsTimer % 1;
                _InGameTime._Seconds += (int)(_SecondsTimer - Remainder);
                _SecondsTimer = Remainder;

            }

            if (_InGameTime._Seconds >= 60)
            {
                int Remainder = _InGameTime._Seconds % 60;
                _InGameTime._Minutes += (_InGameTime._Seconds - Remainder) / 60;
                _InGameTime._Seconds = Remainder;

                if (_TimeOfDay._bOnlyUpdateHandsPerMinute)
                    _ClockUI.UpdateClockUI();

            }

            if (_InGameTime._Minutes >= 60)
            {
                int Remainder = _InGameTime._Minutes % 60;
                _InGameTime._Hours += (_InGameTime._Minutes - Remainder) / 60;
                _InGameTime._Minutes = Remainder;
            }

            if (_InGameTime._Hours >= 24)
        {
            _InGameTime._Hours = 0;
            _CurrentDay += 1;
            //Debug.Log("Hours: " + _InGameTime._Hours + ", Minutes: " + _InGameTime._Minutes + ", Seconds: " + _InGameTime._Seconds);
            Debug.Log("Days Increased!");
            Debug.Log("Day " + _CurrentDay);
            Debug.Log("Time Took: " + TimerCheck);
            TimerCheck = 0;
        }

            _TimeOfDay.SetInGameTime(_InGameTime);
        }
  
    }

    void UpdateNightFade()
    {
        if (_InGameTime._Hours >= _TimeOfDay._NightTimeFadeStart._Hours)
        {
            float TotalFadeTime = (_TimeOfDay._NightTimeStart._Hours - _TimeOfDay._NightTimeFadeStart._Hours) * 60;
            float NightFade = _TimeOfDay._NightTime;

            if (_InGameTime._Hours < _TimeOfDay._NightTimeStart._Hours)
            {
                float HoursTillNight = _TimeOfDay._NightTimeStart._Hours - _InGameTime._Hours;
                float TimeTillNightTime = TotalFadeTime - (TotalFadeTime - ((HoursTillNight * 60) - _InGameTime._Minutes));


                float PercentOfDaytime = 100 - ((100 / TotalFadeTime) * (TimeTillNightTime));
                NightFade *= PercentOfDaytime / 100;
                //Debug.Log(TotalFadeTime + " : " + TimeTillNightTime + " : " + PercentOfDaytime);

            }

            SpriteRenderer MatRen = _NightTimeCanvas.GetComponent<SpriteRenderer>();
            MatRen.color = new Color(_TimeOfDay._NightTimeColour.r, _TimeOfDay._NightTimeColour.g,
                                     _TimeOfDay._NightTimeColour.b, NightFade);

        }
        else if (_InGameTime._Hours < _TimeOfDay._NightTimeEnd._Hours)
        {
            float TotalFadeTime = (_TimeOfDay._NightTimeEnd._Hours - _TimeOfDay._FadeToDayTimeStart._Hours) * 60;
            float NightFade = _TimeOfDay._NightTime;

            if (_InGameTime._Hours >= _TimeOfDay._FadeToDayTimeStart._Hours)
            {
                float HoursTillDayTime = _TimeOfDay._NightTimeEnd._Hours - _InGameTime._Hours;
                float TimeTillDayTime = (TotalFadeTime - ((HoursTillDayTime * 60) - _InGameTime._Minutes));

                float PercentOfDaytime = ((100 / TotalFadeTime) * (TimeTillDayTime));
                NightFade *= (100 - PercentOfDaytime) / 100;
                //Debug.Log(TotalFadeTime + " : " + TimeTillDayTime + " : " + PercentOfDaytime);

            }

            SpriteRenderer MatRen = _NightTimeCanvas.GetComponent<SpriteRenderer>();
            MatRen.color = new Color(_TimeOfDay._NightTimeColour.r, _TimeOfDay._NightTimeColour.g, 
                                     _TimeOfDay._NightTimeColour.b, NightFade);
        }
        else
        {
            SpriteRenderer MatRen = _NightTimeCanvas.GetComponent<SpriteRenderer>();
            MatRen.color = new Color(0, 0, 0, 0);
        }
    }

    void TimeSkip()
    {
        int TH = _TimeOfDay.GetTimeOfDay()._Hours;
        int TM = _TimeOfDay.GetTimeOfDay()._Minutes;


        if (_CurrentSkipTimeInMins < _SkipTimeToInMins)
        {
            _CurrentSkipTimeInMins = Mathf.Lerp(_CurrentSkipTimeInMins, _SkipTimeToInMins,
                                                _TimeOfDay._TimeSkipLerpAmount * Time.deltaTime);
            sTime NewTime = new sTime();

            if (_SkipTimeToInMins - _CurrentSkipTimeInMins < 0.9f)
                _CurrentSkipTimeInMins = _SkipTimeToInMins;

            if (_CurrentSkipTimeInMins > 1440)
            {
                float SkipTime = _CurrentSkipTimeInMins - 1440;
                NewTime._Minutes = (int)SkipTime % 60;
                NewTime._Hours = (int)(SkipTime - NewTime._Minutes) / 60;
                _TimeOfDay.SetInGameTime(NewTime);
            }
            else
            {
                NewTime._Minutes = (int)_CurrentSkipTimeInMins % 60;
                NewTime._Hours = (int)(_CurrentSkipTimeInMins - NewTime._Minutes) / 60;
                _TimeOfDay.SetInGameTime(NewTime);
            }

            _ClockUI.UpdateClockUI();
        }
        else
        {
            _bTimeSkipping = false;
        }
    }

}

[System.Serializable]
public struct sTime
{
    public int _Hours;
    public int _Minutes;
    public int _Seconds;

    public float ConvertTimeToSeconds()
    {
        float FinalMinutes = _Minutes + (_Hours * 60);
        float FinalSeconds = _Seconds + (FinalMinutes * 60);

        return FinalSeconds;
    }
}

