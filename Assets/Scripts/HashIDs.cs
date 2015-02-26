using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour {
	//STATES
	public int idleState;

	//VARS
	public int runningBool;
	public int beatTrigger;

	// Use this for initialization
	void Awake () {
		idleState = 
		runningBool = Animator.StringToHash("Running");
		beatTrigger = Animator.StringToHash("Beat");
	}

}
