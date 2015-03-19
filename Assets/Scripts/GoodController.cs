using UnityEngine;
using System.Collections;
using Rhythmify;

public class GoodController : _AbstractRhythmObject {

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

    void FixedUpdate() {
        Vector3 nextVelocity = new Vector3(movement, rigidbody.velocity.y, 0);

        if (jumping) {
            nextVelocity.y = jumpHeight;
            jumping = false;
        }

        rigidbody.velocity = nextVelocity;

        rigidbody.AddForce(Vector3.down * fallSpeed);
    }

    override protected void init() {
        jumpsLeft = numJumps;
    }

    override protected void asyncUpdate() {
        movement = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0) {
            jumping = true;
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
        
    }
}