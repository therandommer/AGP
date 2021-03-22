using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeOfDay", menuName = "ScriptableObjects/WeatherSO")]
public class WeatherSO : ScriptableObject
{
    public EWeatherTypes _CurrentWeather;
    public int _Temperature;
}
