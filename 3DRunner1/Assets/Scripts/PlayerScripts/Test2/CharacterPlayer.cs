using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum LANE { Left = -2 ,Mid = 0 ,Right = 2}
public enum HitX { Left, Mid, Right, None }
public enum HitY { Up, Mid, Down, Low, None }

public enum HitZ { Forward, Mid, Backward, None }


public class CharacterPlayer : MonoBehaviour
{

    public LANE m_Side = LANE.Mid;
 
   // float NewXPos = 0f;
    public bool SwipeLeft, SwipeRight, SwipeUp,SwipeDown;
    public float XValue;
    public CharacterController m_char;
    public Animator m_Animator;
    private float x;
    public float SpeedDodge;
    public float JumpPower = 7f;
    private float y;
    public bool InJump;
    public bool InRoll;
    public float FwdSpeed = 7f;
    private float ColHeight;
    private float ColCenterY;

    public HitX hitX = HitX.None;
    public HitY hitY = HitY.None;
    public HitZ hitZ = HitZ.None;
    private LANE LastLane;
    public bool StopAllState = false;
    public float stumbleTolerance = 10f;
    private float stumbletime;
    public bool CanInput = true;
    public Collider CollisionCol;




    // Start is called before the first frame update
    void Start()
    {
        stumbletime = stumbleTolerance;
        m_char = GetComponent<CharacterController>();
        ColHeight = m_char.height;
        ColCenterY = m_char.center.y;
        m_Animator = GetComponent<Animator>();
        transform.position = Vector3.zero;
        m_Side = LANE.Mid;
        Debug.Log("SIDE  " + m_Side);
    }

    // Update is called once per frame
    void Update()
    {
        CollisionCol.isTrigger = !CanInput;
        if(!CanInput)
        {
            m_char.Move(Vector3.down * 10f * Time.deltaTime);
            return;
        }
        SwipeLeft = Input.GetKeyDown(KeyCode.LeftArrow)&&CanInput;
        SwipeRight = Input.GetKeyDown(KeyCode.RightArrow)&&CanInput;
        SwipeUp = Input.GetKeyDown(KeyCode.UpArrow)&&CanInput;
        SwipeDown = Input.GetKeyDown(KeyCode.DownArrow)&&CanInput;
 

        if (SwipeLeft && !InRoll)
        {
            Debug.Log("Left");
            Debug.Log("SIDE" + m_Side);
            if (m_Side == LANE.Mid)
            {
                // NewXPos = -XValue;
                LastLane = m_Side;
                m_Side = LANE.Left;
                Debug.Log("m_Side in left " + m_Side);
                PlayAnimation("dodgeLeft");

            }
            else if (m_Side == LANE.Right)
            {
                // NewXPos = 0;
                LastLane = m_Side;
                m_Side = LANE.Mid;
                Debug.Log("m_Side in left " + m_Side);
                PlayAnimation("dodgeLeft");
            }
            else if (m_Side != LastLane)
            {
                LastLane = m_Side;
                Stumble("stumbleOffLeft");
            }
        }
        else if (SwipeRight && !InRoll)
        {
            Debug.Log("Right");
            if (m_Side == LANE.Mid)
            {
                // NewXPos = XValue;
                LastLane = m_Side;
                m_Side = LANE.Right;
                Debug.Log("m_Side in Right " + m_Side);
                PlayAnimation("dodgeRight");
            }
            else if (m_Side == LANE.Left)
            {
                // NewXPos = 0;
                LastLane = m_Side;
                m_Side = LANE.Mid;
                PlayAnimation("dodgeRight");
                Debug.Log("m_Side in Right " + m_Side);
            }
            else if (m_Side != LastLane)
            {
                LastLane = m_Side;
                Stumble("stumbleOffRight");
            }


        }
        if (m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >=1)
        {
            m_Animator.SetLayerWeight(1, 0);
            StopAllState = false;
            stumbletime = Mathf.MoveTowards(stumbletime, stumbleTolerance, Time.deltaTime);
        }
        Vector3 moveVector = new Vector3(x - transform.position.x, y * Time.deltaTime, FwdSpeed * Time.deltaTime);
      //  Debug.Log("m_side " + (int)m_Side);
        x = Mathf.Lerp(x, (int)m_Side, Time.deltaTime * SpeedDodge);
        m_char.Move(moveVector);
        Jump();
        Roll();

    }

    private void ResetCollision()
    {
        Debug.Log("hitX + hitY + hitZ " + hitX + hitY + hitZ);
        hitX = HitX.None;
        hitY = HitY.None;
        hitZ = HitZ.None;

    }
    public void Jump()
    {
        
        if (m_char.isGrounded)
        {
            if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
            {
                PlayAnimation("Landing");
                InJump = false;

            }
            if (SwipeUp)
            {
                y = JumpPower;
                m_Animator.CrossFadeInFixedTime("Jump", 0.1f);
                InJump = true;
            }

        }
        else
        {
                y -= JumpPower * 2 * Time.deltaTime;
                if (m_char.velocity.y < -0.1f)
                {
                PlayAnimation("Falling");
                }
            
        }
    }

    internal float RollCounter;
    public void Roll()
    {
        RollCounter -= Time.deltaTime;
        if (RollCounter <= 0f)
        {
            RollCounter = 0f;
            m_char.center = new Vector3(0, ColCenterY, 0);
            m_char.height = ColHeight;
            InRoll = false;
        }
        if (SwipeDown)
        {
            RollCounter = 0.2f;
            y -= 10f;
            m_char.center = new Vector3(0, ColCenterY/2f, 0);
            m_char.height = ColHeight/2f;
            m_Animator.CrossFadeInFixedTime("roll", 0.1f);
            InRoll = true;
            InJump = false;


        }
    }

    public void PlayAnimation(string anim)
    {
        if (StopAllState /*|| Dead*/)
        {
            return;
       }
        else
        {
            m_Animator.Play(anim);
        }
    }
    
    public IEnumerator DeathPlayer(string anim)
    {
        StopAllState = true;
        m_Animator.SetLayerWeight(1, 0);
        Debug.Log("Anim is " + anim);
        m_Animator.Play(anim);
        yield return new WaitForSeconds(0.2f);
        CanInput = false;
    }

    public void Stumble(string anim)
    {
        m_Animator.ForceStateNormalizedTime(0.0f);
        StopAllState = true;       
            m_Animator.Play(anim);
        if (stumbletime < stumbleTolerance /2f)
        {
           StartCoroutine(DeathPlayer("stumble_low"));
            return;
        }
        stumbletime -= 6f;
        ResetCollision();
        
    }

    public void OnCharacterColliderHit(Collider col)
    {
        hitX = GetHitX(col);
        hitY = GetHitY(col);
        hitZ = GetHitZ(col);

        if (hitZ == HitZ.Forward && hitX == HitX.Mid) // DEATH
        {
            if (hitY == HitY.Low)
            {
                Stumble("stumble_low");
                Debug.Log("Stumble Low");
            //    ResetCollision();
            }
            else if (hitY == HitY.Down)
            {
                StartCoroutine(DeathPlayer("death_lower"));
               // PlayAnimation("death_lower");
                Debug.Log("Death Low");
               // ResetCollision();
            }
            else if (hitY == HitY.Mid)
            {
                if (col.tag == "MovingTrain")
                {
                    //  PlayAnimation("death_lower");
                    StartCoroutine(DeathPlayer("death_lower"));
                    Debug.Log("Detah low ");
                 //   ResetCollision();
                }
                else if (col.tag != "Ramp")
                {
                    //  PlayAnimation("death_bounce");
                    StartCoroutine(DeathPlayer("death_bounce"));
                    Debug.Log("Death Bounce");
                 //   ResetCollision();
                }
            }else if (hitY == HitY.Up && !InRoll)
            {
                //  PlayAnimation("death_upper");
                StartCoroutine(DeathPlayer("death_upper"));
                Debug.Log("Death Upper");
               // ResetCollision();
            }

        }
        else if (hitZ == HitZ.Mid)
        {
            if (hitX == HitX.Right)
            {
                m_Side = LastLane;
                Stumble("stumbleSideRight");
                ResetCollision();
                Debug.Log("Stumble right");
            }
            else if (hitX == HitX.Left)
            {
                m_Side = LastLane;
                Stumble("stumbleSideLeft");
                ResetCollision();
                Debug.Log("Stumble left");
            }
        }else
        {
            if (hitX == HitX.Right)
            {
                m_Animator.SetLayerWeight(1, 1);
                Stumble("stumbleCornerRight");
                ResetCollision();
                Debug.Log("corner righr");

            }
            else if (hitX == HitX.Left)
            {
                m_Animator.SetLayerWeight(1, 1);
                Stumble("stumbleCornerLeft");
                Debug.Log("Corner left");
                ResetCollision();
            }
        }


    }

        public HitX GetHitX(Collider col)
        {
                Bounds char_bounds = m_char.bounds;
                Bounds col_bounds = col.bounds;
                float min_x = Mathf.Max(col_bounds.min.x, char_bounds.min.x);
                float max_x = Mathf.Min(col_bounds.max.x, char_bounds.max.x);
                float average = (min_x + max_x) / 2f - col_bounds.min.x;

                HitX hit;
                if (average > col_bounds.size.x - 0.33f)
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
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_y = Mathf.Max(col_bounds.min.y, char_bounds.min.y);
        float max_y = Mathf.Min(col_bounds.max.y, char_bounds.max.y);
        float average = ((min_y + max_y) / 2f - char_bounds.min.y) / char_bounds.size.y;

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
        Bounds char_bounds = m_char.bounds;
        Bounds col_bounds = col.bounds;
        float min_z = Mathf.Max(col_bounds.min.z, char_bounds.min.z);
        float max_z = Mathf.Min(col_bounds.max.z, char_bounds.max.z);
        float average = ((min_z + max_z) / 2f - char_bounds.min.z) / char_bounds.size.z;

        HitZ hit;

        if (average < 0.33f)
        {
            hit = HitZ.Backward; // was backware

        }
        else if (average < 0.66f)
        {
            hit = HitZ.Mid;
        }
        else
        {
            hit = HitZ.Forward; // was forward
        }
        return hit;
    }
}

