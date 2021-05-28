using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{

    public bool doublePoints;   // which powerup is being activated
    public bool shieldMode;       // which powerup is being activated
    public bool magnet;         // magent powerup
    public bool fasterMode;
    public bool slowerMode;

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
            
            thepowerUpManager.ActivatePowerUp(doublePoints, shieldMode, magnet, fasterMode, slowerMode, powerupLength);       // send all details to powerup manger
        }

        gameObject.SetActive(false);                                                        // disbale the powerup onject
    }
}
