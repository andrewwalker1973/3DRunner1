using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{

    private bool doublePoints;          // which powerup is being activated
    private bool safeMode;              // which powerup is being activated

    private bool powerupActive;        // which powerup is active
    private float powerUpLenghtCounter; // How long is it active for

    private float normalPointsPerSecond;        // to store normal points per second
    private float LowObstacleRate;              // to store lowobstacle percentage rate

    private ScoreManager theScoreManager;       // need the score manager script
    private PlatformGenerator thePlatformGenerator;     // need the platformmanager script
    private GameManager theGameManager;                 // refernce the game manager script

    private PlatformDestroyer[] lowObstacleList;          // List of all the low obstacle in the game


    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
    }


    void Update()
    {
        if (powerupActive)
        {
            powerUpLenghtCounter -= Time.deltaTime;     // Start decreasing the time

            if (theGameManager.powerUpReset)             // if reset counter is true, disable all powerups
            {
                powerUpLenghtCounter = 0;           // disable all powerups on rest
                theGameManager.powerUpReset = false;    // flag to disable powerups in game manager
            }

            if (doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 2;        // double the points per second
                theScoreManager.shouldDouble = true;
            }

            if (safeMode)
            {
       //         thePlatformGenerator.randomLowObstacleThreshold = 0;        // set low obstacle rate to 0
            }


            if (powerUpLenghtCounter <= 0)                                              // when at 0
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond;             // set points per second back
             //   thePlatformGenerator.randomLowObstacleThreshold = LowObstacleRate;  // set low obstacle rate back
                theScoreManager.shouldDouble = false;                               // stop doubling points
                powerupActive = false;                                              // disable the powerup
            }
        }
    }

    public void ActivatePowerUp(bool points, bool safe, float time)     // get values from powerup scripts
    {
        doublePoints = points;              // set value to be local value
        safeMode = safe;                    // set value to be local value
        powerUpLenghtCounter = time;        // set value to be local value

        normalPointsPerSecond = theScoreManager.pointsPerSecond;    // Save original settings
     //   LowObstacleRate = thePlatformGenerator.randomLowObstacleThreshold;  // Save original settings

        if (safeMode)
        {
            lowObstacleList = FindObjectsOfType<PlatformDestroyer>();      // generate a list of all low obstacles and disable
            for (int i = 0; i < lowObstacleList.Length; i++)
            {
                if (lowObstacleList[i].gameObject.name == "Low Obstacle(Clone)")            // look for all objects call Low Obstacle Clone
                {
                    lowObstacleList[i].gameObject.SetActive(false);                         // Disable them
                }
            }
        }
        powerupActive = true;                                               // set power up to be true


    }
}
