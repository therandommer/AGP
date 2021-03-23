using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeOfDay", menuName = "ScriptableObjects/WeatherDamageMultipliers")]
public class WeatherDamageMultipliers : ScriptableObject
{
    [System.NonSerialized] public float _PhysicalDamage = 1.0f;
    [System.NonSerialized] public float _WaterDamage = 1.0f;
    [System.NonSerialized] public float _LightningDamage = 1.0f;
    [System.NonSerialized] public float _FireDamage = 1.0f;
   // [System.NonSerialized] public float _SnowDamage = 1.0f;

    public void UpdateDamages(EWeatherTypes WT, int Temp)
    {
        ResetDamages();

        switch(WT)
        {
            case EWeatherTypes.EWT_Sunny:
                _FireDamage = 1.1f;
                _FireDamage += (0.05f) * (Temp - 13);
               // _SnowDamage = 0.5f;
                _WaterDamage = 0.7f;
                Debug.Log("Fire Damage = " + _FireDamage + " | Water Damage = " + _WaterDamage);
                break;
            case EWeatherTypes.EWT_Cloudy:
                break;
            case EWeatherTypes.EWT_Drizzle:
                _FireDamage = 0.85f;
                _WaterDamage = 1.05f;
                Debug.Log("Fire Damage = " + _FireDamage + " | Water Damage = " + _WaterDamage);
                break;
            case EWeatherTypes.EWT_Raining:
                _FireDamage = 0.75f;
                _WaterDamage = 1.15f;
                Debug.Log("Fire Damage = " + _FireDamage + " | Water Damage = " + _WaterDamage);
                break;
            case EWeatherTypes.EWT_Snowing:
                _FireDamage = 0.85f;
                _WaterDamage = 1.15f;
                _WaterDamage += (0.01f) * (Temp * -1);
                _FireDamage -= (0.01f) * (Temp * -1);
                Debug.Log("Fire Damage = " + _FireDamage + " | Water Damage = " + _WaterDamage);
                break;
            case EWeatherTypes.EWT_ThunderStorm:
                _FireDamage = 0.8f;
                _LightningDamage = 1.5f;
                _WaterDamage = 1.15f;
                Debug.Log("Fire Damage = " + _FireDamage + " | Water Damage = " + _WaterDamage
                     + " | Physical Damage = " + _PhysicalDamage +" | Lightning Damage = " + _LightningDamage);
                break;
            case EWeatherTypes.EWT_SnowStorm:
                _FireDamage = 0.8f;
                _PhysicalDamage = 0.75f;
                _WaterDamage = 1.15f;
                _WaterDamage += (0.01f) * (Temp * -1);
                _FireDamage -= (0.01f) * (Temp * -1);
                Debug.Log("Fire Damage = " + _FireDamage + " | Water Damage = " + _WaterDamage
                    + " | Physical Damage = " + _PhysicalDamage);
                break;
        }
    }

    private void ResetDamages()
    {
        _PhysicalDamage = 1.0f;
        _WaterDamage = 1.0f;
        _LightningDamage = 1.0f;
        _FireDamage = 1.0f;
        //_SnowDamage = 1.0f;
    }
}
