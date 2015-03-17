﻿using UnityEngine;
using System.Collections;
using Rhythmify;

public class PlayerController : _AbstractRhythmObject {
    
    private GameObject controller;
    private HashIDs hash;
    //private Animator animator;
    
    private int playerSpeed = 5;
    private Vector3 moveDirection;
    public float jumpForce = 100f;
    public float moveForce = 365f;
    
    int beat;
    private bool facingRight = true;
    public float maxDashTime = 1.0f;
    public float dashSpeed = 500.0f;
    public float dashStoppingSpeed = 0.1f;
    private float currentDashTime;
    private bool canDash = true;
    
    void Awake() {
        moveDirection = Vector3.zero;
        controller = GameObject.FindGameObjectWithTag("GameController");
        hash = controller.GetComponent<HashIDs>();
        //animator = GetComponent<Animator>();
        currentDashTime = maxDashTime;
    }
    
    private float gravity = 9.8f;
    Transform gravityCenter;
    private float horiz; //Horizontal Axis (Plauer Input)
    private float vert; //Vertical Axis (Player Input)
    
    
    override protected void rhythmUpdate(int beat) {
        //Sync player animations to the music
        //if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) {
        //animator.SetTrigger(hash.beatTrigger);
        //}
    }
    
    override protected void asyncUpdate() {
        //Actual Player Controller Jazz
        MovePlayer();
        
        if (gameObject.transform.position.y < -10) {
            gameObject.transform.position = new Vector3(-10, 5, 0);
        }
        
    }
    
    private float maxSpeed = 5.0f;
    
    void MovePlayer() {
        //Note: Player Movement Assumes InputAxisRaw and NOT InputAxis
        //Rotate Player and handle Running Animation
        float horiz = Input.GetAxisRaw("Horizontal");
        //bool keyPressed = false; 
        
        if (horiz > 0) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //animator.SetBool(hash.runningBool, true);
            facingRight = true;
        } else if (horiz < 0) {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            //animator.SetBool(hash.runningBool, true);
            facingRight = false;
        } else if (horiz == 0) {
            //animator.SetBool(hash.runningBool, false);
        }
        
        //Update the horizontal movement vector
        
        if (horiz != 0) {
            rigidbody.AddForce(Vector3.right * horiz * moveForce / 2);
        }
        
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (true) {
                jump();
            }
        }
        
        rigidbody.velocity = new Vector3(
            Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed),
            0.0f);
        
        // Don't do dashing until jumping completely works
        
        if (Input.GetKeyDown(KeyCode.Z) && canDash) {
            currentDashTime = 0.0f;
            canDash = false;
        }
        
        //  if (currentDashTime < maxDashTime && !canDash  && onBeat(0.1f))
        if (currentDashTime < maxDashTime && !canDash) {
            if (facingRight) {
                
                moveDirection = new Vector3(dashSpeed, 0, 0);
            } else {
                moveDirection = new Vector3(-1.0f * dashSpeed, 0, 0);
            }
            currentDashTime += dashStoppingSpeed;
            moveDirection.y = 0.0f;
            rigidbody.AddForce(moveDirection);
        } else {
            canDash = true;
        }
    }

    private void jump() {
        transform.SetParent(null);
        rigidbody.AddForce(Vector3.up * jumpForce);
    }

    void OnCollisionEnter(Collision collision) {
        transform.SetParent(collision.gameObject.transform);
    }

    void OnCollisionExit(Collision collision) {
        transform.SetParent(null);
    }
}   