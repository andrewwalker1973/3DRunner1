using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FollowGuard : MonoBehaviour
{
    public Animator guardAnimator;
    public Transform Guard;
    public float curDis;

 

    public void Jump()
    {
        StartCoroutine(PlayAnim("Guard_jump"));
    }

    public void Running()
    {
        StartCoroutine(PlayAnim("Guard_Run"));
    }
    public void LeftDodge()
    {
        StartCoroutine(PlayAnim("Guard_dodgeLeft"));
    }

    public void RightDodge()
    {
        StartCoroutine(PlayAnim("Guard_dodgeRight"));
    }

    public void Stumble()
    {
        StopAllCoroutines();
       StartCoroutine(PlayAnim("Guard_grap after"));
    }

    public void CaughtPlayer()
    {
        guardAnimator.Play("catch_1");
    }

    public void HitMovingTrain()
    {
        StartCoroutine(PlayAnim("Guard_death_movingTrain"));
    }
    private IEnumerator PlayAnim(string anim)
    {
        yield return new WaitForSeconds(curDis / 5f);
        guardAnimator.Play(anim);
    }
   
   public void Follow(Vector3 pos, float speed)
    {
         Vector3 position = pos - Vector3.forward * curDis;
        
          Guard.position = Vector3.Lerp(Guard.position, position, Time.deltaTime * speed / curDis);

       
    }
}
