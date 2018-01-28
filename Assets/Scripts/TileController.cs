using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{


	private Color c;
	private float alpha;
	private int collisions;
	public SpriteRenderer sr;
	
	
	void Awake ()
	{
		alpha = 0;
		c = new Color(0, 0, 0, alpha);
		collisions = 0;
		sr = GetComponent<SpriteRenderer>();
		sr.color = c;
	}
	
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void SetColor(Color c)
	{
		sr.color = c;
	}
	
	void OnCollisionEnter2D(Collision2D col) {
		if (gameObject.layer == 8 && col.gameObject.tag == "SoundParticle" && alpha < 200)
		{
			particleController pc = col.gameObject.GetComponent<particleController>() as particleController;
			collisions += 1;
			Color pcolor = pc.color;
			alpha += 0.01f;
			c = (c*alpha + pcolor*0.01f)/(alpha + 0.01f);
			c.a = alpha;
			SetColor(c);
		}
	}
}
