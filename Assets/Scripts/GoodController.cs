using UnityEngine;
using System.Collections;
using Rhythmify;

public class GoodController : _AbstractRhythmObject {

    public int numJumps;
    public float jumpHeight;
    public float moveSpeed;
    public float fallSpeed;
    public float accuracy;

    private GameObject controller;
    private GameObject particleContainer;
    private GameObject particleContainer2;
    private HashIDs hash;
    private Animator animator;
    private int jumpsLeft;
    private float maxSpeed;
    private float movement;
    private bool jumping;
    private bool dodging;
    private Vector3 checkpoint;

    void Awake() {
        controller = GameObject.FindGameObjectWithTag("GameController");
        hash = controller.GetComponent<HashIDs>();
        animator = GetComponent<Animator>();
        particleContainer = transform.FindChild("ParticleSystem").gameObject;
        particleContainer2 = transform.FindChild("ParticleSystem2").gameObject;
    }

    void FixedUpdate() {
        Vector3 nextVelocity = new Vector3(movement, rigidbody.velocity.y, 0);

        if (jumping) {
            nextVelocity.y = jumpHeight;
            jumping = false;
        }

        if (dodging) {
            nextVelocity.x *= 2;
            nextVelocity.y = 0;
        }

        rigidbody.velocity = nextVelocity;

        if (!dodging) {
            rigidbody.AddForce(Vector3.down * fallSpeed);
        }

        if (rigidbody.velocity.y < -20) {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -20, 0);
        }
    }

    override protected void init() {
        jumpsLeft = numJumps;
    }

    override protected void asyncUpdate() {
        movement = Input.GetAxis("Horizontal") * moveSpeed;

		if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Space)) && jumpsLeft > 0 && onBeat(accuracy) && getBeat() % 2 == 1) {
            jumping = true;
            particleContainer.particleSystem.Play (); 
			audio.Play();
            jumpsLeft--;
        }

        if (Input.GetKeyDown(KeyCode.Z) && !dodging && onBeat(accuracy) && getBeat() % 2 == 1) {
            StartCoroutine(dodgeSequence(secondsPerBeat));
            particleContainer2.particleSystem.Play (); 
			audio.Play();
        }
        animate();
    }

    private IEnumerator dodgeSequence(float time) {
        dodging = true;
		animator.SetBool(hash.dodgeBool, true);
        yield return new WaitForSeconds(time);
        dodging = false;
		animator.SetBool(hash.dodgeBool, false);
        resetJumps();
        yield return null;
    }

    private void animate() {
        if (movement > 0) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        } else if (movement < 0) {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        if (!dodging) {
            if (Mathf.Abs(movement) < 0.0001f) {
                animator.SetBool(hash.runningBool, false);
            } else {
                animator.SetBool(hash.runningBool, true);
            }
        }
        if (rigidbody.velocity.y > 0.0001 && transform.parent == null) {
            animator.SetBool(hash.jumpBool, true);
        } else {
            animator.SetBool(hash.jumpBool, false);
        }
    }

    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts [0];

        if (Vector3.Dot(contact.normal, Vector3.up) > 0.70710678118) {
            resetJumps();
        }
    }

    override protected void rhythmUpdate() {
        //Sync player animations to the music
        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) {
            animator.SetTrigger(hash.beatTrigger);
        }
    }

    public void setCheckPoint(Vector3 v3) {
        checkpoint = v3;
    }

    public void resetToCheckPoint(bool canDodge) {
        if (!(canDodge && dodging)) {
            transform.position = checkpoint;
            rigidbody.velocity = Vector3.zero;
        }
    }

    public void resetJumps() {
        jumpsLeft = numJumps;
    }
}