using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Jump Settings
    [SerializeField] private float jumpForce;               // How much force to apply to jump
    public bool isOnGround;                                 // bool to check if grounded
    public bool isJumping;                                  // ARe we in the process of jumping
    [SerializeField] private float movespeed = 9f;         // Start MoveSpeed




    // speed Modifier

    private float moveSpeedStore;                           // store of start speed to be used when restarting
    private float speedMilestoneCountStore;                 // store of start milestone to be used when restarting
    public float speedMultiplier;                           // how much to multiple spped by yo increase
    private float speedMilestoneCount;                      // Milestone value for first speed increase
    public float speedIncreaseMilestone;                    // How much to increase the distance between milestones
    private float speedIncreaseMilestoneStore;              // store of initial milestore to be used when restarting

    //Slide Settings
    public bool isSliding = false;                                              // am Sliding true/false

    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions

    //  public AudioSource deathSound;
    //  public AudioSource jumpSound;

    // Character Controller properties
    private CharacterController controller;                 // define the character controller
    [SerializeField] private float gravity = 20f;                            // define initail gravity
    private float verticalVelocity;                         // define vertical upward motion

   // private float hiScoreCount;                  // what is the hi score


    //Code to give points when picking up coin
    //   public int coinscoreToGive;     // what point value to give
     private ScoreManager theScoreManager;       // reference the score manager

    void Start()
    {

        controller = GetComponent<CharacterController>();           // reference the character controller
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script
                                                                   
        

        // Save settings to reset when restarting game
        speedMilestoneCount = speedIncreaseMilestone;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;

       


    }


    void Update()
    {


        if (controller.isGrounded) //if grounded
        {
            isOnGround = true;             // seton ground to true - am on ground
            isJumping = false;              // we are not jumping so false
            //anim.SetBool("Grounded", true);

            verticalVelocity = -0.1f;       // ensure normal vertical velocity
        }

        #region Inputs for Player
        // Code to manage mobile and keyboard inputs
        if (MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log(" Go Left");
        }
        if (MobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(" Go Right");
        }

        if (MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)            // if swipe up and on ground then jumpm
        {
            //  anim.SetTrigger("Jump");
            isOnGround = false;                                                                         // no longer on ground
            isJumping = true;                                                                           // We are jumping
            verticalVelocity = jumpForce;                                                               // how high to jump
        }
        else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow) && isOnGround)       // if swipe down and we are on ground then slide
        {
            StartSliding();

            // maybe add a cancel invoke here to allow for longer sliding
        }
        else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow) && isJumping && !isOnGround)     // if jumping and not on ground then increase down ward motion to fall faster abd then slide
        {
            verticalVelocity = -5f;
            StartSliding();
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime); // slowly fall to ground level if normal jump
        }
        #endregion

        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.x * Vector3.back;
        // Calcuate move vector
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * movespeed; // character was shakaing 

        //moveVector.x = (targetPosition - transform.position).x * movespeed;


        moveVector.y = verticalVelocity;
        moveVector.x = movespeed;
        controller.Move(moveVector * Time.deltaTime);
    }




    #region Slide and Jump functions
    private void StartSliding()
    {
        // Code executed when the player slides

        // anim.SetBool("Sliding", true);
        isSliding = true;
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, /*controller.center.y / 2*/ -0.5f, controller.center.z);
        Invoke("StopSliding", 1.0f);
    }
    private void StopSliding()
    {
        // Code executed when the player stops sliding
        //  anim.SetBool("Sliding", false);

        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, 0, controller.center.z);
        isSliding = false;
    }

    private void StartJump()
    {
        // Code executed when the player jumps
        //anim.SetTrigger("Jump");
        verticalVelocity = jumpForce;
        isOnGround = false;
        // jumpSound.Play();
    }
    #endregion




    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("enter");
        if (other.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Hit obstalce");
            theScoreManager.SaveHighScore();
            theGameManager.RestartGame();       // AW want pause and choose to continue later
        }
        if (other.gameObject.CompareTag("Enemy"))  // If hit Enemy
        {
            if (isSliding == true)                      // If the Player is sliding, they kick the feet out from under enemy and they die
            {
                
                Destroy(other.gameObject);          // Code to destroy the object that we collided with
            }
            else
            {
                theScoreManager.SaveHighScore();
                //  deathSound.Play();
                theGameManager.RestartGame();  // AW want pause and choose to continue later
                movespeed = moveSpeedStore;     //Reset back to starting game speed
                speedMilestoneCount = speedMilestoneCountStore;  //Reset back to starting game speed increase
                speedIncreaseMilestone = speedIncreaseMilestoneStore; //Reset back to starting game spped milestone
            }
        }
   

        
    }

   



}

