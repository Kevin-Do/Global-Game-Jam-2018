using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{


	public float duration;

	// Use this for initialization
	void Start ()
	{
		StartCoroutine(waitTilEnd());
	}
	

	IEnumerator waitTilEnd()
	{
		yield return new WaitForSeconds(duration);
		Application.Quit();
	}

}
