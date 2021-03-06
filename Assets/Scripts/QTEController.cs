﻿using System.Collections;
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
	public List<string> ArrowKeys;

	public PlayerController pc;
	public List<FollowerController> followers;
	
	//Enumerate QTE Inputs
	
	void Start()
	{
		SZC = GetComponentInChildren<StrikeZoneController>();
		InvokeRepeating("CreateCommand", 0.0f, BPM);
		followers = new List<FollowerController>();
	}

	public void CreateCommand()
	{
		
		int genKey = Random.Range(0, followers.Count + 1);
		
		Quaternion rot = Quaternion.Euler(0,0,180);
		Vector3 pos = transform.position + new Vector3(0f,2f,0f);
		if (genKey == 1)
		{
			rot = Quaternion.Euler(0,0,270);
			pos = transform.position + new Vector3(0f,0.66f,0f);
		} else if (genKey == 2)
		{
			rot = Quaternion.Euler(0,0,0);
			pos = transform.position + new Vector3(0f,-0.66f,0f);
		} else if (genKey == 3)
		{
			rot = Quaternion.Euler(0,0,90);
			pos = transform.position + new Vector3(0f,-2f,0f);
		}

		var arrow = (GameObject)Instantiate(
			ArrowPrefab,
			pos,
			rot);
		arrow.GetComponent<ArrowController>().keyCommand = ArrowKeys[genKey];
		arrow.GetComponent<Rigidbody2D>().velocity = Vector2.left * scrollingSpeed;
	}

	public void Register(FollowerController follower)
	{
		followers.Add(follower);
	}

	private void Update()
	{

		for (int i = 0; i < ArrowKeys.Count; i++)
		{
			if (Input.GetButtonDown(ArrowKeys[i]))
			{
				if (SZC.canPress && Input.GetButtonDown(SZC.currentKeyCommand.ToString()))
				{
					SZC.Correct();
					
				}
				else
				{
					Debug.Log("Wrong!");
				}
			}
			
		}
	}
}
