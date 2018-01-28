using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
	public BoxCollider2D bxColl;

	public string keyCommand;
	// Use this for initialization
	void Start ()
	{
		bxColl = GetComponent<BoxCollider2D>();
	}
	
	
}
