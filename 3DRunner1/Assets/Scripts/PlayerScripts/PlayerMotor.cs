using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    // Define the level parameters

    private const float LANE_DISTANCE = 2.5f; //set the lane width
    private const float TURN_SPEED = 0.05f;     // how much to turn the charatcer in direction of lane chnage
    private int desiredLane = 1; // 0=left 1=middle 2=right


    // Define Character Parameters
    private bool isRunning = true;          // Are we running
    // Basic Movement
    private CharacterController controller;  // reference character controller
    [SerializeField] private float jumpForce;       // How high we can jump
    private float gravity = 20f; // was 12      // How strong is Gravity
    private float verticalVelocity;             // What is our falling speed

    //Slide Settings
    public bool isSliding = false;              // am Sliding true/false

    // Jump Settings
    public bool isJumping;                          // bool to check if we are jumping
    public bool isOnGround;                                 // bool to check if grounded

    // speed Modifier
  //  private float originalSpeed = 18.0f; // was 7   // keep a refence to original speed when doing speed power ups
    public float speed = 18.0f; // was 7            // Initial Speed
    private float speedIncreaseLastTick;            // When last speed increased 
    private float speedIncreaseTime = 2.5f;         // time to speed increase
    private float speedIncreaseAmount = 0.1f;       // How much to increase by
    Vector3 moveVector = Vector3.zero;

    public bool isSafe = false;                 // Invunrbility yes/no

    //  Animation
    //  private Animator anim;



    public GameObject Player;                           // Refernce the player
    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions
    private ScoreManager theScoreManager;       // reference the score manager




    private void Start()
    {
       // speed = originalSpeed;
        controller = GetComponent<CharacterController>();           // pull in the character controller
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script

        //   anim = GetComponent<Animator>();                       // Pull in the animator
          // magnet = gameObject.GetComponent<Magnet>();

        
        //origionroot
        //  gameObject.transform.SetParent(OrigionRoot.transform, false);


    }

    
    private void Update()
    {






        /*if (!theGameManager.RunbuttonPressed)
        {
            return; // if game is not started, dont run below code
        }
        */
        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
         {
             speedIncreaseLastTick = Time.time;
             speed += speedIncreaseAmount;

             // change modifer text display
        //     GameManager.Instance.UpdateModifer(speed - originalSpeed);


         }
        

        // gather the inputs on which lane we should be in

        if (controller.isGrounded) //if grounded
        {
            isOnGround = true;             // seton ground to true - am on ground
            isJumping = false;              // we are not jumping so false
            //     anim.SetBool("Grounded", true);

            verticalVelocity = -0.1f;
        }


        if (MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
           
            MoveLane(false);
        }
        if (MobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveLane(true);
        }

        if (MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            //Jump
            isOnGround = false;                                                                         // no longer on ground
            isJumping = true;                                                                           // We are jumping
                                                                                                        //    anim.SetTrigger("Jump");
            verticalVelocity = jumpForce;


        }
        else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow) && isOnGround)       // if swipe down and we are on ground then slide
        {
            StartSliding();

            
        }

        else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow) && isJumping && !isOnGround)
        {
            //Drop from jump to slide 
            verticalVelocity = -5f;
            StartSliding();
        }
        else
        {
            verticalVelocity -= (gravity * Time.deltaTime); // slowly fall to ground level if normal jump
        }



        // Calculate where we should be in the future
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredLane == 0)
        {
            targetPosition += Vector3.left * LANE_DISTANCE;
        }
        else if (desiredLane == 2)
        {
            targetPosition += Vector3.right * LANE_DISTANCE;
        }
      

        // Calcuate move vector
      
        moveVector.x = (targetPosition - transform.position).x * speed;

        moveVector.y = verticalVelocity;
        moveVector.z = speed;
        controller.Move(moveVector * Time.deltaTime);

        //Rotate charatcter in direction of travel
        Vector3 dir = controller.velocity;
        if (dir != Vector3.zero)
        {
            dir.y = 0;
            transform.forward = Vector3.Lerp(transform.forward, dir, TURN_SPEED);

        }





     

    }



       

    private void MoveLane(bool goingRight)
    {
        
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }



    public void StartRunning()
    {
        isRunning = true;
      //  anim.SetTrigger("StartRunning");

        // can add camera looking at something before game starts here\

    }

    private void StartSliding()
    {
        //  anim.SetBool("Sliding", true);
        isSliding = true;
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);

        Invoke("StopSliding", 1.0f);
    }

    private void StopSliding()
    {
      //  anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
        isSliding = false;

    }


   






    


   

    private void OnTriggerEnter(Collider other)
    {
  
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (isSafe == false)
            {
                theScoreManager.SaveHighScore();
                theGameManager.RestartGame();
            }
        }

        if (other.gameObject.CompareTag("Enemy"))  // If hit Enemy
        {
            if (isSliding == true)                      // If the Player is sliding, they kick the feet out from under enemy and they die
            {

                other.gameObject.SetActive(false);          // Code to destroy the object that we collided with //AW change to pooling
                theScoreManager.AddEnemy(100);              // give  pints for killing enemy
            }
            else
            {
                if (isSafe == false)
                {
                    theScoreManager.SaveHighScore();
                    //  deathSound.Play();
                    theGameManager.RestartGame();  // AW want pause and choose to continue later
                                                   
                }
            }
        }
        



    }

    public void IsSafe()
    {
        isSafe = true;
    }
    public void IsNotSafe()
    {
        isSafe = false;
    }

   
}
