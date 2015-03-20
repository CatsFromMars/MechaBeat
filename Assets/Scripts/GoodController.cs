using UnityEngine;
using System.Collections;
using Rhythmify;

public class GoodController : _AbstractRhythmObject {

	private GameObject controller;
	private HashIDs hash;
	private Animator animator;

    public int numJumps;
    public float jumpHeight;

    public float moveSpeed;
    public float fallSpeed;


    private int jumpsLeft;
    private float maxSpeed;
    private float movement;

    private bool jumping;
    private bool inAir;
    private bool touchedFloor;

	void Awake() {
		controller = GameObject.FindGameObjectWithTag("GameController");
		hash = controller.GetComponent<HashIDs>();
		animator = GetComponent<Animator>();
	}

    void FixedUpdate() {
        Vector3 nextVelocity = new Vector3(movement, rigidbody.velocity.y, 0);

        if (jumping) {
            nextVelocity.y = jumpHeight;
            jumping = false;
			animator.SetBool(hash.jumpBool, false);
        }

        rigidbody.velocity = nextVelocity;

        rigidbody.AddForce(Vector3.down * fallSpeed);
    }

    override protected void init() {
        jumpsLeft = numJumps;
    }

    override protected void asyncUpdate() {
        movement = Input.GetAxis("Horizontal") * moveSpeed;

		if (movement > 0) {
			transform.rotation = Quaternion.Euler(0, 90, 0);
			animator.SetBool(hash.runningBool, true);
		} else if (movement < 0) {
			transform.rotation = Quaternion.Euler(0, -90, 0);
			animator.SetBool(hash.runningBool, true);
		} else if (movement == 0) {
			animator.SetBool(hash.runningBool, false);
		}

        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0) {
            jumping = true;
			animator.SetBool(hash.jumpBool, true);
            jumpsLeft--;
        }
    }

    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0];

        if (Vector3.Dot(contact.normal, Vector3.up) > 0.70710678118) {
            jumpsLeft = numJumps;
        }
    }

    override protected void rhythmUpdate(int beat) {
		//Sync player animations to the music
		if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) {
			animator.SetTrigger(hash.beatTrigger);
		}
	}
}