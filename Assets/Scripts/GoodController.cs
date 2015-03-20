﻿using UnityEngine;
using System.Collections;
using Rhythmify;

public class GoodController : _AbstractRhythmObject {

    public int numJumps;
    public float jumpHeight;
    public float moveSpeed;
    public float fallSpeed;
    private GameObject controller;
    private HashIDs hash;
    private Animator animator;
    private int jumpsLeft;
    private float maxSpeed;
    private float movement;
    private bool jumping;
    private Vector3 checkpoint;

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
        }

        rigidbody.velocity = nextVelocity;

        rigidbody.AddForce(Vector3.down * fallSpeed);
    }

    override protected void init() {
        jumpsLeft = numJumps;
    }

    override protected void asyncUpdate() {
        movement = Input.GetAxis("Horizontal") * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && jumpsLeft > 0 && onBeat(0.1f)) {
            jumping = true;
            jumpsLeft--;
        }
        animate();
    }

    private void animate() {
        if (Mathf.Abs(movement) < 0.0001f) {
            animator.SetBool(hash.runningBool, false);
        } else {
            if (movement > 0) {
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else {
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }
            animator.SetBool(hash.runningBool, true);
        }
        if (rigidbody.velocity.y > 0.0001 && transform.parent == null) {
            animator.SetBool(hash.jumpBool, true);
        } else if (rigidbody.velocity.y < -0.0001 && transform.parent == null) {
            //Falling animation
        } else {
            animator.SetBool(hash.jumpBool, false);
        }
    }

    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts [0];

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

    public void setCheckPoint(Vector3 v3) {
        checkpoint = v3;
    }

    public void resetToCheckPoint() {
        transform.position = checkpoint;

        rigidbody.velocity = Vector3.down;
    }
}