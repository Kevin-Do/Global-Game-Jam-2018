using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleController : MonoBehaviour {

	public Rigidbody2D rb;
	// Use this for initialization
	public Collider2D cd;

	private particleController left;
	private particleController right;
	private LineRenderer leftLine;
	public float cutoff;
	private float strength;
	public Color color;

	void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		cd = GetComponent<Collider2D>();
		cutoff = 2.0f;
		strength = 1.0f;
	}

	public void setStrength(float s)
	{
		strength = s;
	}

	public void Connect(particleController p, Color c)
	{
		p.right = gameObject.GetComponent<particleController>();
		left = p;
		leftLine = gameObject.GetComponent<LineRenderer>();
		leftLine.material.color = c;
		color = c;
		

	}

	void DrawLine()
	{
		if (leftLine != null)
		{
			Vector3 leftPos = left.GetComponent<Transform>().position;
			if ((leftPos - transform.position).magnitude > cutoff)
			{
				Destroy(leftLine);
				leftLine = null;
			} else
			{
				leftLine.SetPosition(0,leftPos);
				leftLine.SetPosition(1,transform.position);
				Color currColor = leftLine.material.color;
				currColor.a -= Time.deltaTime / strength;
				leftLine.material.color = currColor;
				leftLine.SetColors( currColor, currColor);
			}
		}
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		DrawLine();
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer != 8)
		{
			Physics2D.IgnoreCollision(collision.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		}
	}
}
