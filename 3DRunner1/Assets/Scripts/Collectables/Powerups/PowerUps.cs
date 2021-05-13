using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public bool doublePoints;   // which powerup is being activated
    public bool safeMode;       // which powerup is being activated

    public float powerupLength; // How long can it be powered up for

    private PowerUpManager thepowerUpManager;       // referenc ethe powerup manager

    


    void Start()
    {
        thepowerUpManager = FindObjectOfType<PowerUpManager>();         // find the Powerup Manager script
    }

   
    

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))                                          // if player hits object
        {
            thepowerUpManager.ActivatePowerUp(doublePoints, safeMode, powerupLength);       // send all details to powerup manger
        }

        gameObject.SetActive(false);                                                        // disbale the powerup onject
    }
}
