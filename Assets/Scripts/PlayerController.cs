using UnityEngine;
using System.Collections;
using Rhythmify;

public class PlayerController : _AbstractRhythmObject
{

		private GameObject controller;
		private HashIDs hash;
		private Animator animator;
		private float horiz; //Horizontal Axis (Plauer Input)
		private float vert; //Vertical Axis (Player Input)
	
		private int playerSpeed = 5;
		private Vector3 moveDirection;
		public bool onFloor = false;
		private bool jumping = false;
		public bool canDoubleJump = false;
		public float jumpForce = 100f;			// Amount of force added when the player jumps.
		private float airJumpForce; // amount of force added when player jumps in middle of jump
		public float moveForce = 365f;			// Amount of force added to move the player left and right.

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
				animator = GetComponent<Animator> ();
				currentDashTime = maxDashTime;
				airJumpForce = jumpForce * 0.6f;
		}
	
		void FixedUpdate ()
		{
				//Cache player input
				horiz = Input.GetAxisRaw ("Horizontal");
				vert = Input.GetAxisRaw ("Vertical");
		}
	
		override
		protected void rhythmUpdate (int beat)
		{
				//Sync player animations to the music
				if (animator.GetCurrentAnimatorStateInfo (0).nameHash == hash.idleState)
						animator.SetTrigger (hash.beatTrigger);
		
		}
	
		override
		protected void asyncUpdate ()
		{
		
				//Actual Player Controller Jazz
				MovePlayer ();

				//Check to see if we're on the floor
				animator.SetBool (hash.floorBool, onFloor);
				
				//Apply gravity
				//moveDirection.y -= gravity * Time.deltaTime;
				//rigidbody.AddForce (new Vector3(0.0f, -gravity * Time.deltaTime, 0.0f));
				//check if player fell to death
				if (gameObject.transform.position.y < -10)
						gameObject.transform.position = new Vector3 (-10, 5, 0);
		
		}

		private float maxSpeed = 5.0f;

		void MovePlayer ()
		{
		
				//Note: Player Movement Assumes InputAxisRaw and NOT InputAxis
				//Rotate Player and handle Running Animation
				if (horiz > 0) {
						transform.rotation = Quaternion.Euler (0, 90, 0);
						animator.SetBool (hash.runningBool, true);
						facingRight = true;
				} else if (horiz < 0) {
						transform.rotation = Quaternion.Euler (0, -90, 0);
						animator.SetBool (hash.runningBool, true);
						facingRight = false;
				} else if (horiz == 0)
						animator.SetBool (hash.runningBool, false);
		
				//Update the horizontal movement vector
			
				if (horiz != 0) {
						rigidbody.AddForce (Vector3.right * horiz * moveForce / 2);
				}
				
				if (horiz == 0) {
						if (onFloor) {
								rigidbody.velocity = new Vector3 (0.0f, 0.0f, rigidbody.velocity.z);
						} else 
								rigidbody.velocity = new Vector3 (0.0f, rigidbody.velocity.y, rigidbody.velocity.z);
				
				}
		
				// If the player's horizontal velocity is greater than the maxSpeed...
				if (Mathf.Abs (rigidbody.velocity.x) > maxSpeed) {
						// ... clamp the player's velocity to the maxSpeed in the x axis.
						rigidbody.velocity = new Vector3 (Mathf.Sign (rigidbody.velocity.x) * maxSpeed, rigidbody.velocity.y, rigidbody.velocity.z);
				}



				if (Input.GetKeyDown (KeyCode.Space) && onBeat(0.1f)) {
						if (onFloor) {
								animator.SetBool (hash.jumpBool, true);
								jumping = true;
								rigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse); 
						} /*
						if (onFloor && !canDoubleJump) {
								animator.SetBool (hash.jumpBool, true);
								canDoubleJump = true;
								jumping = true;
								rigidbody.AddForce (Vector3.up * jumpForce, ForceMode.Impulse); 
						} else if (!onFloor && canDoubleJump) {
								Debug.Log ("gets here");
								animator.SetBool (hash.jumpBool, true);
								canDoubleJump = false;
								rigidbody.AddForce (Vector3.up * airJumpForce, ForceMode.Impulse); 
						} */
				}
				// If the player's horizontal velocity is greater than the maxSpeed...
				if (Mathf.Abs (rigidbody.velocity.y) > maxSpeed) {
						// ... clamp the player's velocity to the maxSpeed in the x axis.
						rigidbody.velocity = new Vector3 (rigidbody.velocity.x, Mathf.Sign (rigidbody.velocity.y) * maxSpeed, rigidbody.velocity.z);
				}
		
				if (Input.GetKeyDown (KeyCode.Z) && canDash && onBeat(0.1f)) {
			
						currentDashTime = 0.0f;
						canDash = false;
				}
		
				if (currentDashTime < maxDashTime && !canDash) {
						if (facingRight)
				
								moveDirection = new Vector3 (dashSpeed, 0, 0);
						else
								moveDirection = new Vector3 (-1.0f * dashSpeed, 0, 0);
						currentDashTime += dashStoppingSpeed;
						moveDirection.y = 0.0f;
						rigidbody.AddForce (moveDirection);
				} else {
						canDash = true;
				}

	
		}

		//Jumping Animation Events
		void Rise ()
		{	
				Debug.Log ("RISE");
		}
	
		void Fall ()
		{ 
				Debug.Log ("FALL");

		}
	
	
		//Handle Floor Collision
		void OnCollisionStay (Collision collision)
		{
				if (collision.collider.gameObject.tag == "Floor") {
						onFloor = true;
			
						rigidbody.useGravity = false;
			
						animator.SetBool (hash.jumpBool, false);
						jumping = false;
						canDoubleJump = false;
				}
		
		}
	
		void OnCollisionExit (Collision collision)
		{
				if (collision.collider.gameObject.tag == "Floor") {
						onFloor = false;
						rigidbody.useGravity = true;
						canDoubleJump = true;
				}
		}
	
}	