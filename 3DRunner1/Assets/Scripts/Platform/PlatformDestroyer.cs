using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestroyer : MonoBehaviour
{
    public GameObject platformDestructionPoint;
    
  
    void Start()
    {
        platformDestructionPoint = GameObject.Find("PlatformDestructionPoint");         // find the platform desruction point attached to the camera
    }

   
    void Update()
    {
        if (transform.position.x < platformDestructionPoint.transform.position.x)   // if gameobject past the point disable it
        {
            
            gameObject.SetActive(false);        // Disable the game object and have it availbe in the platform pool
        }

    }
}
