using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerController : MonoBehaviour {
	
	//Follower Factors
	public Transform playerPosition;
	public QTEController qte;

	public bool isFollowing = false;

	private List<Vector3> pastPositions;
	private List<bool> pastFloors;
	private int numOffset;
	public int spacing;
	private int index;

	public Animator Animator;
	public particleController particlePrefab;
	public float soundSpeed;
	public Color soundColor;
	public int numSoundParticles;
	public float lifetimeSoundParticles;
	public float soundStrength;
	public float cooldown;
	

	private bool canEmit;
	private bool onFloor;
	private bool standingStill;
	
	
	void Awake () {
		pastPositions = new List<Vector3>();
		pastFloors = new List<bool>();
		Animator = GetComponent<Animator>();
		spacing = 8;
		canEmit = true;
	}

	void SavePosition()
	{
		if (isFollowing)
		{
			Vector3 playerPosition = GameObject.Find("Player").transform.position;

			if (pastPositions.Count > 0)
			{
				if ((pastPositions[0] - playerPosition).magnitude > 0.00001 )
				{
					pastPositions.Insert(0, playerPosition);
					pastFloors.Insert(0, (GameObject.Find("Player").GetComponent<PlayerController>() as PlayerController).onFloor);
				}
				
			}
			else
			{
				pastPositions.Insert(0, playerPosition);
				pastFloors.Insert(0, (GameObject.Find("Player").GetComponent<PlayerController>() as PlayerController).onFloor);
			}
		
			if (pastPositions.Count > numOffset)
			{
				pastPositions.RemoveAt(pastPositions.Count - 1);
				pastFloors.RemoveAt(pastFloors.Count - 1);
			}
		}
		
	}
	
	void Follow()
	{
		
		if (isFollowing && pastPositions.Count > 0) {
			Vector3 lastPosition = pastPositions[pastPositions.Count - 1];
			Vector3 currentPosition = transform.position;
			standingStill = (lastPosition - currentPosition).magnitude < 0.001f;

			if (!standingStill)
			{
				Animator.SetBool("FollowerWalk", onFloor);
				transform.position = (lastPosition + currentPosition * 3) / 4;
				onFloor = pastFloors[pastFloors.Count - 1];
			}
			else
			{
				Animator.SetBool("FollowerWalk", false);
			}

		}
	}

	// Update is called once per frame
	void Update () {
		
		SavePosition();
		Follow();
		if (!standingStill)
		{
			Animator.SetBool("FollowerJump", !onFloor);
		}

		if (isFollowing && canEmit)
		{
			StartCoroutine(SoundEmit());
		}
		
	}
	
	private void CreateParticles(float angle, int numSoundParticles, bool loop)
	{
		
		particleController prev = null;
		particleController first = null;
		for (float i = 0; i < angle; i += (float) angle/numSoundParticles)
		{
			
			particleController pc = Instantiate(particlePrefab) as particleController;
			if (prev != null)
			{
				prev.Connect(pc, soundColor);
			}
			else
			{
				first = pc;
			}
			prev = pc;
			
			Physics2D.IgnoreCollision(pc.GetComponent<Collider2D>(), GetComponent<Collider2D>());
			Vector2 dir =  new Vector2( Mathf.Cos(i) , Mathf.Sin(i));
			pc.rb.velocity = dir * soundSpeed;
			pc.transform.position = transform.position;
			pc.setStrength(soundStrength);
			
			Destroy (pc.gameObject , lifetimeSoundParticles);
		}
		if (loop)
		{
			prev.Connect(first, soundColor);
		}
	}
	
	IEnumerator SoundEmit()
	{
		canEmit = false;
		CreateParticles(Mathf.PI * 2, (int) numSoundParticles, true);
		yield return new WaitForSeconds(cooldown);
		canEmit = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (!isFollowing && other.gameObject.tag == "Player")
		{
			PlayerController player = other.gameObject.GetComponent(typeof(PlayerController)) as PlayerController;
			numOffset = spacing +  spacing * player.followers;
			index = player.followers + 1;
			player.followers += 1;
			isFollowing = true;
			qte.Register(this);
		}
		if (other.gameObject.tag == "Floor") {
			onFloor = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Floor") {
			onFloor = false;
		}
	}
}
