using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationPrompt : MonoBehaviour
{
    public Vector3 startingPosition;
	/*
	void OnCollisionEnter2D(Collision2D col)
	{
		if (NavigationManager.CanNavigate(this.tag))
		{
			Debug.Log("attempting to exit via " + tag);

			var lastPosition = GameState.GetLastScenePosition(this.tag);

			Debug.Log("Last know pos for " + this.tag + " is " + lastPosition);

			if (lastPosition != Vector3.zero)
			{
				GameState.CurrentPlayer.gameObject.transform.position = lastPosition;
			}
			else
            {
				Debug.Log("Set pos to 0");
				GameState.CurrentPlayer.gameObject.transform.position = Vector3.zero;
			}
			GameState.saveLastPosition = false;
			GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, startingPosition);

			NavigationManager.NavigateTo(this.tag);

		}
	}
	*/
	void OnTriggerEnter2D(Collider2D col)
	{
		Debug.Log("attempting to exit via " + tag);
		GameState.CurrentPlayer.EnemyToDelete.Clear();
		var lastPosition = GameState.GetLastScenePosition(this.tag);

		Debug.Log("Last know pos for " + this.tag + " is " + lastPosition);

		if (lastPosition != Vector3.zero)
		{
			GameState.CurrentPlayer.gameObject.transform.position = lastPosition;
			GameState.saveLastPosition = false;
		}
		else
		{
			Debug.Log("Set pos to 0");
			GameState.saveLastPosition = true;
			GameState.CurrentPlayer.gameObject.transform.position = Vector3.zero;
		}
		if(GameState.saveLastPosition != false)
        {
			GameState.SetLastScenePosition(SceneManager.GetActiveScene().name, startingPosition);
        }

		GameState.CurrentPlayer.LastSceneName = SceneManager.GetActiveScene().name;

		NavigationManager.NavigateTo(this.tag);

	}


}
