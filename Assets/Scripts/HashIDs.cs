using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {
	//STATES
	public int idleState;
	public int jumpState;
	public int fallingState;

	//VARS
	public int runningBool;
	public int beatTrigger;
	public int floorBool;
	public int jumpBool;

	// Use this for initialization
	void Awake () {
		idleState = Animator.StringToHash("Base Layer.IdleStill");
		jumpState = Animator.StringToHash("Base Layer.Jump");
		fallingState = Animator.StringToHash("Base Layer.Fall");

		floorBool = Animator.StringToHash("onFloor");
		runningBool = Animator.StringToHash("Running");
		beatTrigger = Animator.StringToHash("Beat");
		jumpBool = Animator.StringToHash("Jump");
	}

}
