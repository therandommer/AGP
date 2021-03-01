using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SleepingBed : MonoBehaviour
{
    [SerializeField] private TimeOfDay _TimeOfDay;
    [SerializeField] private GameObject _SkipTimeTextObject;
    [SerializeField] private GameObject _HourBorderObject;
    [SerializeField] private GameObject _MinuteBorderObject;
    private GameObject _PlayerEnteringBed;
    private bool _bCanSleepOnBed = false;

    private Text _SkipToTimeText;
    // private float _MinutesToSkip;
    private GameObject _CanvasUI;
    private bool _bIsChangingHours = true;
    private sTime _TimeToSkipTo;

    private void Start()
    {
        _SkipToTimeText = _SkipTimeTextObject.GetComponent<Text>();
        _CanvasUI = _SkipTimeTextObject.transform.parent.gameObject;
        _SkipTimeTextObject.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_bCanSleepOnBed)
            CheckForInput();       
    }

    void CheckForInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_bIsChangingHours)
            {
                _HourBorderObject.SetActive(true);
                _MinuteBorderObject.SetActive(false);
            }
            else
            {
                _HourBorderObject.SetActive(false);
                _MinuteBorderObject.SetActive(true);
            }

            if (_SkipTimeTextObject.activeInHierarchy)
            {
                _SkipTimeTextObject.transform.parent.gameObject.SetActive(false);
                _bIsChangingHours = true;
                _TimeOfDay.PauseTime(false);

                if (_PlayerEnteringBed != null)
                {
                    _PlayerEnteringBed.GetComponent<PlayerMovement>().enabled = true;
                }
            }
            else
            {
                _TimeToSkipTo = _TimeOfDay.GetTimeOfDay();
                _SkipTimeTextObject.transform.parent.gameObject.SetActive(true);
                _TimeOfDay.PauseTime(true);
                if (_PlayerEnteringBed != null)
                {
                    _PlayerEnteringBed.GetComponent<PlayerMovement>().enabled = false;
                }
            }
        }

        if (_SkipTimeTextObject.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                _bIsChangingHours = true ? !_bIsChangingHours : _bIsChangingHours;

                if(_bIsChangingHours)
                {
                    _HourBorderObject.SetActive(true);
                    _MinuteBorderObject.SetActive(false);
                }
                else
                {
                    _HourBorderObject.SetActive(false);
                    _MinuteBorderObject.SetActive(true);
                }

            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (_bIsChangingHours)
                    _TimeToSkipTo._Hours++;
                else
                    _TimeToSkipTo._Minutes += 5;

                if (_TimeToSkipTo._Hours > 23)
                    _TimeToSkipTo._Hours = 0;

                if (_TimeToSkipTo._Minutes > 59)
                    _TimeToSkipTo._Minutes = _TimeToSkipTo._Minutes % 60;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                if (_bIsChangingHours)
                    _TimeToSkipTo._Hours--;
                else
                    _TimeToSkipTo._Minutes -= 5;

                if (_TimeToSkipTo._Hours < 0)
                    _TimeToSkipTo._Hours = 23;

                if (_TimeToSkipTo._Minutes < 0)
                    _TimeToSkipTo._Minutes = 60 + _TimeToSkipTo._Minutes;
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {

                // int ToHours = _TimeToSkipTo._Hours;
                // int FromHours = _TimeOfDay.GetTimeOfDay()._Hours;
                // int HoursToMinsSkip = 0;
                //
                // if (ToHours > FromHours)
                //     HoursToMinsSkip = (ToHours - FromHours) * 60;
                // else
                //     HoursToMinsSkip = ((24 - FromHours) + ToHours) * 60;
                //
                // int ToMinutes = _TimeToSkipTo._Minutes;
                // int FromMinutes = _TimeOfDay.GetTimeOfDay()._Minutes;
                // int MinsSkip = 0;
                //
                // if (ToMinutes > FromMinutes)
                //     MinsSkip = (ToMinutes - FromMinutes);
                // else
                // {
                //     MinsSkip = ((60 - FromMinutes) + ToMinutes) * 60;
                //     HoursToMinsSkip -= 60;
                // }
                // _MinutesToSkip = HoursToMinsSkip + MinsSkip;



                //if (_TimeToSkipTo.ConvertTimeToSeconds() > _TimeOfDay.GetTimeOfDay().ConvertTimeToSeconds())
                //    _SkipTimeToInMins = _TimeToSkipTo.ConvertTimeToSeconds() / 60;
                //else
                //    _SkipTimeToInMins = (_TimeToSkipTo.ConvertTimeToSeconds() + (86400)) / 60;
                //
                //_CurrentSkipTimeInMins = _TimeOfDay.GetTimeOfDay().ConvertTimeToSeconds() / 60;
                //
                //_bTimeSkipping = true;

                _TimeOfDay.SkipTime(_TimeToSkipTo);
            }

            string HoursText;
            string MinutesText;

            if (_TimeToSkipTo._Hours < 10)
                HoursText = "0" + _TimeToSkipTo._Hours;
            else
                HoursText = _TimeToSkipTo._Hours.ToString();

            if (_TimeToSkipTo._Minutes < 10)
                MinutesText = "0" + _TimeToSkipTo._Minutes;
            else
                MinutesText = _TimeToSkipTo._Minutes.ToString();

            _SkipToTimeText.text = HoursText + " : " + MinutesText;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.gameObject.name == "Player")
        {
            Debug.Log("Player is near the bed!");
            _bCanSleepOnBed = true;
            _PlayerEnteringBed = collision.transform.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.gameObject.name == "Player")
        {
            Debug.Log("player is no longer near the bed!");
            _bCanSleepOnBed = false;
            _PlayerEnteringBed = null;
        }
    }
}
