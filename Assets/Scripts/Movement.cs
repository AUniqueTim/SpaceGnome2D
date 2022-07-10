using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Movement : MonoBehaviour
{

    [SerializeField] public GameObject player;
    [SerializeField] public GameObject playerObject;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Rigidbody2D playerRB;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private BoxCollider2D mainCollider;
    [SerializeField] private BoxCollider2D groundTriggerCollider;

    [SerializeField] private float playerSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;

    private float gravity = -9.87f;

    [SerializeField] public bool jumpAllowed;
    [SerializeField] public bool isJumping;
    [SerializeField] private bool grounded;
    [SerializeField] public bool playerDestroyed;


    [SerializeField] private float defaultJumpHeight;
    [SerializeField] public static float defaultJumpSpeed;
    [SerializeField] public static float jumpHeight;
    [SerializeField] public static float jumpSpeed;
    [SerializeField] public float fallSpeed;

    public Transform startingPosition;

    public Ramp rampScript;
    public ObjectKiller objectKillerScript;
    [SerializeField] private Movement movementScript;

    [SerializeField] private Animator pokeAnimator;
    public string runState;

    public bool flipX;

    //public GameObject smallChild;
    //public GameObject dialogue1;


    //START SINGLETON

    public static Movement instance;
    public static Movement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Movement>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject();
                    singleton.AddComponent<Movement>();
                    singleton.name = "(Singleton) Movement";
                }
            }
            return instance;
        }
    }


    public void Start()
    {
       startingPosition.position = gameObject.transform.position;

        //movementScript = GetComponent<Movement>();
        
        jumpAllowed = true;
        instance = this;
        defaultJumpSpeed = 1;
        jumpSpeed = defaultJumpSpeed;
        jumpHeight = defaultJumpHeight;

    }
    // Update is called once per frame
    void Update()
    {

       //Sprite Direction Flip
        if (flipX) { spriteRenderer.flipX = true; }
        else if (!flipX) { spriteRenderer.flipX = false; }


        //Jump Input and Prerequisites
        if (Input.GetKeyDown(KeyCode.Space) && jumpAllowed) { Jump(); }

        if (isJumping) { jumpAllowed = false; }
        else if (grounded) { jumpAllowed = true; }
        else { jumpAllowed = false; }


        if (rampScript.isExitingCollision) { if (grounded) { jumpSpeed = defaultJumpSpeed; rampScript.isExitingCollision = false; } }
        if (rampScript.isEnteringCollision) { if (!grounded) { jumpSpeed = rampScript.rampJumpHeight; rampScript.isEnteringCollision = false; } }
       // else if (rampScript.isEnteringCollision) { if (grounded) { jumpSpeed = defaultJumpSpeed; rampScript.isEnteringCollision = false; } }
       
       
        RunStates();

        if (movementScript.GetComponent<Movement>() != isActiveAndEnabled)
        {
            
        }
    }
    //Interactables
   

    public void OnCollisionEnter2D(Collision2D collision)
    {
 //       if (collision.gameObject.tag == "PlayerKiller"){ GameOver(); }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        grounded = true;

    


    }
    public void OnTriggerExit2D(Collider2D groundSensor)
    {
        grounded = false;
    
      
    }

    //STATE MACHINE
    public void RunStates()
    {
        if (Input.GetAxisRaw("Horizontal") == 0) { Idle(); runState = "isIdle"; }
        else if (Input.GetAxisRaw("Horizontal") > 0) { MoveForward(); runState = "isWalking"; }
        else if (Input.GetAxisRaw("Horizontal") < 0) { MoveBackward(); runState = "isWalkingBackwards"; }
        else if (Input.GetAxisRaw("Vertical") == 0) { Idle(); runState = "isIdle"; }
        else if (Input.GetAxisRaw("Vertical") >0 ) { MoveUp(); runState = "isMovingUp"; }
        else if (Input.GetAxisRaw("Vertical") < 0) { MoveDown(); runState = "isMovingDown"; }
        if (Input.GetAxisRaw("Jump") > 0) { Jump(); runState = "isJumping"; }

        if (Input.GetKey(KeyCode.W)) { playerRB.transform.position += Vector3.up * playerSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.S)) { playerRB.transform.position += Vector3.down * playerSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.D)) {  flipX = false;  playerRB.transform.position += Vector3.right * playerSpeed * Time.deltaTime; }
        if (Input.GetKey(KeyCode.A)) { flipX = true; playerRB.transform.position += -Vector3.right * playerSpeed * Time.deltaTime;  }

        if (Input.GetKeyUp(KeyCode.Space)) { Jump(); playerRB.transform.position += Vector3.up * jumpHeight * jumpSpeed * -gravity * Time.deltaTime;  }
        if (Input.GetKey(KeyCode.LeftShift)) { ResetStates();  Slide(); playerSpeed = 10; runState = "isSliding"; /*Add boost bar, depleting when used via timer?*/}
        else if (Input.GetKeyUp(KeyCode.LeftShift)) { playerSpeed = 3; ResetStates(); }

    }
    //Idle Animation State

    public void Idle()
    {
        ResetStates();
        pokeAnimator.SetBool("idle", true);
        runState = "isIdle";
    }
    public void StopIdle()
    {
        
        pokeAnimator.SetBool("idle", false);
    }

    //Sliding Animation State
    public void Slide()
    {
        ResetStates();
        pokeAnimator.SetBool("sliding", true);
        runState = "isSliding";
    }
    public void StopSliding()
    {
        pokeAnimator.SetBool("sliding", false);
    }


    //Walking Animation State

    public void MoveForward()
    {
        ResetStates();
        pokeAnimator.SetBool("walking", true);
        runState = "isWalking";
    }

    public void StopMovingForward()
    {
        pokeAnimator.SetBool("walking", false);
    }

    public void MoveBackward()
    {
        ResetStates();
        pokeAnimator.SetBool("backwards", true);
        runState = "isWalkingBackwards";
    }

    public void StopMovingBackward()
    {
        pokeAnimator.SetBool("backwards", false);

    }

    public void MoveUp()
    {
        ResetStates();
        pokeAnimator.SetBool("movinngUp", true);
        runState = "isMovingUp";
    }
    public void MoveDown()
    {
        ResetStates();
        pokeAnimator.SetBool("movinngDown", true);
        runState = "isMovingDown";
    }

    //Jumping Animation State
    public void Jump()
    {
        ResetStates();

        isJumping = true;
        runState = "isJumping";
        playerRB.transform.position += Vector3.up * jumpHeight * jumpSpeed * -gravity * Time.deltaTime;
        pokeAnimator.SetBool("isJumping", true);
        Debug.Log("Jumped.");
        //StopJump();

    }
    public void StopJump()
    {
        isJumping = false;
        jumpAllowed = false;
        pokeAnimator.SetBool("isJumping", false);
        Debug.Log("Player Stopped Jumping.");
        runState = "None";
    }
    void ResetStates()
    {
        StopSliding();
        StopIdle();
        StopJump();
        StopMovingBackward();
        StopMovingForward();
        runState = "None";
    }
}
