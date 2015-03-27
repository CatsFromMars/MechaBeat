using UnityEngine;
using System.Collections;
using Rhythmify;

public class Gusty : _AbstractRhythmObject {

    GameObject controller;
    HashIDs hash;
    Animator animator;
    public float[] waypoints; //Has to be manually defined for each Gutsy Unit
    private int currentWaypoint = 0;
    private float speed = 0f;
    public float walkingSpeed = 5f; //Made public so that you can adjust for harder difficulties.
	public bool relative = false;

    // Use this for initialization
    void Awake() {
        controller = GameObject.FindGameObjectWithTag ("GameController");
        hash = controller.GetComponent<HashIDs>();
        animator = GetComponent<Animator>();

		Vector3 target;
		if(relative) target = new Vector3(waypoints[currentWaypoint]+transform.position.x,transform.position.y, transform.position.z);
		else target = new Vector3(waypoints[currentWaypoint],transform.position.y, transform.position.z);
		transform.LookAt(target); //Look at the next waypoint
	}

    void moveStart() {
        //Animation Event for walking. Used to sync up steps. Otherwise it would moonwalk.
        speed = walkingSpeed;
    }

    void moveEnd() {
        //Animation Event for walking. Used to sync up steps. Otherwise it would moonwalk.
        speed = 0;
    }

    override protected void asyncUpdate () {
        //Move towards the waypoint.
        float step = speed * Time.deltaTime;
		Vector3 target;

		if(relative) target = new Vector3(waypoints[currentWaypoint]+transform.position.x,transform.position.y, transform.position.z);
		else target = new Vector3(waypoints[currentWaypoint],transform.position.y, transform.position.z);

		transform.position = Vector3.MoveTowards(transform.position, target, step);

		if(transform.position == target) { //If we reach a waypoint...
            currentWaypoint = (currentWaypoint+1) % waypoints.Length; //Set the next waypoint
			if(relative) target = new Vector3(waypoints[currentWaypoint]+transform.position.x,transform.position.y, transform.position.z);
			else target = new Vector3(waypoints[currentWaypoint],transform.position.y, transform.position.z);
			transform.LookAt(target); //Look at the next waypoint
        }
    }

    override protected void rhythmUpdate() {

        if(getBeat()%6 == 0) {
            animator.SetTrigger(hash.attackTrigger); //Attack every 6th beat.
        }
        else {
            animator.SetTrigger(hash.beatTrigger);
        }
    }
}
