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

    [Header("6am - 6pm enemies")]
    public List<GameObject> DayTimeEnemyPrefabs = new List<GameObject>();
    [Header("6:01pm - 5:59am enemies")]
    public List<GameObject> NightTimeEnemyPrefabs = new List<GameObject>();
    [Header("Use this for Boss encounters or Tutorial fights")]
    public List<GameObject> GuarnteedEnemies = new List<GameObject>();
    [Header("If you don't want other enemies in the encounter")]
    public bool OnlyGuaranteedEnemies = false;

    public List<GameObject> EnemyList = new List<GameObject>();

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
            GameState.CurrentPlayer.LastScenePosition = GameState.CurrentPlayer.gameObject.transform.position;

            GameState.CurrentPlayer.LastSceneName = SceneManager.GetActiveScene().name;
            foreach (GameObject Enemy in GuarnteedEnemies)
            {
                EnemyList.Add(Enemy);
            }
            if (!OnlyGuaranteedEnemies)
            {
                int NumOfEnemies = Random.Range(1, 9 - GuarnteedEnemies.Count);
                for (int i = 0; i < NumOfEnemies; i++)
                {
                    int RandomDayEnemy = Random.Range(1, DayTimeEnemyPrefabs.Count);
                    int RandomNightEnemy = Random.Range(1, NightTimeEnemyPrefabs.Count);

                    if (GameState.Time.GetTimeOfDay()._Hours > 6 && GameState.Time.GetTimeOfDay()._Hours < 18)
                    {
                        EnemyList.Add(DayTimeEnemyPrefabs[RandomDayEnemy]);
                    }
                    else
                    {
                        EnemyList.Add(NightTimeEnemyPrefabs[RandomNightEnemy]);
                    }
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
