using UnityEngine;
using System.Collections;
using Rhythmify;

public class Player : _AbstractRhythmObject
{

		private GameObject controller;
		private HashIDs hash;
		private Animator animator;
		private CharacterController characterController;
		private float horiz; //Horizontal Axis (Plauer Input)
		private float vert; //Vertical Axis (Player Input)
		
		private int playerSpeed = 5;
		private int fallingSpeed = -10;
		public int jumpHeight = 10;
		private Vector3 moveDirection;
		public bool onFloor = false;
		private bool jumping = false;
		private int gravity = 20;
		int beat;
		private bool facingRight = true;
		public float maxDashTime = 1.0f;
		public float dashSpeed = 1.0f;
		public float dashStoppingSpeed = 0.1f;
		private float currentDashTime;
	private bool canDoubleJump = false;
	private bool canDash = true;

		void Awake ()
		{
				moveDirection = Vector3.zero;
				controller = GameObject.FindGameObjectWithTag ("GameController");
				hash = controller.GetComponent<HashIDs> ();
				animator = GetComponent<Animator> ();
				characterController = GetComponent<CharacterController> ();
				currentDashTime = maxDashTime;
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
				moveDirection.y -= gravity * Time.deltaTime;

				//check if player fell to death
				if (gameObject.transform.position.y < -10)
						gameObject.transform.position = new Vector3 (-10, 5, 0);

		}
		
		

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
				moveDirection.x = horiz * playerSpeed;
		
		
				//Jumping!
				if (Input.GetButtonDown ("Jump")) {
						animator.SetBool (hash.jumpBool, true);

				}

				if (Input.GetKeyDown (KeyCode.Z) && canDash) {
						
						currentDashTime = 0.0f;
						canDash = false;
				}
			
				if (currentDashTime < maxDashTime) {
						if (facingRight)
				
								moveDirection = new Vector3 (dashSpeed, 0, 0);
						else
								moveDirection = new Vector3 (-1.0f * dashSpeed, 0, 0);
						currentDashTime += dashStoppingSpeed;
						moveDirection.y = 0.0f;
				} else {
						canDash = true;
				}
			
				//Now apply the movement vector via the Character Controller!
				characterController.Move (moveDirection * Time.deltaTime);


		}

		//Jumping Animation Events
		void Rise ()
		{		
				Debug.Log ("RISE");
				if (canDoubleJump) {
						jumping = true; 
						moveDirection.y = jumpHeight;
						canDoubleJump = false;
				} else {
						jumping = true;
						moveDirection.y = jumpHeight;
						canDoubleJump = true;
				}
		}

		void Fall ()
		{
		if (canDoubleJump && Input.GetKeyDown(KeyCode.Space)) {
						jumping = true; 
						moveDirection.y = jumpHeight;
						canDoubleJump = false;
				} else {
						Debug.Log ("FALL");
						moveDirection.y = 0;
						jumping = false;
				}
		}


		//Handle Floor Collision
		void OnCollisionStay (Collision collision)
		{
				if (collision.collider.gameObject.tag == "Floor")
						onFloor = true;
				animator.SetBool (hash.jumpBool, false);
				jumping = false; 

		}

		void OnCollisionExit (Collision collision)
		{
				if (collision.collider.gameObject.tag == "Floor")
						onFloor = false;
		}

}
