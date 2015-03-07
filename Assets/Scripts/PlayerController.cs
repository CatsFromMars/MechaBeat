using UnityEngine;
using System.Collections;
using Rhythmify;

public class PlayerController : _AbstractRhythmObject {

    private GameObject controller;
    private HashIDs hash;
    //private Animator animator;
    
    private int playerSpeed = 5;
    private Vector3 moveDirection;
    public bool onFloor = false;
    private bool jumping = false;
    //public bool canDoubleJump = false;
    public float jumpForce = 100f;          // Amount of force added when the player jumps.
    private float airJumpForce; // amount of force added when player jumps in middle of jump
    public float moveForce = 365f;          // Amount of force added to move the player left and right.

    int beat;
    //private bool facingRight = true;
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
        airJumpForce = jumpForce * 0.6f;
    }
    
    void FixedUpdate() {
        //Cache player input
    }
    
    override protected void rhythmUpdate(int beat) {
        //Sync player animations to the music
        //if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) {
            //animator.SetTrigger(hash.beatTrigger);
        //}
    }
    
    override protected void asyncUpdate() {
        //Actual Player Controller Jazz
        MovePlayer();

        //Check to see if we're on the floor
        //animator.SetBool(hash.floorBool, onFloor);
                
        //Apply gravity
        //moveDirection.y -= gravity * Time.deltaTime;
        //rigidbody.AddForce (new Vector3(0.0f, -gravity * Time.deltaTime, 0.0f));
        //check if player fell to death
        if (gameObject.transform.position.y < -10) {
            gameObject.transform.position = new Vector3(-10, 5, 0);
        }
        
    }

    private float maxSpeed = 5.0f;

    void MovePlayer() {
        //Note: Player Movement Assumes InputAxisRaw and NOT InputAxis
        //Rotate Player and handle Running Animation
        float horiz = Input.GetAxisRaw("Horizontal");

        if (horiz > 0) {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            //animator.SetBool(hash.runningBool, true);
            //facingRight = true;
        } else if (horiz < 0) {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            //animator.SetBool(hash.runningBool, true);
            //facingRight = false;
        } else if (horiz == 0) {
            //animator.SetBool(hash.runningBool, false);
        }
        
        //Update the horizontal movement vector
            
        if (horiz != 0) {
            rigidbody.AddForce(Vector3.right * horiz * moveForce / 2);
        }
                
        if (horiz == 0) {
            if (onFloor) {
                rigidbody.velocity = new Vector3(0.0f, 0.0f, 0.0f);
            } else { 
                rigidbody.velocity = new Vector3(0.0f, rigidbody.velocity.y, 0.0f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            //if (onFloor) {
                //animator.SetBool(hash.jumpBool, true);
                //jumping = true;
                rigidbody.AddForce(Vector3.up * jumpForce); 
            //} 
        }
        
        rigidbody.velocity = new Vector3(
            Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rigidbody.velocity.y, -maxSpeed, maxSpeed),
            0.0f);

        /* Don't do dashing until jumping completely works

        if (Input.GetKeyDown(KeyCode.Z) && canDash && onBeat(0.1f)) {
            currentDashTime = 0.0f;
            canDash = false;
        }
        
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

        */
    }

    //Jumping Animation Events
    void Rise() {   
        //Debug.Log("RISE");
    }
    
    void Fall() { 
        //Debug.Log("FALL");
    }
    
    
    //Handle Floor Collision
    void OnCollisionStay(Collision collision) {
        if (collision.collider.gameObject.tag == "Floor") {
            onFloor = true;
            
            rigidbody.useGravity = false;
            
            //animator.SetBool(hash.jumpBool, false);
            jumping = false;
            //canDoubleJump = false;
        }
    }
    
    void OnCollisionExit(Collision collision) {
        if (collision.collider.gameObject.tag == "Floor") {
            onFloor = false;
            rigidbody.useGravity = true;
            //canDoubleJump = true;
        }
    }
    
}   