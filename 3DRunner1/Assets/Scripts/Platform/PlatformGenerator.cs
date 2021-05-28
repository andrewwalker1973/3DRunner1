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

    public ObjectPooler[] theObjectPools;      // Refernce the object pooler script

    private CoinGenerator theCoinGenerator;      // reference the coin genertion script
    public PoolManager thePoolManager;
    public float randomCoinThreshold;           // Randomize if coin appears



    public float powerUpHeight;                     // How high to pisiton powerp
    public ObjectPooler[] thePowerUpPools;                // the pool to reference for powerups
    public ObjectPooler[] thecrystalPools;                // the pool to reference for Crystals
    public float powerUpThreshold;                  // what is the threshold for appearing



    public ObjectPooler[] theObstacleObjectPools;      // Refernce the object pooler script
    private int obstacleSelector = 0;           // int to number the platforms
    private int oldObstacleSelector = 4;

    private int powerupSelector = 0;
    private int prelaneToSpawn;
    private int powerUpLocationLane = 0;
    private float powerUpLocation = 0f;

    public float crystalThreshold;
    private int crystalLocationLane = 0;
    private float crystalLocation = 0f;
    private int crystalSelector;
    private float crystalLocation_offset = 15f;

    // Enemy
    public float enemyThreshold;
    public ObjectPooler[] theEnemyPools;                // the pool to reference for Enemies
    private int enemyLocationLane = 0;
    private float enemyLocation = 0f;
    private int enemySelector;
    private float enemyLocation_offset = -15f;


    void Start()
    {



        platformWidths = new float[theObjectPools.Length];      // create array with all the platfomr widths for the platforms chosen
        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider>().size.z;
        }

        minHeight = transform.position.y;               // set min height to be the height of the current platfomr in y
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();           // find coin genertor script
    }


    void Update()
    {
        if (transform.position.z < generationPoint.position.z)          // if current point less than gen point on camera, create a platform
        {


            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);     // randomize the distance between platforms

            platformSelector = Random.Range(0, theObjectPools.Length);              // Randomize which platfomr to select


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


            if (Random.Range(0f, 100f) < powerUpThreshold)
            {
                powerupSelector = Random.Range(0, thePowerUpPools.Length);              // Get random powerup from Pool
                GameObject Powerup1 = thePowerUpPools[powerupSelector].GetPooledObject();       // make it a game object
                powerUpLocationLane = Random.Range(0, 3);                                       // randomize which lane to add it in
                switch (powerUpLocationLane)
                {
                    case 0:
                        powerUpLocation = -1.5f;                                    // left lane
                        break;
                    case 1:
                        powerUpLocation = 0f;                                       // mid lane
                        break;
                    case 2:
                        powerUpLocation = 1.5f;                                     // right lane
                        break;
                    default:
                        powerUpLocation = 0f;                                       // default to middle lane
                        break;
                }

                        Powerup1.transform.position = transform.position + new Vector3(powerUpLocation, 1.0f);      // set powerup intrack - track position + lane + height;
                Powerup1.SetActive(true);                                                                              // enabke the powerup
            }

            // Crystal Picup
            if (Random.Range(0f, 100f) < crystalThreshold)
            {
                crystalSelector = Random.Range(0, thecrystalPools.Length);              // Get random powerup from Pool
                GameObject crystal1 = thecrystalPools[crystalSelector].GetPooledObject();       // make it a game object
                crystalLocationLane = Random.Range(0, 3);                                       // randomize which lane to add it in
                switch (crystalLocationLane)
                {
                    case 0:
                        crystalLocation = -1.5f;                                    // left lane
                        break;
                    case 1:
                        crystalLocation = 0f;                                       // mid lane
                        break;
                    case 2:
                        crystalLocation = 1.5f;                                     // right lane
                        break;
                    default:
                        crystalLocation = 0f;                                       // default to middle lane
                        break;
                }

                crystal1.transform.position = transform.position + new Vector3(crystalLocation, 2.5f, crystalLocation_offset);      // set powerup intrack - track position + lane + height;
                crystal1.SetActive(true);                                                                              // enabke the powerup
                crystal1.transform.GetChild(0).gameObject.SetActive(true);
            }


            // Enemy selector
            // Crystal Picup
            if (Random.Range(0f, 100f) < enemyThreshold)
            {
                enemySelector = Random.Range(0, theEnemyPools.Length);              // Get random powerup from Pool
                GameObject enemy1 = theEnemyPools[enemySelector].GetPooledObject();       // make it a game object
                enemyLocationLane = Random.Range(0, 3);                                       // randomize which lane to add it in
                switch (enemyLocationLane)
                {
                    case 0:
                        enemyLocation = -1.5f;                                    // left lane
                        break;
                    case 1:
                        enemyLocation = 0f;                                       // mid lane
                        break;
                    case 2:
                        enemyLocation = 1.5f;                                     // right lane
                        break;
                    default:
                        enemyLocation = 0f;                                       // default to middle lane
                        break;
                }

                enemy1.transform.position = transform.position + new Vector3(enemyLocation, 2.5f, enemyLocation_offset);      // set powerup intrack - track position + lane + height;
                enemy1.SetActive(true);                                                                              // enabke the powerup
                enemy1.transform.GetChild(0).gameObject.SetActive(true);
            }



            transform.position = new Vector3(transform.position.x, heightChange, transform.position.z + (platformWidths[platformSelector] / 2) + distanceBetween);





            // Create the actual platform piece in the game world
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();                        // run the function in the ObjectPool script called GetpooledObject to find the next game object and make it a game object
            newPlatform.transform.position = transform.position;                            // set the new platforms position
            newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
            newPlatform.SetActive(true);                                                    // Set it active in the game



                   if (Random.Range(0f, 100f) < randomCoinThreshold)        // if random value below threshold spawn a coin set
                   {

                        
                        int laneToSpawn = Random.Range(0, 7);
                        while (laneToSpawn == prelaneToSpawn)
                        {
                            laneToSpawn = Random.Range(0, 7);
                        }
                prelaneToSpawn = laneToSpawn;
                        switch (laneToSpawn)
                        {
                            case 0:
                                CoinLeftLane();
                                break;
                            case 1:
                                CoinMiddleLane();
                                break;
                            case 2:
                                CoinRightLane();
                                break;
                            case 3:
                                CoinLeftLane();
                                CoinMiddleLane();
                                break;
                            case 4:
                                CoinMiddleLane();
                                CoinRightLane();
                                break;
                            case 5:
                                CoinLeftLane();
                                CoinRightLane();
                                break;
                            case 6:
                                CoinLeftLane();
                                CoinRightLane();
                                CoinMiddleLane();
                                break;

                            default:
                                    CoinLeftLane();
                                    CoinRightLane();
                                    CoinMiddleLane();
                                    break;

                        
                        }



                   }


            
                        // OBSTACLE 1
                        obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);              // Randomize which Obstacle to select
                                                                                                        // While loop to ensure the obstancles are not repeated
                        while (obstacleSelector == oldObstacleSelector)
                        {
                            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

                        }
                            oldObstacleSelector = obstacleSelector;         // Remember the old obstacle number to check for later

                        GameObject Obstacle1 = theObstacleObjectPools[obstacleSelector].GetPooledObject();      // Select Obstacle1 from the Pool
                        Vector3 obstacle1Location = new Vector3(0f, 2.5f, 0f);                                             // define where on the platform to place it, location 0 on Z
                        Obstacle1.transform.position = transform.position + obstacle1Location;                   // Set its position to platform position
                        Obstacle1.transform.rotation = transform.rotation;                                      // Set the rotation to be same as platfomr
                        Obstacle1.SetActive(true);                                                              // Set the Obstacle Active


                        // OBSTACLE 2
                        obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);
                        while (obstacleSelector == oldObstacleSelector)
                        {
                            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

                        }
                        oldObstacleSelector = obstacleSelector;
                        GameObject Obstacle2 = theObstacleObjectPools[obstacleSelector].GetPooledObject(); 
                        Vector3 obstacle2Location = new Vector3(0f, 2.5f, 20f);
                        Obstacle2.transform.position = transform.position + obstacle2Location;             // Set its position to platform position
                        Obstacle2.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                        Obstacle2.SetActive(true);


                        // OBSTACLE 3
                        obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);
                        while (obstacleSelector == oldObstacleSelector)
                        {
                            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

                        }
                        oldObstacleSelector = obstacleSelector;
                        GameObject Obstacle3 = theObstacleObjectPools[obstacleSelector].GetPooledObject();
                        Vector3 obstacle3Location = new Vector3(0f, 2.5f, -20f);
                        Obstacle3.transform.position = transform.position + obstacle3Location;             // Set its position to platform position
                        Obstacle3.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                        Obstacle3.SetActive(true);


                        // OBSTACLE 4
                        obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);
                        while (obstacleSelector == oldObstacleSelector)
                        {
                            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

                        }
                        oldObstacleSelector = obstacleSelector;
                        GameObject Obstacle4 = theObstacleObjectPools[obstacleSelector].GetPooledObject();
                        Vector3 obstacle4Location = new Vector3(0f, 2.5f, -35f);
                        Obstacle4.transform.position = transform.position + obstacle4Location;             // Set its position to platform position
                        Obstacle4.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                        Obstacle4.SetActive(true);
            



            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (platformWidths[platformSelector] / 2));


        }
    }

    
    private void CoinLeftLane()
    {
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x -1.7f, transform.position.y + 3.5f, transform.position.z));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x -1.7f, transform.position.y + 3.5f, transform.position.z + 20f));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x -1.7f, transform.position.y + 3.5f, transform.position.z - 20f));
    }

    private void CoinMiddleLane()
    {
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z));        // spawn coin in center of platform
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z + 20f));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 3.5f, transform.position.z - 20f));
    }

    private void CoinRightLane()
    {
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x +1.7f, transform.position.y + 3.5f, transform.position.z));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x +1.7f, transform.position.y + 3.5f, transform.position.z + 20f));
        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x +1.7f, transform.position.y + 3.5f, transform.position.z - 20f));
    }
}


