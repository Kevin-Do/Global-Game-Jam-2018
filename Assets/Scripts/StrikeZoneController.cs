using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikeZoneController : MonoBehaviour
{
	public BoxCollider2D bxColl;

	public bool canPress = false;

	public string currentKeyCommand;

	void Start ()
	{
		bxColl = GetComponent<BoxCollider2D>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		canPress = true;
		currentKeyCommand = other.gameObject.GetComponent<ArrowController>().keyCommand;
		Debug.Log("You can now press!" + currentKeyCommand );

	}

	private void OnTriggerExit2D(Collider2D other)
	{
		Destroy(other.gameObject);
		canPress = false;
	}
}
