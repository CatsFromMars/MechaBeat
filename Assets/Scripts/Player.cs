using UnityEngine;
using System.Collections;

public class Player : _AbstractRhythmObject {

	private GameObject controller;
	private HashIDs hash;
	private Animator animator;

	private float horiz; //Horizontal Axis (Plauer Input)
	private float vert; //Vertical Axis (Player Input)

	public int playerSpeed = 5;

	int beat;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController");
		hash = controller.GetComponent<HashIDs>();
		animator = GetComponent<Animator>();
	}

	void FixedUpdate() {
		//Cache player input
		horiz = Input.GetAxisRaw("Horizontal");
		vert = Input.GetAxisRaw("Vertical");
	}

	override
	public void rhythmUpdate(int beat) {
		//Sync player animations to the music
		if(animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) animator.SetTrigger(hash.beatTrigger);
	}

	override
	protected void asyncUpdate(){

		//Actual Player Controller Jazz
		MovePlayer();

	}

	void MovePlayer() {

		//Note: Player Movement Assumes InputAxisRaw and NOT InputAxis
		if(horiz > 0) {
			Debug.Log("RIGHT");
			transform.rotation = Quaternion.Euler(0,90,0);
			animator.SetBool(hash.runningBool, true);
			transform.Translate(Vector3.forward * (Time.deltaTime * 5));

		}

		else if(horiz < 0) {
			Debug.Log("LEFT");
			transform.rotation = Quaternion.Euler(0,-90,0);
			animator.SetBool(hash.runningBool, true);
			transform.Translate(Vector3.forward * (Time.deltaTime * 5));
		}

		else if (horiz == 0) animator.SetBool(hash.runningBool, false);

	}

}
