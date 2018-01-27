using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager Instance = null;
	public GameObject Player;
	public GameObject PauseUI;
	public bool Paused = false;
	
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
		
		//Handle Pause Menu
		PauseGame();
	}

	void PauseGame()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Paused = !Paused;
			PauseUI.SetActive(Paused);
			Player.GetComponent<PlayerController>().freezePlayer = Paused;
		}
	}

	void ReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
	void Quit()
	{
		Application.Quit();
	}
}
