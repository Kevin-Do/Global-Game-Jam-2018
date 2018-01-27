using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance = null;
	public GameObject Player;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {
		PauseGame();
	}

	void PauseGame()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			var paused = Player.GetComponent<PlayerController>().freezePlayer;
			Player.GetComponent<PlayerController>().freezePlayer = !paused;
		}
	}
}
