using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding in code to manage enemy following and bumping obstacles
[System.Serializable]
public enum SIDE { Left = -2,Mid = 0 ,Right = 2}
public enum HitX { Left, Mid, Right, None}
public enum HitY { Up, Mid, Down, Low, None }
public enum HitZ { Forward, Mid, Backward, None }

public class PlayerMotor : MonoBehaviour
{
    // Adding in code to manage enemy following and bumping obstacles
    public SIDE m_Side = SIDE.Mid;
    float NewXPos = 0f;
    public bool SwipeLeft, SwipeRight ,SwipeUp, SwipeDown;

    public float XValue;
    private float posX;
    public float SpeedDodge;
    public float jumpForce =8f ;       // How high we can jump
    private float posY;
    //Slide Settings
    public bool isSliding = false;              // am Sliding true/false
    // Jump Settings
    public bool isJumping;
    private float ColHeight;
    private float ColCenterY;

    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    private SIDE LastSide;
    public bool StopAllState = false;
   // public float stumbleTolerance = 10f;
   // private float stumbletime;
    public bool CanInput = true;
    private Animator m_Animator;
      public Collider CollisionCol;
    public CapsuleCollider cc1;
    private float CC1Height;
    private float CC1Center;
    private float timer;
    public FollowGuard guard;
    private float curDistance =0.6f;
    public bool Dead,hitmovingTrain;

    //  public CharacterController controller;

    // Define the level parameters

    private const float LANE_DISTANCE = 2.5f; //set the lane width
    private const float TURN_SPEED = 0.05f;     // how much to turn the charatcer in direction of lane chnage
    private int desiredLane = 1; // 0=left 1=middle 2=right


    // Define Character Parameters
    private bool isRunning;          // Are we running
    // Basic Movement
    private CharacterController controller;  // reference character controller
   // Copied above  [SerializeField] private float jumpForce;       // How high we can jump
    private float gravity = 20f; // was 12      // How strong is Gravity
    private float verticalVelocity;             // What is our falling speed
/*
    //Slide Settings
    public bool isSliding = false;              // am Sliding true/false

    // Jump Settings
    public bool isJumping; 
*/// bool to check if we are jumping
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
    private GameContinueManager theGameContinueManager;

    public GameObject whatIhit;
    public GameObject safeModeWhatIHit;
    public GameObject safeModeWhatIHitEnemy;


    private void Start()
    {
       // speed = originalSpeed;
        controller = GetComponent<CharacterController>();           // pull in the character controller
        m_Animator = GetComponent<Animator>();
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script
        theGameManager = FindObjectOfType<GameManager>();
        theGameContinueManager = FindObjectOfType<GameContinueManager>();

       // CapsuleCollider cc1 = GetComponent<CapsuleCollider>();

        //   anim = GetComponent<Animator>();                       // Pull in the animator
        // magnet = gameObject.GetComponent<Magnet>();


        //origionroot
        //  gameObject.transform.SetParent(OrigionRoot.transform, false);

        // new move code
       // stumbletime = stumbleTolerance;
        transform.position = Vector3.zero;          // New code - center player in middle
        m_Side = SIDE.Mid;
        ColHeight = controller.height;
        ColCenterY = controller.center.y;
        CC1Height = cc1.height;
        CC1Center = cc1.center.y;




    }


    private void Update()
    {

        //Copied from below
        if (!theGameManager.isRunning)
        {
            return; // if game is not started, dont run below code
        }
        guard.curDis = curDistance;
        CollisionCol.isTrigger = !CanInput;
       if (Dead)
        {
            if (curDistance < 3f)
            {
                 curDistance = Mathf.MoveTowards(curDistance, 0.0f, Time.deltaTime * 5f);
                 guard.Follow(transform.position, speed);
                if (hitmovingTrain)
                {
                    guard.HitMovingTrain();
                    return;
                }
                guard.CaughtPlayer();
                m_Animator.Play("caught");
              
            }
        }
      
            if (!CanInput)
            {
                controller.Move(Vector3.down * 10f * Time.deltaTime);
                Debug.Log("!CanInput");
                return;
            }
            // New movement code
            SwipeLeft = MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow) && CanInput;
            SwipeRight = MobileInput.Instance.SwipeRight || Input.GetKeyDown(KeyCode.RightArrow) && CanInput;
            SwipeUp = MobileInput.Instance.SwipeUp || Input.GetKeyDown(KeyCode.UpArrow) && CanInput;
            SwipeDown = MobileInput.Instance.SwipeDown || Input.GetKeyDown(KeyCode.DownArrow) && CanInput;

           // if (controller.isGrounded)
          //  {
          //      verticalVelocity = -0.1f;
         //   }

            if (SwipeLeft && !isSliding)
            {

                if (m_Side == SIDE.Mid)
                {
                    // NewXPos = -XValue;
                    LastSide = m_Side;
                    m_Side = SIDE.Left;
                    m_Animator.Play("dodgeLeft");
                    guard.LeftDodge();


                }
                else if (m_Side == SIDE.Right)
                {
                    //NewXPos = 0;
                    LastSide = m_Side;
                    m_Side = SIDE.Mid;
                    m_Animator.Play("dodgeLeft");
                    guard.LeftDodge();
                }
                else if (m_Side != LastSide)
                {

                    LastSide = m_Side;
                     Stumble("stumbleOffLeft", 0);
                   guard.Stumble();
                    curDistance = 0.6f;
                }
            }
            else if (SwipeRight && !isSliding)
            {

                if (m_Side == SIDE.Mid)
                {
                    // NewXPos = XValue;
                    LastSide = m_Side;
                    m_Side = SIDE.Right;
                    m_Animator.Play("dodgeRight");
                    guard.RightDodge();


                }
                else if (m_Side == SIDE.Left)
                {
                    // NewXPos = 0;
                    LastSide = m_Side;
                    m_Side = SIDE.Mid;

                    m_Animator.Play("dodgeRight");
                    guard.RightDodge();
                }
                else if (m_Side != LastSide)
                {

                    LastSide = m_Side;
                   Stumble("stumbleOffRight", 0);
                     guard.Stumble();
                    curDistance = 0.6f;
                }
            }

            curDistance = Mathf.MoveTowards(curDistance, 5f, Time.deltaTime * 0.1f);
        //Debug.Log("curDistance " + curDistance);
            guard.Follow(transform.position, speed);
            timer = Mathf.MoveTowards(timer, 0.0f, Time.deltaTime);
            if (timer <= 0.0f)
            {
                StopAllState = false;
                m_Animator.SetLayerWeight(1, 0);

            }
          //  stumbletime = Mathf.MoveTowards(stumbletime, stumbleTolerance, Time.deltaTime);
            Vector3 moveVector = new Vector3(posX - transform.position.x, posY * Time.deltaTime, speed * Time.deltaTime);
            posX = Mathf.Lerp(posX, (int)m_Side, Time.deltaTime * SpeedDodge);
            controller.Move(moveVector);
            Jump();
            Slide();





            /*   if (!theGameManager.isRunning)
                {
                    return; // if game is not started, dont run below code
                }
              */
            /*      if (Time.time - speedIncreaseLastTick > speedIncreaseTime)
                   {
                       speedIncreaseLastTick = Time.time;
                       speed += speedIncreaseAmount;
            */
            // change modifer text display
            //     GameManager.Instance.UpdateModifer(speed - originalSpeed);


            //   }


            // gather the inputs on which lane we should be in

            /*  if (controller.isGrounded) //if grounded
              {
                  isOnGround = true;             // seton ground to true - am on ground
                  isJumping = false;              // we are not jumping so false
                  //     anim.SetBool("Grounded", true);

                  verticalVelocity = -0.1f;
              }
      */

            /*   if (MobileInput.Instance.SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow))
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



               */



        

    }

       

  /*  private void MoveLane(bool goingRight)
    {
        
        desiredLane += (goingRight) ? 1 : -1;
        desiredLane = Mathf.Clamp(desiredLane, 0, 2);

    }

*/

    public void StartRunning()
    {
        isRunning = true;
      //  anim.SetTrigger("StartRunning");

        // can add camera looking at something before game starts here\

    }

 /*   private void StartSliding()
    {
        //  anim.SetBool("Sliding", true);
        isSliding = true;
        controller.height /= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y / 2, controller.center.z);

        Invoke("StopSliding", 1.0f);
    }
 */
  /*  private void StopSliding()
    {
      //  anim.SetBool("Sliding", false);
        controller.height *= 2;
        controller.center = new Vector3(controller.center.x, controller.center.y * 2, controller.center.z);
        isSliding = false;

    }
  */

   

    // new jump system 
    public void Jump()
    {
        if (controller.isGrounded)
        {
            if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                PlayAnimation("Landing");
                isJumping = false;
            }
            if (SwipeUp)
            {
                posY = jumpForce;

                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                guard.Jump();
                isJumping = true;

            }
        }
        else
        {

            posY -= jumpForce * 2 * Time.deltaTime;
            

            if (controller.velocity.y < -0.1f)
            {
                PlayAnimation("Falling");
            }

        }
        
    }

    // new slide system

    internal float SlideCounter;
    public void Slide()
    {
        SlideCounter -= Time.deltaTime;
        if (SlideCounter <= 0f)
        {
            SlideCounter = 0f;
            controller.center = new Vector3(0, ColCenterY, 0);
            controller.height = ColHeight;
            cc1.height = CC1Height;
            cc1.center = new Vector3(0, CC1Center, 0);
            isSliding = false;
        }

        if (SwipeDown)
        {
          //  SlideCounter = 0.2f;
           SlideCounter = 0.8f;
            posY -= 10f;
            controller.center = new Vector3(0, ColCenterY /2f, 0);
            controller.height = ColHeight/2f;
            cc1.height = CC1Height / 2f;
            cc1.center = new Vector3(0, CC1Center / 2f, 0);
           m_Animator.CrossFadeInFixedTime("roll", 0.1f);   
           
            isSliding = true;
            isJumping = false;

        }
    }

    
    public void OnCharacterColliderHit(Collider col)
    {
        Debug.Log(" OnCharacterColliderHit " + col);

        hitX = GetHitX(col);
        hitY = GetHitY(col);
        hitZ = GetHitZ(col);

        if (isSafe)
        {
            Debug.Log("Is safe");
            safeModeWhatIHit = col.gameObject;
            safeModeWhatIHit.SetActive(false);
            StartCoroutine(reEnableColidedObstacleSafeMode());
            ResetCollision();
        }

        if (hitZ == HitZ.Forward && hitX == HitX.Mid)  // play death anim
        {
            if (hitY == HitY.Low)
            {
                Stumble("stumble_low",0);
                
                Debug.Log("Stumble low");
                ResetCollision();
            }
            else if (hitY == HitY.Down)
            {
                StartCoroutine(DeathPlayer("death_lower", whatIhit));
                Debug.Log("Death low");
                ResetCollision();
            }
            else if (hitY == HitY.Mid)
            {
                if (col.tag == "MovingTrain" )
                {
                    whatIhit = col.gameObject;
                    Debug.Log("I HIT " + whatIhit);
                    StartCoroutine(DeathPlayer("death_movingTrain", whatIhit));
                    hitmovingTrain = true;

                    Debug.Log("Movin train");
                    ResetCollision();

                }
                else if (col.tag != "Ramp" || col.tag != "Ground" || col.tag != "Coin" )
                {
                    whatIhit = col.gameObject;
                    StartCoroutine(DeathPlayer("death_bounce", whatIhit));
                    Debug.Log("Death Bounce");
                    ResetCollision();
                }
            }
            else if (hitY == HitY.Up && !isSliding )
            {
                whatIhit = col.gameObject;
                StartCoroutine(DeathPlayer("death_upper", whatIhit));
                Debug.Log("Death Upper");
                ResetCollision();
            }
        }
        else if (hitZ == HitZ.Mid)
        {
            if (hitX == HitX.Right)
            {
                m_Side = LastSide;
                Stumble("stumbleSideRight",0);
                // now Stumble("stumble_right);
                Debug.Log("Stumble right");
                ResetCollision();
            }
            else if (hitX == HitX.Left)
            {
                m_Side = LastSide;
                Stumble("stumbleSideLeft",0);
                Debug.Log("Stumble left");
                ResetCollision();
            }
        }
        else
        {
            if (hitX == HitX.Right)
            {
                m_Animator.SetLayerWeight(1, 1);
                Stumble("stumbleCornerRight",1);
              //  LastSide = m_Side;
                Debug.Log("Stumble corner right");
                ResetCollision();
            }
            else if (hitX == HitX.Left)
            {
                m_Animator.SetLayerWeight(1, 1);
                Stumble("stumbleCornerLeft",1);
                //LastSide = m_Side;
                Debug.Log("Stumble corner left");
                ResetCollision();
            }
        }
    }

    public HitX GetHitX(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;
        float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
        float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
        float average = (min_x + max_x) / 2f -col_bounds.min.x;
        
        HitX hit;
        if (average > col_bounds.size.x -0.33f)
        {
            hit = HitX.Right;

        }
        else if (average < 0.33f)
        {
            hit = HitX.Left;
        }
        else
        {
            hit = HitX.Mid;
        }
        return hit;
    }

    public HitY GetHitY(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y) /char_bounds.size.y;
      
        HitY hit;
        if (average < 0.17f)
        {
            hit = HitY.Low;
        }
        else if (average < 0.33f)
        {
            hit = HitY.Down;

        }
        else if (average < 0.66f)
        {
            hit = HitY.Mid;
        }
        else
        {
            hit = HitY.Up;
        }
        return hit;

    }


    public HitZ GetHitZ(Collider col)
    {
        Bounds char_bounds = controller.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f -col_bounds.min.z) / char_bounds.size.z;
 
        HitZ hit;
        
        if (average < 0.33f)
        {
            hit = HitZ.Forward; // was backware

        }
        else if (average < 0.66f)
        {
            hit = HitZ.Mid;
        }
        else
        {
            hit = HitZ.Backward; // was forward
        }
        return hit;

    }

    private void ResetCollision()
    {
        Debug.Log ("hitX + hitY + hitZ " + hitX + hitY + hitZ ) ;
        hitX = HitX.None;
        hitY = HitY.None;
        hitZ = HitZ.None;

    }

    public void PlayAnimation(string anim)
    {
        if (StopAllState || Dead)
        {
            return;
        }
        else
        {
             m_Animator.Play(anim);
        }
    }

   

    public IEnumerator DeathPlayer(string anim, GameObject hitobj)
    {

        Dead = true;
        StopAllState = true;
        m_Animator.SetLayerWeight(1, 0);
        m_Animator.Play(anim);

        
         yield return new WaitForSeconds(0.2f);
       // AW yield return new WaitForSeconds(7.2f);

        hitobj.SetActive(false);

        // try add in my code to bring up death menu
        if (isSafe == false)
        {
            theScoreManager.SaveHighScore();  // AW maybe not best place for this
                                              //  whatIhit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
                                              // whould be better with other.gameObject.transform.parent.parent.gameObject
                                              // but does not work for obstalce
                                              //  whatIhit.SetActive(false);
            Debug.Log("Shoudl continue screen");
            theGameContinueManager.PlayerDiedContinueOption();

            //StartCoroutine(reEnableColidedObstacle());
            // other.gameObject.SetActive(false);
            Debug.Log("disable");
            //PlayAnimation("Landing");
        }
        else
                if (isSafe == true)
        {
            Debug.Log("shoudl destroy object");
         //   safeModeWhatIHit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
                                                                                // whould be better with other.gameObject.transform.parent.parent.gameObject
                                                                                // but does not work for obstalce
        //    safeModeWhatIHit.SetActive(false);
            StartCoroutine(reEnableColidedObstacleSafeMode());
        }

        // end my code
        CanInput = false;

    }

    public IEnumerator StumbleDeathPlayer(string anim)
    {

        Dead = true;
        StopAllState = true;
        m_Animator.SetLayerWeight(1, 0);
        m_Animator.Play(anim);


        yield return new WaitForSeconds(1.2f);
        // AW yield return new WaitForSeconds(7.2f);

        //hitobj.SetActive(false);

        // try add in my code to bring up death menu
        if (isSafe == false)
        {
            theScoreManager.SaveHighScore();  // AW maybe not best place for this
                                              //  whatIhit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
                                              // whould be better with other.gameObject.transform.parent.parent.gameObject
                                              // but does not work for obstalce
                                              //  whatIhit.SetActive(false);
            Debug.Log("Shoudl continue screen");
            theGameContinueManager.PlayerDiedContinueOption();

            //StartCoroutine(reEnableColidedObstacle());
            // other.gameObject.SetActive(false);
            Debug.Log("disable");
            //PlayAnimation("Landing");
        }
        else
                if (isSafe == true)
        {
           // Debug.Log("shoudl destroy object");
            //   safeModeWhatIHit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
            // whould be better with other.gameObject.transform.parent.parent.gameObject
            // but does not work for obstalce
            //    safeModeWhatIHit.SetActive(false);
           // StartCoroutine(reEnableColidedObstacleSafeMode());
        }

        // end my code
        CanInput = false;

    }
    public void Stumble(string anim,int layer)
    {
       
       
        
        StopAllState = true;
         m_Animator.Play(anim);
        
        timer=m_Animator.GetCurrentAnimatorStateInfo(layer).length;
        if (curDistance < 3f)
        {
            Debug.Log("In stublem death");
           // guard.Stumble();  //AW adding in
            StartCoroutine(StumbleDeathPlayer("stumble_low"));
            

            return;
        }
        curDistance = 0.6f;
        // stumbletime -= 6f;
        ResetCollision();
    }

    private void OnTriggerEnter(Collider other)
    {
  
        if (other.gameObject.CompareTag("Obstacle"))
        {
            if (isSafe == false)
            {
                theScoreManager.SaveHighScore();  // AW maybe not best place for this
                whatIhit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
                                                                            // whould be better with other.gameObject.transform.parent.parent.gameObject
                                                                            // but does not work for obstalce
                whatIhit.SetActive(false);
                theGameContinueManager.PlayerDiedContinueOption();

                //StartCoroutine(reEnableColidedObstacle());
               // other.gameObject.SetActive(false);
                Debug.Log("disable");
            }
            else
                if (isSafe == true)
            {
                Debug.Log("shoudl destroy object");
                safeModeWhatIHit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
                                                                                    // whould be better with other.gameObject.transform.parent.parent.gameObject
                                                                                    // but does not work for obstalce
                safeModeWhatIHit.SetActive(false);
                StartCoroutine(reEnableColidedObstacleSafeMode());
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
                   safeModeWhatIHitEnemy = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
                                                                                                     // whould be better with other.gameObject.transform.parent.parent.gameObject
                                                                                                     // but does not work for obstalce
                    safeModeWhatIHitEnemy.SetActive(false);
                    theGameContinueManager.PlayerDiedContinueOption();  
                                                   
                }
            }
        }
        



    }

    public void IsSafe()
    {
        isSafe = true;
        

        //whatIhit = other.gameObject.transform.parent.gameObject;    // Disable the obstacle i collided with AW need to make sure it re-appers later
        // whould be better with other.gameObject.transform.parent.parent.gameObject
        // but does not work for obstalce
        // whatIhit.SetActive(false);

        Debug.Log("SAFE MODE");

        // try reable character
        CanInput = true;
        StartCoroutine(reEnableColidedObstacle());
    }
    public void IsNotSafe()
    {
        isSafe = false;
        Debug.Log("### END SAFE MODE");
    }

    IEnumerator reEnableColidedObstacle()
        {
        Debug.Log("starting re-enable");
                yield return new WaitForSeconds(5f);
                whatIhit.SetActive(true);
        }

    IEnumerator reEnableColidedObstacleSafeMode()
    {
        Debug.Log("starting re-enable");
        yield return new WaitForSeconds(5f);
        safeModeWhatIHit.SetActive(true);
    }

    public void SetPlayerIdle()
    {
        Dead = false;
        PlayAnimation("Landing");  // AW have an idle animation here
        guard.Running();

    }

}
