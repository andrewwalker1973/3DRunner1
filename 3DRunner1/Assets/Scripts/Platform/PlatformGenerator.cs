using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public Transform generationPoint;   // where to generate the platform
    private float distanceBetween;      // distance between the platforms
    private float platformWidth;        // the Width of the platform

    //Variables for Randomize the distance between platfroms
    public float distanceBetweenMin;        // Min distance between the platforms
   public float distanceBetweenMax;        // Max distance between the platforms
    private float minHeight;                // Min height from bottom of screen
    public Transform maxHeightPoint;        // Max height of gameobject on screen
    private float maxHeight;
   public float maxHeightChange;           // how much can we increase in height
   private float heightChange;


    private int platformSelector;           // int to number the platforms

    public float[] platformWidths;          // array to manage the widths of the platforms 
    public float[] platformWidthsZone2;          // array to manage the widths of the platforms 

    public ObjectPooler[] theObjectPools;      // Refernce the object pooler script
    //public GameObject[] theObjectPools;
    public ObjectPooler[] theZone2Pools;      // Refernce the object pooler script

    private CoinGenerator theCoinGenerator;      // reference the coin genertion script
    public PoolManager thePoolManager;
    public float randomCoinThreshold;           // Randomize if coin appears



   // public float powerUpHeight;                     // How high to pisiton powerp
    public ObjectPooler[] thePowerUpPools;                // the pool to reference for powerups
    public ObjectPooler[] thecrystalPools;                // the pool to reference for Crystals
    public float powerUpThreshold;                  // what is the threshold for appearing



    public ObjectPooler[] theObstacleObjectPools;      // Refernce the object pooler script
    
    private int obstacleSelector = 0;           // int to number the platforms
   
    private int oldplatformSelector = 4;

    private int powerupSelector = 0;
    private int prelaneToSpawn;
    private int powerUpLocationLane = 0;
    private float powerUpLocation = 0f;

    public float crystalThreshold;
    private int crystalLocationLane = 0;
    private float crystalLocation = 0f;
    private int crystalSelector;
    private float crystalLocation_offset = 15f;
    public GameObject crystal;

    // Enemy
    public float enemyThreshold;
    public ObjectPooler[] theEnemyPools;                // the pool to reference for Enemies
    private int enemyLocationLane = 0;
    private float enemyLocation = 0f;
    private int enemySelector;
    private float enemyLocation_offset = -15f;
    public bool gameManagerPoolOnline = false;
    public GameObject enemy;


    private int enemyMovingSelector;
    public ObjectPooler[] theEnemylaneMovingPools;
    public GameObject enemyFullLane;

    private int runningEnemyMovingSelector;
    public ObjectPooler[] theRunningEnemylaneMovingPools;
    public GameObject runningEnemyFullLane;

    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions
    private PlayerMotor thePlayerMotor;

    public JumpLineCoin theJumpLineCoin;
    public StraightLineCoin theStraightLineCoin;

    public GameObject straightlineCoin;
    public GameObject jumpLineCoin;
    public GameObject singleLaneHighLowObstacle;
    public GameObject powerUp;

    protected float m_CurrentZoneDistance;

    void Start()
    {



        platformWidths = new float[theObjectPools.Length];      // create array with all the platfomr widths for the platforms chosen
        for (int i = 0; i < theObjectPools.Length; i++)
        {
              platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider>().size.z;
           
        }

        //Setup for zone2
        platformWidthsZone2 = new float[theZone2Pools.Length];      // create array with all the platfomr widths for the platforms chosen
        for (int i = 0; i < theZone2Pools.Length; i++)
        {
            platformWidthsZone2[i] = theZone2Pools[i].pooledObject.GetComponent<BoxCollider>().size.z;

        }

       minHeight = transform.position.y;               // set min height to be the height of the current platfomr in y
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();           // find coin genertor script
        theGameManager = FindObjectOfType<GameManager>();
        theJumpLineCoin = FindObjectOfType<JumpLineCoin>();
        theStraightLineCoin = FindObjectOfType<StraightLineCoin>();
        thePlayerMotor = FindObjectOfType<PlayerMotor>();

        gameManagerPoolOnline = true;
        Debug.Log("platform online");
    }


    void Update()
    {
       
            if (transform.position.z < generationPoint.position.z)          // if current point less than gen point on camera, create a platform
            {


              //  distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);     // randomize the distance between platforms

            // Code to change Zone based on distancce raveled.
            if (m_CurrentZoneDistance < 300)
            {

                platformSelector = Random.Range(0, theObjectPools.Length);              // Randomize which platfomr to select
                while (platformSelector == oldplatformSelector)
                {
                    platformSelector = Random.Range(0, theObjectPools.Length);

                }
                oldplatformSelector = platformSelector;

            }
            else
            {
                platformSelector = Random.Range(0, theZone2Pools.Length);              // Randomize which platfomr to select
                while (platformSelector == oldplatformSelector)
                {
                    platformSelector = Random.Range(0, theZone2Pools.Length);

                }
                oldplatformSelector = platformSelector;
            }


           heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);      // randomize the height to be chnaged

                // Code to ensure we dont go to high or too low off screen
                if (heightChange > maxHeight)
                {
                    heightChange = maxHeight;
               }
                else if (heightChange < minHeight)
                {
                    heightChange = minHeight;
                }



            if (m_CurrentZoneDistance < 300)
            {
                transform.position = new Vector3(transform.position.x, heightChange, transform.position.z + (platformWidths[platformSelector] / 2) + distanceBetween);
                GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();
                newPlatform.transform.position = transform.position;                            // set the new platforms position
                newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
                newPlatform.SetActive(true);                                                    // Set it active in the game
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (platformWidths[platformSelector] / 2));
            }
            else
            {
                transform.position = new Vector3(transform.position.x, heightChange, transform.position.z + (platformWidthsZone2[platformSelector] / 2) + distanceBetween);
                GameObject newPlatform = theZone2Pools[platformSelector].GetPooledObject();
                       newPlatform.transform.position = transform.position;                            // set the new platforms position
                        newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
                       newPlatform.SetActive(true);                                                    // Set it active in the game
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (platformWidthsZone2[platformSelector] / 2));
            }





            // Create the actual platform piece in the game world
            //   GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();                        // run the function in the ObjectPool script called GetpooledObject to find the next game object and make it a game object

            //         newPlatform.transform.position = transform.position;                            // set the new platforms position
            //         newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
            //       newPlatform.SetActive(true);                                                    // Set it active in the game







        //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (platformWidths[platformSelector] / 2));


             }

        if (theGameManager.isRunning)
        {
            float scaledSpeed = thePlayerMotor.speed * Time.deltaTime;
        //   m_CurrentZoneDistance += scaledSpeed;
            
        }
        



    }


  /*  private void CoinLeftLane()
    {
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x - 1.7f, transform.position.y + 3.5f, transform.position.z));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x - 1.7f, transform.position.y + 3.5f, transform.position.z + 20f));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x - 1.7f, transform.position.y + 3.5f, transform.position.z - 20f));
    }

    private void CoinMiddleLane()
    {
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z));        // spawn coin in center of platform
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 20f));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z - 20f));
    }

    private void CoinRightLane()
    {
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 1.7f, transform.position.y + 3.5f, transform.position.z));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 1.7f, transform.position.y + 3.5f, transform.position.z + 20f));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 1.7f, transform.position.y + 3.5f, transform.position.z - 20f));
    }
  */
    public void SpawnSingleLaneHighLowObstacles(Vector3 newPosition)
    {
      
    obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);
        /*  while (obstacleSelector == oldObstacleSelector)
          {
              obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

          }
        */
        // oldObstacleSelector = obstacleSelector;
      singleLaneHighLowObstacle = theObstacleObjectPools[obstacleSelector].GetPooledObject();
        singleLaneHighLowObstacle.transform.position = newPosition;             // Set its position to platform position
        singleLaneHighLowObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
        singleLaneHighLowObstacle.SetActive(true);
        singleLaneHighLowObstacle.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SpawnJumpCoins(Vector3 newPosition)
    {
        jumpLineCoin = theJumpLineCoin.GetPooledObject();
        jumpLineCoin.transform.localPosition = Vector3.zero;
        jumpLineCoin.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        jumpLineCoin.transform.GetChild(1).gameObject.transform.localPosition = Vector3.zero;
        jumpLineCoin.transform.GetChild(2).gameObject.transform.localPosition = Vector3.zero;
        jumpLineCoin.transform.position = newPosition;
        jumpLineCoin.transform.GetChild(0).gameObject.transform.position = newPosition + new Vector3(0f, 1f, 0f);
        jumpLineCoin.transform.GetChild(1).gameObject.transform.position = newPosition + new Vector3(0f, 1.5f, 2f);
        jumpLineCoin.transform.GetChild(2).gameObject.transform.position = newPosition + new Vector3(0f, 2f, 4f);
        jumpLineCoin.SetActive(true);
        jumpLineCoin.transform.GetChild(0).gameObject.SetActive(true);
        jumpLineCoin.transform.GetChild(1).gameObject.SetActive(true);
        jumpLineCoin.transform.GetChild(2).gameObject.SetActive(true);
    }

    public void SpawnStraightCoins(Vector3 newPosition)
    {
        straightlineCoin = theStraightLineCoin.GetPooledObject();
        straightlineCoin.transform.localPosition = Vector3.zero;
        straightlineCoin.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        straightlineCoin.transform.GetChild(1).gameObject.transform.localPosition = Vector3.zero;
        straightlineCoin.transform.GetChild(2).gameObject.transform.localPosition = Vector3.zero;
    
        straightlineCoin.transform.position = newPosition ;
        straightlineCoin.transform.GetChild(0).gameObject.transform.position = newPosition + new Vector3(0f, 0f, 0f);
        straightlineCoin.transform.GetChild(1).gameObject.transform.position = newPosition + new Vector3(0f, 0f, -4f);
        straightlineCoin.transform.GetChild(2).gameObject.transform.position = newPosition + new Vector3(0f, 0f, -8f);
        straightlineCoin.SetActive(true);
        straightlineCoin.transform.GetChild(0).gameObject.SetActive(true);
        straightlineCoin.transform.GetChild(1).gameObject.SetActive(true);
        straightlineCoin.transform.GetChild(2).gameObject.SetActive(true);
    }


    public void SpawnPowerUps(Vector3 newPosition)
    {

        // Decide which power Up to present
        powerupSelector = Random.Range(0, thePowerUpPools.Length);              // Get random powerup from Pool
        powerUp = thePowerUpPools[powerupSelector].GetPooledObject();       // make it a game object

        // coin1.transform.localPosition = Vector3.zero;
        // coin1.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        powerUp.transform.position = newPosition;
        powerUp.SetActive(true);
        powerUp.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SpawnCrystals(Vector3 newPosition)
    {

        // Decide which Crystal to present
        crystalSelector = Random.Range(0, thecrystalPools.Length);              // Get random powerup from Pool
        crystal = thecrystalPools[crystalSelector].GetPooledObject();       // make it a game object


        crystal.transform.position = newPosition;
        crystal.SetActive(true);
        crystal.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SpawnEnemys(Vector3 newPosition)
    {

        // Decide which Crystal to present
        enemySelector = Random.Range(0, theEnemyPools.Length);              // Get random powerup from Pool
        enemy = theEnemyPools[enemySelector].GetPooledObject();       // make it a game object


        enemy.transform.position = newPosition;
        enemy.SetActive(true);
        enemy.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SpawnLaneEnemys(Vector3 newPosition)
    {

        // Decide which Enemy lane  to present
        enemyMovingSelector = Random.Range(0, theEnemylaneMovingPools.Length);              // Get random powerup from Pool
        enemyFullLane = theEnemylaneMovingPools[enemyMovingSelector].GetPooledObject();       // make it a game object


        enemyFullLane.transform.position = newPosition;
        enemyFullLane.SetActive(true);
        enemyFullLane.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void SpawnRunningEnemys(Vector3 newPosition)
    {

        // Decide which Enemy lane  to present
        runningEnemyMovingSelector = Random.Range(0, theRunningEnemylaneMovingPools.Length);              // Get random powerup from Pool
        runningEnemyFullLane = theRunningEnemylaneMovingPools[runningEnemyMovingSelector].GetPooledObject();       // make it a game object


        runningEnemyFullLane.transform.position = newPosition;
        runningEnemyFullLane.SetActive(true);
        runningEnemyFullLane.transform.GetChild(0).gameObject.SetActive(true);
    }
}



