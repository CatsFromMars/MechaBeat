using UnityEngine;
using System.Collections;
using Rhythmify;

public class Gusty : _AbstractRhythmObject {

    GameObject controller;
    HashIDs hash;
    Animator animator;
    public GameObject[] waypoints; //Has to be manually defined for each Gutsy Unit
    private int currentWaypoint = 0;
    private float speed = 0f;
    public float walkingSpeed = 5f; //Made public so that you can adjust for harder difficulties.

    // Use this for initialization
    void Awake() {
        controller = GameObject.FindGameObjectWithTag ("GameController");
        hash = controller.GetComponent<HashIDs>();
        animator = GetComponent<Animator>();
        transform.position = new Vector3 (waypoints[0].transform.position.x, transform.position.y, transform.position.z);
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
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].transform.position, step);

		if(transform.position == new Vector3(waypoints[currentWaypoint].transform.position.x, 
		                                     transform.position.y, transform.position.z)) { //If we reach a waypoint...
            currentWaypoint = (currentWaypoint+1) % waypoints.Length;             //Set the next waypoint
			transform.LookAt(new Vector3(waypoints[currentWaypoint].transform.position.x, 
			                             transform.position.y, transform.position.z));      //Look at the next waypoint
        }
    }

    override protected void rhythmUpdate (int beat) {

        if(beat%6 == 0) {
            animator.SetTrigger(hash.attackTrigger); //Attack every 6th beat.
        }
        else {
            animator.SetTrigger(hash.beatTrigger);
        }
    }
}
