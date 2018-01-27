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
	private bool canJump;
	public bool freezePlayer;
	public int followers;
	
	//Jump Variables
	public int jumpCount = 0;
	public int jumpLimit = 1;

	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		followers = 0;
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
		if (Input.GetButtonDown("Jump") && jumpCount < jumpLimit && canJump)
		{
			jumpCount++;
			Vector2 movement = Vector2.up * playerJumpForce;
			rb.velocity = new Vector2(rb.velocity.x, (float) 0.1);
			rb.AddForce(movement * playerSpeed);
		}
	}

	void OnCollisionStay2D(Collision2D col) {
		if (col.gameObject.tag == "Floor") {
			canJump = true;
			jumpCount = 0;
		}
	}
	
	
}
