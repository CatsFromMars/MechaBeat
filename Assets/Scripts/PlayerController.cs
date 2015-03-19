using UnityEngine;
using System.Collections;
using Rhythmify;

public class PlayerController : _AbstractRhythmObject {
    
    private GameObject controller;
    private HashIDs hash;
    private Animator animator;
    private int playerSpeed = 5;
    private Vector3 moveDirection;
    private bool canDoubleJump = false;
    public float jumpForce = 1500f;          
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
        animator = GetComponent<Animator>();
        currentDashTime = maxDashTime;
    }
    private float horiz;
    private float vert;
    
    
    override protected void rhythmUpdate(int beat) {
        //Sync player animations to the music
        if (animator.GetCurrentAnimatorStateInfo(0).nameHash == hash.idleState) {
            animator.SetTrigger(hash.beatTrigger);
        }
    }
    
    override protected void asyncUpdate() {
        //Actual Player Controller Jazz
        MovePlayer();
        
        if (gameObject.transform.position.y < -10) {
            gameObject.transform.position = new Vector3(-10, 5, 0);
        }
        
    }
    
    private float maxSpeed = 5.0f;
    private int jumpCount = 0;

    void MovePlayer() {
        //Note: Player Movement Assumes InputAxisRaw and NOT InputAxis
        //Rotate Player and handle Running Animation
        float horiz = Input.GetAxisRaw("Horizontal");
        
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
                
        if (Mathf.Abs(rigidbody.velocity.y) < 0.1) {
            jumpCount = 0;
        }
                
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.X)) {

            rigidbody.AddForce(Vector3.up * jumpForce);

        }

        rigidbody.velocity = new Vector3(
            Mathf.Clamp(rigidbody.velocity.x, -maxSpeed, maxSpeed),
            Mathf.Clamp(rigidbody.velocity.y, -2 * maxSpeed, maxSpeed), 0.0f);

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
            transform.position = new Vector3(transform.position.x, transform.position.y, -1.0f); 
        } else {
            canDash = true;
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f); 
        }
    }
}
