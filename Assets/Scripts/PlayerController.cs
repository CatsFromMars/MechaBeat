using UnityEngine;
using System.Collections;
using Rhythmify;

public class PlayerController : _AbstractRhythmObject
{
	
		private GameObject controller;
		private HashIDs hash;
		//private Animator animator;
	
		private int playerSpeed = 5;
		private Vector3 moveDirection;
		public bool canDoubleJump = false;
		public float maxJumpForce = 1500f;          // Amount of force added when the player jumps.
		private float currentJumpForce = 0f;
		private float doubleJumpForce; // amount of force added when player jumps in middle of jump
		public float moveForce = 365f;          // Amount of force added to move the player left and right.
		
		int beat;
		private bool facingRight = true;
		public float maxDashTime = 1.0f;
		public float dashSpeed = 500.0f;
		public float dashStoppingSpeed = 0.1f;
		private float currentDashTime;
		private bool canDash = true;
	
		void Awake ()
		{
				moveDirection = Vector3.zero;
				controller = GameObject.FindGameObjectWithTag ("GameController");
				hash = controller.GetComponent<HashIDs> ();
				//animator = GetComponent<Animator>();
				currentDashTime = maxDashTime;
				doubleJumpForce = maxJumpForce * 0.6f;
		}
	
		private float gravity = 9.8f;
		Transform gravityCenter;
		private float horiz; //Horizontal Axis (Plauer Input)
		private float vert; //Vertical Axis (Player Input)
	
	
		override protected void rhythmUpdate (int beat)
		{
				//Sync player animations to the music
				//if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) {
				//animator.SetTrigger(hash.beatTrigger);
				//}
		}
	
		override protected void asyncUpdate ()
		{
				//Actual Player Controller Jazz
				MovePlayer ();
		
				if (gameObject.transform.position.y < -10) {
						gameObject.transform.position = new Vector3 (-10, 5, 0);
				}
		
		}
	
		private float maxSpeed = 5.0f;
		private int jumpCount = 0;

		void MovePlayer ()
		{
				//Note: Player Movement Assumes InputAxisRaw and NOT InputAxis
				//Rotate Player and handle Running Animation
				float horiz = Input.GetAxisRaw ("Horizontal");
		
				if (horiz > 0) {
						transform.rotation = Quaternion.Euler (0, 90, 0);
						//animator.SetBool(hash.runningBool, true);
						facingRight = true;
				} else if (horiz < 0) {
						transform.rotation = Quaternion.Euler (0, -90, 0);
						//animator.SetBool(hash.runningBool, true);
						facingRight = false;
				} else if (horiz == 0) {
						//animator.SetBool(hash.runningBool, false);
				}
		
				//Update the horizontal movement vector
		
				if (horiz != 0) {
						rigidbody.AddForce (Vector3.right * horiz * moveForce / 2);
				}
				
				if (Mathf.Abs (rigidbody.velocity.y) < 0.1)
						jumpCount = 0;
				
				if (Input.GetKeyDown (KeyCode.Space)) {
						Debug.Log (rigidbody.velocity.y);
						if (jumpCount == 1 && canDoubleJump) {
								moveDirection = Vector3.up;
								canDoubleJump = false;
								currentJumpForce = 100.0f;
								jumpCount = 2;
								
						} else if (jumpCount == 0 && Mathf.Abs (rigidbody.velocity.y) < 0.1) { // normal jump
								//animator.SetBool(hash.jumpBool, true);
								moveDirection = Vector3.up;
								canDoubleJump = true;
								currentJumpForce = 100.0f;
								jumpCount = 1; 
						} else if (jumpCount == 0 && rigidbody.velocity.y < -0.1) {
								moveDirection = Vector3.up;
								canDoubleJump = false;
								currentJumpForce = 100.0f;
								jumpCount = -1; 
						}
			
			
				}
				
				if (jumpCount != 0 && currentJumpForce <= maxJumpForce) {
			
						rigidbody.AddForce (Vector3.up * 200.0f, ForceMode.Acceleration); 
						currentJumpForce += 200.0f;
				}
		
				rigidbody.velocity = new Vector3 (
			Mathf.Clamp (rigidbody.velocity.x, -maxSpeed, maxSpeed),
			Mathf.Clamp (rigidbody.velocity.y, -2 * maxSpeed, maxSpeed),
			0.0f);
		
				// Don't do dashing until jumping completely works
		
				if (Input.GetKeyDown (KeyCode.Z) && canDash) {
						currentDashTime = 0.0f;
						canDash = false;
				}
				
				//  if (currentDashTime < maxDashTime && !canDash  && onBeat(0.1f))
				if (currentDashTime < maxDashTime && !canDash) {
						if (facingRight) {
				
								moveDirection = new Vector3 (dashSpeed, 0, 0);
						} else {
								moveDirection = new Vector3 (-1.0f * dashSpeed, 0, 0);
						}
						currentDashTime += dashStoppingSpeed;
						moveDirection.y = 0.0f;
						rigidbody.AddForce (moveDirection);
				} else {
						canDash = true;
				}
		}
	
		void OnCollisionEnter (Collision other)
		{
				if (other.collider.tag == "Moving") {
						this.transform.parent = other.collider.gameObject.transform;
				}
		}
	
		void OnCollisionExit (Collision other)
		{
				if (other.collider.tag == "Moving") {
						this.transform.parent = null;
				}
		}
}   