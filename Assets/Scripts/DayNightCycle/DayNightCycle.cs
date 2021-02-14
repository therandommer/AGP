using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    /*====
     Private Editable Variables
     =====*/
    [Header("Time Settings")]
    [SerializeField] TimeOfDay _TimeOfDay;

    /*===
      Private 
      ===*/
    private sTime _InGameTime;
    private uint _CurrentDay = 0;
    private float _SecondsPerDay = 0;
    private float _SecondsTimer = 0;
    private float TimerCheck = 0;
    private bool _bSkippingTime = false;
    private sTime _TimeSkipped;

    // Start is called before the first frame update
    void Start()
    {
        _SecondsPerDay = (60 * 60 * 24) / _TimeOfDay._PerDayLength.ConvertTimeToSeconds();
        _InGameTime = _TimeOfDay._StartingTime;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

    }

    void UpdateTime()
    {
        if (!_TimeOfDay.IsTimePaused())
        {
            if (_TimeOfDay._AmountOfHoursToSkip > 0 && !_bSkippingTime)
            {
                _bSkippingTime = true;

            }

            if (_bSkippingTime)
                _SecondsTimer += _SecondsPerDay * Time.deltaTime * _TimeOfDay._SkipTimeMultiplier;
            else
                _SecondsTimer += _SecondsPerDay * Time.deltaTime;

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

}

[CreateAssetMenu(fileName = "TimeOfDay", menuName = "ScriptableObjects/TimeOfDay")]
public class TimeOfDay : ScriptableObject
{
    [SerializeField, Tooltip("Real world time per in-game day")] public sTime _PerDayLength;
    [SerializeField, Tooltip("What time should the game start at")] public sTime _StartingTime;
    [SerializeField, Tooltip("How Fast should the game skip time")] public float _SkipTimeMultiplier = 10;
    private sTime _InGameTime;
    private bool _TimePaused;
    [HideInInspector] public int _AmountOfHoursToSkip = 0;

    public void PauseTime(bool Pause) => _TimePaused = Pause;
    public bool IsTimePaused() => _TimePaused;
    public void SetInGameTime(sTime Time) => _InGameTime = Time;
    public sTime GetTimeOfDay() => _InGameTime;   
    
    public void SkipTime(int AmountOfHours)
    {
        _AmountOfHoursToSkip = AmountOfHours;
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

