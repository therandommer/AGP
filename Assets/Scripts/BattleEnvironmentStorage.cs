using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnvironmentStorage : MonoBehaviour
{

    public GameObject NorthCaveDay;
    public GameObject NorthCaveNight;

    public GameObject EastSwampDay;
    public GameObject EastSwampNight;

    public GameObject SouthForestDay;
    public GameObject SouthForestNight;

    public GameObject WestDesertDay;
    public GameObject WestDesertNight;

    public GameObject TownNight;
    public GameObject FinalFight;


    public GameObject CheckTimeForBackground(int HourOfTheDay)
    {
        if(HourOfTheDay > 6 && HourOfTheDay < 18)
        {
            switch (GameState.PlayerLoc)
            {
                case PlayerLocation.North:
                    return NorthCaveDay;
                case PlayerLocation.South:
                    return SouthForestDay;
                case PlayerLocation.East:
                    return EastSwampDay;
                case PlayerLocation.West:
                    return WestDesertDay;
                case PlayerLocation.Center:
                    return FinalFight;
            }
        }
        else
        {
            switch (GameState.PlayerLoc)
            {
                case PlayerLocation.North:
                    return NorthCaveNight;
                case PlayerLocation.South:
                    return SouthForestNight;
                case PlayerLocation.East:
                    return EastSwampNight;
                case PlayerLocation.West:
                    return WestDesertNight;
                case PlayerLocation.Center:
                    return FinalFight;
            }
        }
        return TownNight;
    }



}
