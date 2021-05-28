using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PowerUpManager : MonoBehaviour
{

    private bool doublePoints;          // which powerup is being activated
    private bool shieldMode;              // which powerup is being activated
    private bool magnet;                // magnet powerup
    private bool fasterMode;
   private bool slowerMode;


    private bool powerupActive;        // which powerup is active
    private float powerUpLenghtCounter; // How long is it active for

    private float normalPointsPerSecond;        // to store normal points per second
    private float LowObstacleRate;              // to store lowobstacle percentage rate

    private ScoreManager theScoreManager;       // need the score manager script
   // private PlatformGenerator thePlatformGenerator;     // need the platformmanager script
    private GameManager theGameManager;                 // refernce the game manager script
    private PlayerMotor theplayerMotor;                    // Refernce the player motor script

    private PlatformDestroyer[] lowObstacleList;          // List of all the low obstacle in the game

    public GameObject PowerUpScoreImage;
    public GameObject PowerUpSafeImage;
    public GameObject PowerUpMagentImage;
    public GameObject PowerUpFasterImage;
   public GameObject PowerUpSlowerImage;

    public GameObject coinDetectorObj;
    float magnetDuration;
    float fasterDuration;
    float slowerDuration;

    private float normalSpeed;


    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();
        theGameManager = FindObjectOfType<GameManager>();
        theplayerMotor = FindObjectOfType<PlayerMotor>();

        coinDetectorObj = GameObject.FindGameObjectWithTag("CoinDetector");
        coinDetectorObj.SetActive(false);
    }


    void Update()
    {
        if (powerupActive)
        {
            if (fasterMode)
            {
                Debug.Log("Setting faster Time");
                fasterDuration -= Time.deltaTime;
            }

            if (slowerMode)
            {
                Debug.Log("Setting Slower Time");
                slowerDuration -= Time.deltaTime;
            }

            powerUpLenghtCounter -= Time.deltaTime;     // Start decreasing the time
            magnetDuration -= Time.deltaTime;
            
  

            if (theGameManager.powerUpReset)             // if reset counter is true, disable all powerups
            {
                powerUpLenghtCounter = 0;           // disable all powerups on rest
                theGameManager.powerUpReset = false;    // flag to disable powerups in game manager
            }

            if (doublePoints)
            {
                PowerUpScoreImage.SetActive(true);
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 2;        // double the points per second
                theScoreManager.shouldDouble = true;
            }

            if (shieldMode)
            {
               // theplayerMotor.isSafe = true;
              //  PowerUpSafeImage.SetActive(true);
            }

            
            if (magnetDuration <= 0)
            {
                coinDetectorObj.SetActive(false);
                PowerUpMagentImage.SetActive(false);
              
            }

            if (fasterMode && fasterDuration <= 0)
            {
                Debug.Log(" fasterDuration " + fasterDuration);
                theplayerMotor.speed = normalSpeed;
                PowerUpFasterImage.SetActive(false);

            }

            if (slowerMode && slowerDuration <= 0)
            {
                Debug.Log(" slowerDuration " + slowerDuration);
                theplayerMotor.speed = normalSpeed;
                PowerUpSlowerImage.SetActive(false);

            }
           

            if (powerUpLenghtCounter <= 0)                                              // when at 0
            {
               
                
                theScoreManager.pointsPerSecond = normalPointsPerSecond;             // set points per second back
             //   thePlatformGenerator.randomLowObstacleThreshold = LowObstacleRate;  // set low obstacle rate back
                theScoreManager.shouldDouble = false;                               // stop doubling points
                powerupActive = false;                                              // disable the powerup
                theplayerMotor.IsNotSafe();
                PowerUpScoreImage.SetActive(false);
                PowerUpSafeImage.SetActive(false);

                
                
            }
        }
    }

    public void ActivatePowerUp(bool points, bool shield, bool mag, bool fasterM, bool slowerM, float time)     // get values from powerup scripts
    {

        doublePoints = points;              // set value to be local value
        shieldMode = shield;                    // set value to be local value
        magnet = mag;                       // set value to be local value
        fasterMode = fasterM;               // set faster mode to be local value
        slowerMode = slowerM;

        Debug.Log(" vars " + points + shield + mag + fasterM + /*slowerM +*/  time);
        powerUpLenghtCounter = time;        // set value to be local value

        normalPointsPerSecond = theScoreManager.pointsPerSecond;    // Save original settings
        normalSpeed = theplayerMotor.speed;                         // Save Speed settings

        if (shieldMode)
        {
            theplayerMotor.IsSafe();
            PowerUpSafeImage.SetActive(true);
        }

        if (magnet == true)
        {
            Debug.Log("Faster");
            coinDetectorObj.SetActive(true);
            magnetDuration = time;
            PowerUpMagentImage.SetActive(true);

        }

        if (fasterMode)
        {
            Debug.Log("Faster");
            theplayerMotor.speed += 5f;
            fasterDuration = time;
            PowerUpFasterImage.SetActive(true);
        }

        if (slowerMode)
        {
            Debug.Log("Slower");
            theplayerMotor.speed -= 5f;
            slowerDuration = time;
            PowerUpSlowerImage.SetActive(true);
        }
      
        powerupActive = true;                                               // set power up to be true


    }
}
