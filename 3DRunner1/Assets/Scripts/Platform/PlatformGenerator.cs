using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
   // public GameObject thePlatform;      // The platform game object 
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

  // public GameObject[] thePlatforms;
    private int platformSelector;           // int to number the platforms

    public float[] platformWidths;          // array to manage the widths of the platforms 

    public ObjectPooler[] theObjectPools;      // Refernce the object pooler script

    private CoinGenerator theCoinGenerator;      // reference the coin genertion script
    public float randomCoinThreshold;           // Randomize if coin appears

    public float randomLowObstacleThreshold;        // Random Range for low obstacle pool
    public ObjectPooler LowObstaclePool;            // Define the low Obstacle Pool

    public float randomHighObstacleThreshold;        // Random Range for high obstacle pool
    public ObjectPooler HighObstaclePool;            // Define the high Obstacle Pool
        
    public float powerUpHeight;                     // How high to pisiton powerp
    public ObjectPooler powerUpPool;                // the pool to reference for powerups
    public float powerUpThreshold;                  // what is the threshold for appearing




    void Start()
    {

        
        
        platformWidths = new float[theObjectPools.Length];      // create array with all the platfomr widths for the platforms chosen
        for (int i = 0; i < theObjectPools.Length; i++)
        {
            // platformWidths[i] = theObjectPools[i].pooledObject.transform.localScale.x;
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider>().size.x;
        }

        minHeight = transform.position.y;               // set min height to be the height of the current platfomr in y
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();           // find coin genertor script
    }


    void Update()
    {
        if (transform.position.x < generationPoint.position.x)          // if current point less than gen point on camera, create a platform
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

            // Random generate a poweruP
            if (Random.Range(0f, 100f) < powerUpThreshold)
            {
                GameObject newPowerup = powerUpPool.GetPooledObject();                      // find powerup in pool
                newPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight / 2, powerUpHeight), 0f);       // add between platforms min 1/2 distance max distance
                newPowerup.SetActive(true);                                                 // set powerup active

            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2) + distanceBetween, heightChange, transform.position.z);  // Determine the position to spawn new platform



            // Create the actual platform piece in the game world
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();                        // run the function in the ObjectPool script called GetpooledObject to find the next game object and make it a game object
            newPlatform.transform.position = transform.position;                            // set the new platforms position
            newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
            newPlatform.SetActive(true);                                                    // Set it active in the game

     /*       if (Random.Range(0f, 100f) < randomCoinThreshold)        // if random value below threshold spawn a coin set
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));        // spawn coin in center of platform
            }
     */

     /*       if (Random.Range(0f, 100f) < randomLowObstacleThreshold)        // if random value below threshold spawn a low Obstacke
            {
                GameObject newLowObstacle = LowObstaclePool.GetPooledObject();      // get Low obstacle from pool
                float lowObstacleXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge

                Vector3 lowObstaclePosition = new Vector3(lowObstacleXPosition, 1f, 0f);            // Raise the obstaklce up a bit
                newLowObstacle.transform.position = transform.position + lowObstaclePosition;             // Set its position to platform position
                newLowObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                newLowObstacle.SetActive(true);                                     // set it visable
            }
     */

            if (Random.Range(0f, 100f) < randomHighObstacleThreshold)        // if random value below threshold spawn a high Obstacke
            {
                GameObject newHighObstacle = HighObstaclePool.GetPooledObject();      // get High obstacle from pool
                float highObstacleXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge

                Vector3 highObstaclePosition = new Vector3(highObstacleXPosition, 2.5f, 0f);            // Raise the obstaklce up a bit
                newHighObstacle.transform.position = transform.position + highObstaclePosition;             // Set its position to platform position
                newHighObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                newHighObstacle.SetActive(true);                                     // set it visable
            }
            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);  // Determine the position to spawn new platform


        }
    }
}

 
