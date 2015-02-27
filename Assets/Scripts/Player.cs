using UnityEngine;
using System.Collections;

public class Player : _AbstractRhythmObject {

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

	void Awake() {
		moveDirection = Vector3.zero;
		controller = GameObject.FindGameObjectWithTag("GameController");
		hash = controller.GetComponent<HashIDs>();
		animator = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
	}

	void FixedUpdate() {
		//Cache player input
		horiz = Input.GetAxisRaw("Horizontal");
		vert = Input.GetAxisRaw("Vertical");

	}

	override
	protected void rhythmUpdate(int beat) {
		//Sync player animations to the music
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) animator.SetTrigger(hash.beatTrigger);

	}

	override
	protected void asyncUpdate(){

		//Actual Player Controller Jazz
		MovePlayer();

		//Check to see if we're on the floor
		animator.SetBool(hash.floorBool, onFloor);

		//Apply gravity
		moveDirection.y -= gravity * Time.deltaTime;

	}

	void MovePlayer() {

		//Note: Player Movement Assumes InputAxisRaw and NOT InputAxis

		//Rotate Player and handle Running Animation
		if(horiz > 0) {
			transform.rotation = Quaternion.Euler(0,90,0);
			animator.SetBool(hash.runningBool, true);
		}
		else if(horiz < 0) {
			transform.rotation = Quaternion.Euler(0,-90,0);
			animator.SetBool(hash.runningBool, true);
		}
		else if (horiz == 0) animator.SetBool(hash.runningBool, false);

		//Update the horizontal movement vector
		moveDirection.x = horiz*playerSpeed;

		//Jumping!
		if(Input.GetButtonDown("Jump")) animator.SetBool(hash.jumpBool, true);
		
		//Now apply the movement vector via the Character Controller!
		characterController.Move(moveDirection * Time.deltaTime);
	}

	//Jumping Animation Events
	void Rise() {
		Debug.Log ("RISE");
		jumping = true;
		moveDirection.y = jumpHeight;
	}
	void Fall() {
		Debug.Log ("FALL");
		moveDirection.y = 0;
	}


	//Handle Floor Collision
	void OnCollisionStay(Collision collision) {
		if(collision.collider.gameObject.tag == "Floor") onFloor = true;
		animator.SetBool(hash.jumpBool, false);
		jumping = false; 
	}

	void OnCollisionExit(Collision collision) {
		if(collision.collider.gameObject.tag == "Floor") onFloor = false;
	}

}
