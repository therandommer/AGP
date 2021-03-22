using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum eTimeMultipliers
{
    ETM_Default,
    ETM_Town, 
    ETM_Combat,
    ETM_WorldMapMoving,
    ETM_WorldMapIdle,
    ETM_InDialogue, 
};

public enum eDayOfTheWeek
{
    EDOTW_Monday = 0,
    EDOTW_Tuesday = 1,
    EDOTW_Wednesday = 2,
    EDOTW_Thirsday = 3,
    EDOTW_Friday = 4,
    EDOTW_Saturday = 5,
    EDOTW_Sunday = 6,
}

[System.Serializable]
struct fTimeMultipliers
{
    [System.NonSerialized] public eTimeMultipliers _TimeMulti;
    [SerializeField] public float _DefaultTime;
    [SerializeField] public float _Town;
    [SerializeField] public float _Combat;
    [SerializeField] public float _WorldMapMoving;
    [SerializeField] public float _WorldMapIdle;
    [SerializeField] public float _InDialogue;
};

[CreateAssetMenu(fileName = "TimeOfDay", menuName = "ScriptableObjects/TimeOfDay")]
public class TimeOfDay : ScriptableObject
{
    [Header("General Settings")]
    [SerializeField, Tooltip("Real world time per in-game day")] public sTime _PerDayLength;
    [SerializeField, Tooltip("Untick for clock hands to constantly update, tick to update per in game minute")]  
    public bool _bOnlyUpdateHandsPerMinute = true;
    [SerializeField] private fTimeMultipliers _TimeMultipliers;

    [Header("Fade Time Settings")]
    [SerializeField, Tooltip("What time should the game start at")] public sTime _StartingTime;
    [SerializeField, Tooltip("When does it start to turn night time")] public sTime _NightTimeFadeStart;
    [SerializeField, Tooltip("When is it night time")] public sTime _NightTimeStart;
    [SerializeField, Tooltip("When does the night time start to fade back to daytime")] public sTime _FadeToDayTimeStart;
    [SerializeField, Tooltip("When does night time end")] public sTime _NightTimeEnd;

    /** change this to increase or decrease the time */
    [Header("Night Time Darkness settings")]
    [SerializeField, Tooltip("How dark should the game be, changes the alpha of the texture"),
        Range(0.0f, 1.0f)]
    public float _NightTime;

    [SerializeField, Tooltip("How Fast should the game skip time")] public float _TimeSkipLerpAmount = 1;
    [SerializeField, Tooltip("What colour should night time be")] public Color _NightTimeColour;

    /*===
    below are used for time skipping when we need to skip to a certain time of day 
     ===*/
    private sTime _SkipTill;
    private bool _bNeedToSkipTime = false;
    private sTime _InGameTime;
    //=====

    private uint _DayNumber = 0;
    private bool _TimePaused;
    private eDayOfTheWeek _CurrentDay = eDayOfTheWeek.EDOTW_Monday;

    public void PauseTime(bool Pause) => _TimePaused = Pause;
    public bool IsTimePaused() => _TimePaused;
    public void SetInGameTime(sTime Time) => _InGameTime = Time;
    public sTime GetTimeOfDay() => _InGameTime;

    public eDayOfTheWeek GetDayOfTheWeek() => _CurrentDay;

    public void SetDayOfTheWeek(eDayOfTheWeek Day) => _CurrentDay = Day;

    public void SkipTime(sTime SkipTillWhen)
    {
        _SkipTill = SkipTillWhen;
        _bNeedToSkipTime = true;
    }

    /// <summary>
    /// don't call this function, only needs to be used
    /// on the DayNightCycle Script. Used to check if we need to skip to
    /// a certain time
    /// </summary>
    /// <returns></returns>
    public bool NeedToSkipTime()
    {
        if(_bNeedToSkipTime)
        {
            _bNeedToSkipTime = false;
            return true;
        }

        return false;
    }

    public sTime GetSkipTillTime() => _SkipTill;

    public void SetDayNumber(uint DayNumber) => _DayNumber = DayNumber;

    public uint GetDayNumber() => _DayNumber;

    public float GetTimeMultiplier()
    {
        switch(_TimeMultipliers._TimeMulti)
        {
            case eTimeMultipliers.ETM_Default:
                return _TimeMultipliers._DefaultTime;
                break;
            case eTimeMultipliers.ETM_Town:
                return _TimeMultipliers._Town;
                break;
            case eTimeMultipliers.ETM_Combat:
                return _TimeMultipliers._Combat;
                break;
            case eTimeMultipliers.ETM_WorldMapMoving:
                return _TimeMultipliers._WorldMapMoving;
                break;
            case eTimeMultipliers.ETM_WorldMapIdle:
                return _TimeMultipliers._WorldMapIdle;
                break;
            case eTimeMultipliers.ETM_InDialogue:
                return _TimeMultipliers._InDialogue;
                break;
        };

        return 1.0f;
    }
}
