using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{

    public float speed = 18.0f; // was 7 
    public bool isSafe;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsNotSafe()
    {
        isSafe = false;
        Debug.Log("### END SAFE MODE");
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
     //   CanInput = true;
     //   StartCoroutine(reEnableColidedObstacle());
    }

    public void SetPlayerIdle()
    {
      //  Dead = false;
     //   PlayAnimation("Landing");  // AW have an idle animation here
    //    guard.Running();

    }
}
