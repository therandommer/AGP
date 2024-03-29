﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EWeatherTypes
{
    EWT_Sunny,
    EWT_Cloudy,
    EWT_Drizzle,
    EWT_Raining,
    EWT_Snowing,
    EWT_ThunderStorm,
    EWT_SnowStorm,
};

public class WeatherSystem : MonoBehaviour
{

    [SerializeField] private WeatherSO _WeatherSO;
    [SerializeField] private WeatherDamageMultipliers _WDM;
    [SerializeField] private TimeOfDay _TimeOfDay;
    [SerializeField] private float _WeatherChangeRangeMin = 60;
    [SerializeField] private float _WeatherChangeRangeMax = 120;
    [SerializeField, Range(1, 100)] private int _ChanceForWeatherToChange = 25;

    [Header("Particles & Sprites")]
    [SerializeField] private GameObject _DrizzlePS;
    [SerializeField] private GameObject _RainingPS;
    [SerializeField] private GameObject _SnowPS;
    [SerializeField] private GameObject _SnowStormPS;
    [SerializeField] private GameObject _CloudsSprites;
    [SerializeField] private GameObject _LightningSprite;

    /** In Celsius */
    private int _Temperature = 0;
    private bool _bIsFoggy = false;
    private float _WeatherCheckTimer = 0;
    private EWeatherTypes _CurrentWeather;

    // Start is called before the first frame update
    void Start()
    {
        _WeatherCheckTimer = Random.Range(_WeatherChangeRangeMin, _WeatherChangeRangeMax);
        CheckForWeatherChange();

        
    }

    private void Awake()
    {
        GameObject[] WSs = GameObject.FindGameObjectsWithTag("WeatherSystem");
        if (WSs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        _WeatherCheckTimer -= Time.deltaTime;

        if (_WeatherCheckTimer <= 0)
            CheckForWeatherChange();

        if(_TimeOfDay.GetTimeOfDay()._Hours > 19 && _CurrentWeather == EWeatherTypes.EWT_Sunny)
        {
            NightTimeWeatherCheck();
        }
    }

    void CheckForWeatherChange()
    {
        HideAllSystems();

        if (Random.Range(0, 100) <= _ChanceForWeatherToChange)
        {
            int Hours = _TimeOfDay.GetTimeOfDay()._Hours;

            if (Hours > 5 && Hours < 9)
                MorningWeatherCheck();
            else if (Hours < 19)
                EveningWeatherCheck();
            else
                NightTimeWeatherCheck();

        }
        else
            Debug.Log("Weather_System: Weather Didnt Change!");

        _WeatherSO._CurrentWeather = _CurrentWeather;
        _WeatherSO._Temperature = _Temperature;
        _WeatherCheckTimer = Random.Range(_WeatherChangeRangeMin, _WeatherChangeRangeMax);

        switch(_CurrentWeather)
        {
            case EWeatherTypes.EWT_Sunny:
                HideAllSystems();
                break;
            case EWeatherTypes.EWT_Cloudy:
                _CloudsSprites.SetActive(true);
                break;
            case EWeatherTypes.EWT_Drizzle:
                _CloudsSprites.SetActive(true);
                _DrizzlePS.SetActive(true);
                break;
            case EWeatherTypes.EWT_Raining:
                _CloudsSprites.SetActive(true);
                _RainingPS.SetActive(true);
                break;
            case EWeatherTypes.EWT_Snowing:
                _CloudsSprites.SetActive(true);
                _SnowPS.SetActive(true);
                break;
            case EWeatherTypes.EWT_ThunderStorm:
                _CloudsSprites.SetActive(true);
                _RainingPS.SetActive(true);
                break;
            case EWeatherTypes.EWT_SnowStorm:
                _CloudsSprites.SetActive(true);
                _SnowStormPS.SetActive(true);
                break;
        }

        _WDM.UpdateDamages(_CurrentWeather, _Temperature);
    }

    void MorningWeatherCheck()
    {
        int RanWeather = Random.Range(0, 100);

        if(RanWeather <= 30)
        {
            //Sunny Weather
            _CurrentWeather = EWeatherTypes.EWT_Sunny;
            _Temperature = Random.Range(13, 30);
        }
        else if (RanWeather <= 60)
        {
            //Cloudy
            _CurrentWeather = EWeatherTypes.EWT_Cloudy;
            _Temperature = Random.Range(5, 10);
        }
        else if (RanWeather <= 75)
        {
            //Drizzle
            _CurrentWeather = EWeatherTypes.EWT_Drizzle;
            _Temperature = Random.Range(4, 9);
        }
        else if (RanWeather <= 90)
        {
            //Raining
            _CurrentWeather = EWeatherTypes.EWT_Raining;
            _Temperature = Random.Range(1, 7);
        }
        else if (RanWeather <= 96)
        {
            //Snowing
            _CurrentWeather = EWeatherTypes.EWT_Snowing;
            _Temperature = Random.Range(-10, 0);
        }
        else if (RanWeather <= 100)
        {
            //Storm
            if(RanWeather <= 98)
            {
                //ThunderStorm
                _CurrentWeather = EWeatherTypes.EWT_ThunderStorm;
                _Temperature = Random.Range(13, 30);
            }
            else
            {
                //SnowStorm
                _CurrentWeather = EWeatherTypes.EWT_SnowStorm;
                _Temperature = Random.Range(-35, -10);
            }
        }

        Debug.Log("Weather_System: Weather Changed To " + _CurrentWeather + "  " + _Temperature + "C");
    }
    void NightTimeWeatherCheck()
    {
        int RanWeather = Random.Range(0, 100);

        if (RanWeather <= 15)
        {
            //Cloudy
            _CurrentWeather = EWeatherTypes.EWT_Cloudy;
            _Temperature = Random.Range(5, 10);
        }
        else if (RanWeather <= 50)
        {
            //Drizzle
            _CurrentWeather = EWeatherTypes.EWT_Drizzle;
            _Temperature = Random.Range(4, 9);
        }
        else if (RanWeather <= 80)
        {
            //Raining
            _CurrentWeather = EWeatherTypes.EWT_Raining;
            _Temperature = Random.Range(1, 7);
        }
        else if (RanWeather <= 90)
        {
            //Snowing
            _CurrentWeather = EWeatherTypes.EWT_Snowing;
            _Temperature = Random.Range(-10, 0);
        }
        else if (RanWeather <= 100)
        {
            //Storm
            if (RanWeather <= 98)
            {
                //ThunderStorm
                _CurrentWeather = EWeatherTypes.EWT_ThunderStorm;
                _Temperature = Random.Range(13, 30);
            }
            else
            {
                //SnowStorm
                _CurrentWeather = EWeatherTypes.EWT_SnowStorm;
                _Temperature = Random.Range(-35, -10);
            }
        }

        Debug.Log("Weather_System: Weather Changed To " + _CurrentWeather + "  " + _Temperature + "C");
    }
    void EveningWeatherCheck()
    {
        int RanWeather = Random.Range(0, 100);

        if (RanWeather <= 20)
        {
            //Sunny Weather
            _CurrentWeather = EWeatherTypes.EWT_Sunny;
            _Temperature = Random.Range(13, 30);
        }
        else if (RanWeather <= 40)
        {
            //Cloudy
            _CurrentWeather = EWeatherTypes.EWT_Cloudy;
            _Temperature = Random.Range(5, 10);
        }
        else if (RanWeather <= 60)
        {
            //Drizzle
            _CurrentWeather = EWeatherTypes.EWT_Drizzle;
            _Temperature = Random.Range(4, 9);
        }
        else if (RanWeather <= 80)
        {
            //Raining
            _CurrentWeather = EWeatherTypes.EWT_Raining;
            _Temperature = Random.Range(1, 7);
        }
        else if (RanWeather <= 95)
        {
            //Snowing
            _CurrentWeather = EWeatherTypes.EWT_Snowing;
            _Temperature = Random.Range(-10, 0);
        }
        else if (RanWeather <= 100)
        {
            //Storm
            if (RanWeather <= 98)
            {
                //ThunderStorm
                _CurrentWeather = EWeatherTypes.EWT_ThunderStorm;
                _Temperature = Random.Range(13, 30);
            }
            else
            {
                //SnowStorm
                _CurrentWeather = EWeatherTypes.EWT_SnowStorm;
                _Temperature = Random.Range(-35, -10);
            }
        }

        Debug.Log("Weather_System: Weather Changed To " + _CurrentWeather + "  " + _Temperature + "C");
    }

    void HideAllSystems()
    {
        _DrizzlePS.SetActive(false);
        _RainingPS.SetActive(false);
        _SnowPS.SetActive(false);
        _SnowStormPS.SetActive(false);
        _CloudsSprites.SetActive(false);
        _LightningSprite.SetActive(false);
    }
}
