using UnityEngine;
using System.Collections;

public class AnimatorController : MonoBehaviour {
	private GameObject controller;
	private HashIDs hash;
	private Animator animator;

	// Use this for initialization
	void Awake() {
		controller = GameObject.FindGameObjectWithTag ("GameController");
		hash = controller.GetComponent<HashIDs> ();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Mathf.Abs(rigidbody.velocity.x) > 0.1) animator.SetBool(hash.runningBool, true);
		else animator.SetBool(hash.runningBool, false);
	}
}
