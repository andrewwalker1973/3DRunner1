using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    // private const float LANE_DISTANCE = 2.5f; //set the lane width
    private const float LANE_DISTANCE = 2f; //set the lane width
    private const float TURN_SPEED = 0.05f;

    //
    private bool isRunning = true;
    //Slide Settings
    public bool isSliding = false;                                              // am Sliding true/false

    public bool isSafe = false;

    //  Animation
    //  private Animator anim;
    // Magnet magnet;
    // Movement
    private CharacterController controller;
    public float jumpForce = 4f; // WAS 4
    private float gravity = 12f;
    private float verticalVelocity;


    private int desiredLane = 1; // 0=left 1=middle 2=right

    // speed Modifier
    private float originalSpeed = 18.0f; // was 7
    public float speed = 18.0f; // was 7
    private float speedIncreaseLastTick;
    private float speedIncreaseTime = 2.5f;
    private float speedIncreaseAmount = 0.1f;

    // added in code for power up process
    /*  public enum PowerUpType { Magnet }
      public Dictionary<PowerUpType, PowerUp> powerUpDictionary;
      private float powerUpDuration = 10f;
      private List<PowerUpType> itemsToRemove;
    */
    public GameObject Player;
    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions
    private ScoreManager theScoreManager;       // reference the score manager

    //   public GameObject MagnetCollider;
    //  CoinMove coinMoveScript;

    //attepmpt at origion root
    //    public GameObject OrigionRoot;


    private void Start()
    {
        speed = originalSpeed;
        controller = GetComponent<CharacterController>();
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script

        //   anim = GetComponent<Animator>();
          // magnet = gameObject.GetComponent<Magnet>();

        
        //origionroot
        //  gameObject.transform.SetParent(OrigionRoot.transform, false);


    }

    
    private void Update()
    {






        if (!isRunning)
        {
            return; // if game is not started, dont run below code
        }

        if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
        {
            speedIncreaseLastTick = Time.time;
            speed += speedIncreaseAmount;

            // change modifer text display
       //     GameManager.Instance.UpdateModifer(speed - originalSpeed);


        }

        // gather the inputs on which lane we should be in

        if (MobileInput.Instance.SwipeLeft)
        {
            MoveLane(false);
        }
        if (MobileInput.Instance.SwipeRight)
        {
            MoveLane(true);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // move left
            MoveLane(false);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // move right
            MoveLane(true);
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
        Vector3 moveVector = Vector3.zero;
        //  moveVector.x = (targetPosition - transform.position).normalized.x * speed; // character was shakaing 

        moveVector.x = (targetPosition - transform.position).x * speed;



        // Calc Y
        if (controller.isGrounded) //if grounded
        {
            //bool isGrounded = true;
       //     anim.SetBool("Grounded", true);

            verticalVelocity = -0.1f;


            //   if (Input.GetKeyDown(KeyCode.Space))
            if (MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow))
            {
                //Jump
            //    anim.SetTrigger("Jump");
                verticalVelocity = jumpForce;


            }
            else if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                //slide
                StartSliding();
            }
        }
        else
        {
            // bool isGrounded = false;
            //  Debug.Log("NOTGrounded "); 
            verticalVelocity -= (gravity * Time.deltaTime); // slowly fall to ground level
                                                            //  Debug.Log("Floating");
                                                            // Fast Falling area
                                                            // if (Input.GetKeyDown(KeyCode.M))
            if (MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow))
            {
                verticalVelocity = -jumpForce;  //drop immediatly to ground
                                                // Add code to drop quick and then slide
                StartSliding();
                                                // anim.SetBool("DropSlide", true);

            }



        }



        moveVector.y = verticalVelocity;
        moveVector.z = speed;


        // move player
        //Debug.Log("Move Vecto" + moveVector);
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
            Debug.Log("Hit obstalce");
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
                                                   //  movespeed = moveSpeedStore;     //Reset back to starting game speed
                                                   //   speedMilestoneCount = speedMilestoneCountStore;  //Reset back to starting game speed increase
                                                   //  speedIncreaseMilestone = speedIncreaseMilestoneStore; //Reset back to starting game spped milestone
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
