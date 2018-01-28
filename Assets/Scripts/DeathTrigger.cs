using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{

	public BoxCollider2D bxColl;
	public AudioSource audioSource;
	
	// Use this for initialization
	void Start ()
	{
		bxColl = GetComponent<BoxCollider2D>();
		audioSource = GetComponent<AudioSource>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			GameManager.Instance.LoadScene("Level2");
			audioSource.mute = false;
		}
	}
}
