using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherUI : MonoBehaviour
{
    [SerializeField] private WeatherSO _WeatherSO;
    [SerializeField] private GameObject _SunnyUI;
    [SerializeField] private GameObject _CloudyUI;
    [SerializeField] private GameObject _DrizzleUI;
    [SerializeField] private GameObject _RainingUI;
    [SerializeField] private GameObject _SnowingUI;
    [SerializeField] private GameObject _ThunderStormUI;
    [SerializeField] private GameObject _SnowStormUI;
    
    void Start()
    {
        HideUI();
    }

    private void Awake()
    {
        ShowRightUI();
    }

    // Update is called once per frame
    void Update()
    {
        HideUI();
        ShowRightUI();
    }

    void ShowRightUI()
    {
        switch(_WeatherSO._CurrentWeather)
        {
            case EWeatherTypes.EWT_Sunny:
                _SunnyUI.SetActive(true);
                break;
            case EWeatherTypes.EWT_Cloudy:
                _CloudyUI.SetActive(true);
                break;
            case EWeatherTypes.EWT_Drizzle:
                _DrizzleUI.SetActive(true);
                break;
            case EWeatherTypes.EWT_Raining:
                _RainingUI.SetActive(true);
                break;
            case EWeatherTypes.EWT_Snowing:
                _SnowingUI.SetActive(true);
                break;
            case EWeatherTypes.EWT_ThunderStorm:
                _ThunderStormUI.SetActive(true);
                break;
            case EWeatherTypes.EWT_SnowStorm:
                _SnowStormUI.SetActive(true);
                break;

        }
    }

   void HideUI()
    {
        _SunnyUI.SetActive(false);
        _CloudyUI.SetActive(false);
        _DrizzleUI.SetActive(false);
        _RainingUI.SetActive(false);
        _SnowingUI.SetActive(false);
        _ThunderStormUI.SetActive(false);
        _SnowStormUI.SetActive(false);
    }
}
