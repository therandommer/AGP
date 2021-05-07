using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckForFinalBossFight : MonoBehaviour
{
	public List<GameObject> BossFightMinions = new List<GameObject>();
	public Sprite BossResponceSprite;

	void OnTriggerEnter2D(Collider2D col)
	{
			
		if(GameState.NumberOfBossesDefeated > GameState.NumberofBossesNeededToFightFinalBoss)
        {
			GameState.EnemyPrefabsForBattle = BossFightMinions.ToArray();

			SceneManager.LoadScene("TownBattle");
		}
		else
        {
			ShowMessage.Instance.StartCouroutineForMessage("Cannot Enter!", "You see " + (GameState.NumberofBossesNeededToFightFinalBoss - GameState.NumberOfBossesDefeated) + " magical seals barring you from entry", BossResponceSprite, 2f);
        }
	}

	public void TestFinalBossFight()
    {
		GameState.EnemyPrefabsForBattle = BossFightMinions.ToArray();
		GameState.PlayerLoc = PlayerLocation.Center;
		SceneManager.LoadScene("TownBattle");
	}
}
