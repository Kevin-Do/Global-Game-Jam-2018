using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class QTEController : MonoBehaviour
{
	//Prefab
	public GameObject ArrowPrefab;
	public StrikeZoneController SZC;
	
	//Setup
	public float BPM;
	public float scrollingSpeed;
	
	//Inputs
	public string[] ArrowKeys;
	
	//Enumerate QTE Inputs
	
	void Start()
	{
		SZC = GetComponentInChildren<StrikeZoneController>();
		InvokeRepeating("CreateCommand", 0.0f, BPM);
	}

	public void CreateCommand()
	{
		var arrow = (GameObject)Instantiate(
			ArrowPrefab,
			transform.position,
			transform.rotation);
		//Random.Range(0, 3)
		arrow.GetComponent<ArrowController>().keyCommand = ArrowKeys[0];
		Debug.Log("Creating Arrow with command: " + ArrowKeys[0]);
		arrow.GetComponent<Rigidbody2D>().velocity = Vector2.left * scrollingSpeed;
	}

	private void Update()
	{
		if (Input.anyKeyDown)
		{
			//Debug.Log(SZC.canPress.ToString() + " " + SZC.currentKeyCommand.ToString());
			if (SZC.canPress && Input.GetButtonDown(SZC.currentKeyCommand.ToString()))
			{
				Debug.Log("Correct!");
			}
			else
			{
				Debug.Log("Wrong!");
			}
		}
	}
}
