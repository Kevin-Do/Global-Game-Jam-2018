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

	public void PauseGame()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			Paused = !Paused;
			PauseUI.SetActive(Paused);
			Player.GetComponent<PlayerController>().freezePlayer = Paused;
		}
	}

	public void ReturnToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
	
	public void LoadScene(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	
	public void Quit()
	{
		Application.Quit();
	}
}
