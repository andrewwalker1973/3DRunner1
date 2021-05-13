using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{

    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions

    void Start()
    {
       theGameManager = FindObjectOfType<GameManager>();                // reference the game manager script
    }

     private void OnTriggerEnter(Collider other)
    {
        

        if (other.gameObject.tag == "Player" )                  // if obstacle hit player
        {
            
            theGameManager.RestartGame();                       // call restart function
            gameObject.SetActive(false);                        // Disable the object.
        }

        
    }
}
