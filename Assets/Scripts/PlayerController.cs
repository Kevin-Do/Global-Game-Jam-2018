using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	//Components
	public Rigidbody2D rb;
	
	
	//Player Factors
	public float playerSpeed;
	public float playerJumpForce;
	public bool onFloor;
	private bool canEmit;
	public bool freezePlayer;
	public int followers;
	
	//Jump Variables
	public int jumpCount = 0;
	public int jumpLimit = 1;
	
	public particleController particlePrefab;
	public float soundSpeed;
	public Color soundColor;
	public int numSoundParticles;
	public float lifetimeSoundParticles;
	
	
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		followers = 0;
		onFloor = false;
		canEmit = true;
	}
	
	
	void Update()
	{
		//Handle Pause Menu
		if (freezePlayer)
		{
			return;
		}
		
		//Handle Jumping
		Jump();

		//Handle Movement
		Move();
		
		//Handle Emitting a wave
		SoundEmit();
	}

	void Move() 
	{
		float moveHorizontal = Input.GetAxisRaw("Horizontal");
		rb.velocity = new Vector2(moveHorizontal * playerSpeed, rb.velocity.y);

		//Handle facing left/right
		if (moveHorizontal != 0f && (moveHorizontal < 0f) != transform.localScale.x < 0) 
		{
			Flip();
		}
	}
	
	void Flip() 
	{
		Vector3 currentScale = transform.localScale;
		currentScale.x *= -1;
		transform.localScale = currentScale;
	}

	void Jump()
	{
		if (Input.GetButtonDown("Jump") && jumpCount < jumpLimit && onFloor)
		{
			jumpCount++;
			Vector2 movement = Vector2.up * playerJumpForce;
			rb.velocity = new Vector2(rb.velocity.x, (float) 0.1);
			rb.AddForce(movement * playerSpeed);
			
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
			pc.setStrength(3 / lifetimeSoundParticles );
			
			Destroy (pc.gameObject , lifetimeSoundParticles);
		}
		if (loop)
		{
			prev.Connect(first, soundColor);
		}
	}
	
	void SoundEmit()
	{
		if (Input.GetButtonDown("Emit0") && canEmit)
		{

			if (onFloor)
			{
				CreateParticles(Mathf.PI, (int) numSoundParticles / 2, false);
			}
			else
			{
				CreateParticles(Mathf.PI * 2, (int) numSoundParticles, true);
			}
			
			
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Floor") {
			onFloor = true;
			jumpCount = 0;
		}
	}
	
	void OnCollisionExit2D(Collision2D col) {
		if (col.gameObject.tag == "Floor") {
			onFloor = false;
		}
	}
	
	
}
