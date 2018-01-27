using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {
	
	//Follower Factors
	public Transform playerPosition;

	public bool isFollowing = false;

	private List<Vector3> pastPositions;
	private int numOffset;

	
	void Awake () {
		pastPositions = new List<Vector3>();
//		player = GameObject.Find("Player").GetComponent<PlayerController>("PlayerController");
	}
	
	// Use this for initialization
	void Start () {
//		
	}

	void SavePosition()
	{
		if (isFollowing)
		{
			Vector3 playerPosition = GameObject.Find("Player").transform.position;

			if (pastPositions.Count > 0)
			{
				if (pastPositions[0] != playerPosition)
				{
					pastPositions.Insert(0, playerPosition);
				} 
			}
			else
			{
				pastPositions.Insert(0, playerPosition);
			}
		
			if (pastPositions.Count > numOffset)
			{
				pastPositions.RemoveAt(pastPositions.Count - 1);
			}
		}
		
	}
	
	void Follow()
	{
		
		if (isFollowing && pastPositions.Count > 0) {
			Vector3 lastPosition = pastPositions[pastPositions.Count - 1];
			Vector3 currentPosition = transform.position;

			transform.position = (lastPosition + currentPosition * 3) / 4;

		}
	}

	// Update is called once per frame
	void Update () {
		
		SavePosition();
		Follow();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		
		if (!isFollowing && other.gameObject.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
			numOffset = 10 + 10 * player.followers;
			Debug.Log(player.followers);
			player.followers += 1;
			isFollowing = true;
			Debug.Log(player.followers);
		}
	}
}
