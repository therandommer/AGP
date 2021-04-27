using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomBattle : MonoBehaviour
{
    public int battleProbability;
    int encounterChance = 100;
    public int secondsBetweenBattles;
    public string battleSceneName;

    List<GameObject> EnemyList;
    public GameObject[] DayTimeEnemyPrefabs;
    public GameObject[] NightTimeEnemyPrefabs;


    void OnTriggerEnter2D(Collider2D col)
    {
        if (!GameState.justExitedBattle)
        {
            encounterChance = Random.Range(1, 100);
            if (encounterChance > battleProbability)
            {
                StartCoroutine(RecalculateChance());
            }
        }
        else
        {
            StartCoroutine(RecalculateChance());
            GameState.justExitedBattle = false;
        }
    }


    IEnumerator RecalculateChance()
    {
        while (encounterChance > battleProbability)
        {
            yield return new WaitForSeconds(secondsBetweenBattles);
            encounterChance = Random.Range(1, 100);
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (encounterChance <= battleProbability)
        {
            Debug.Log("Battle");
            GameObject.Find("Player").GetComponent<PlayerController>().LastScenePosition = GameObject.Find("Player").transform.position;

            int NumOfEnemies = Random.Range(1, 9);

            for (int i = 0; i < NumOfEnemies; i++)
            {
                int RandomDayEnemy = Random.Range(1, DayTimeEnemyPrefabs.Length);
                int RandomNightEnemy = Random.Range(1, NightTimeEnemyPrefabs.Length);

                if (GameState.Time.GetTimeOfDay()._Hours > 6 && GameState.Time.GetTimeOfDay()._Hours < 18)
                {
                    EnemyList.Add(DayTimeEnemyPrefabs[RandomDayEnemy]);
                }
                else
                {
                    EnemyList.Add(NightTimeEnemyPrefabs[RandomNightEnemy]);
                }
            }
            GameState.EnemyPrefabsForBattle = EnemyList.ToArray();

            SceneManager.LoadScene(battleSceneName);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        encounterChance = 100;
        StopCoroutine(RecalculateChance());
    }

}
