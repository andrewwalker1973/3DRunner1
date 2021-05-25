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
    public PoolManager thePoolManager;
    public float randomCoinThreshold;           // Randomize if coin appears

    //public float randomLowObstacleThreshold;        // Random Range for low obstacle pool
    // public ObstaclePool LowObstaclePool;            // Define the low Obstacle Pool

    // public float randomHighObstacleThreshold;        // Random Range for high obstacle pool
    // public ObjectPooler HighObstaclePool;            // Define the high Obstacle Pool

    public float powerUpHeight;                     // How high to pisiton powerp
    public ObjectPooler powerUpPool;                // the pool to reference for powerups
    public float powerUpThreshold;                  // what is the threshold for appearing

    private GameObject newLowObstacle;
    private GameObject newLowObstacle1;
    private GameObject newLowObstacle2;

    public ObjectPooler[] theObstacleObjectPools;      // Refernce the object pooler script
    private int obstacleSelector = 0;           // int to number the platforms
    private int oldObstacleSelector = 4;

    /* private string containerString = "Container";
     private string spawnPointString = "Spawn Points"; // string to find our Spawn Points container
     private string powerupSpawnPointString = "Powerup Spawn Points"; // string to find our powerup spawn points container
    */


    void Start()
    {



        platformWidths = new float[theObjectPools.Length];      // create array with all the platfomr widths for the platforms chosen
        for (int i = 0; i < theObjectPools.Length; i++)
        {
            // platformWidths[i] = theObjectPools[i].pooledObject.transform.localScale.x;
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

            // Random generate a poweruP
            /*       if (Random.Range(0f, 100f) < powerUpThreshold)
                   {
                       GameObject newPowerup = powerUpPool.GetPooledObject();                      // find powerup in pool
                       newPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2f, Random.Range(powerUpHeight / 2, powerUpHeight), 0f);       // add between platforms min 1/2 distance max distance
                       newPowerup.SetActive(true);                                                 // set powerup active

                   }

       */
            // transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2)/* + distanceBetween, heightChange, transform.position.x*/);  // Determine the position to spawn new platform
            transform.position = new Vector3(transform.position.x, heightChange, transform.position.z + (platformWidths[platformSelector] / 2) + distanceBetween);





            // Create the actual platform piece in the game world
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();                        // run the function in the ObjectPool script called GetpooledObject to find the next game object and make it a game object
            newPlatform.transform.position = transform.position;                            // set the new platforms position
            newPlatform.transform.rotation = transform.rotation;                            // Set the new platforms rotation
            newPlatform.SetActive(true);                                                    // Set it active in the game



                   if (Random.Range(0f, 100f) < randomCoinThreshold)        // if random value below threshold spawn a coin set
                   {
                        int laneToSpawn = Random.Range(0, 3);
                
                        switch (laneToSpawn)
                        {
                            case 0:
                        
                        theCoinGenerator.SpawnCoins(new Vector3(transform.position.x - 1.5f, transform.position.y + 1f, transform.position.z));
                                break;
                            case 1:
                                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z));        // spawn coin in center of platform
                                break;
                            case 2:
                                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 1.5f, transform.position.y + 1f, transform.position.z));
                                break;
                            default:
                                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 1.5f, transform.position.y + 1f, transform.position.z));
                                break;
                }



                   }
            
            /*   if (Random.Range(0f, 100f) < randomLowObstacleThreshold)        // if random value below threshold spawn a coin set
                {
                    theCoinGenerator.SpawnObstacle(new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z));        // spawn coin in center of platform
                }
              */

            // if (Random.Range(0f, 100f) < randomLowObstacleThreshold)        // if random value below threshold spawn a low Obstacke
            //{

     
            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);              // Randomize which platfomr to select
            while (obstacleSelector == oldObstacleSelector)
            {
                obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

            }
            oldObstacleSelector = obstacleSelector;
            //  Debug.Log("obstacleSelector" + obstacleSelector);
            GameObject newLowObstacle = theObstacleObjectPools[obstacleSelector].GetPooledObject();


  
            float lowObstacleXPosition = platformWidths[platformSelector] / 2;

            Vector3 height = new Vector3(0f, 2.5f, 0f);
            Vector3 lowObstaclePosition = new Vector3(0f, 2.5f, lowObstacleXPosition);            // Raise the obstaklce up a bit
            newLowObstacle.transform.position = transform.position + height;             // Set its position to platform position
            newLowObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
            newLowObstacle.SetActive(true);


            //AW
            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);
            while (obstacleSelector == oldObstacleSelector)
            {
                obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

            }
            oldObstacleSelector = obstacleSelector;
            // Debug.Log("obstacleSelector" + obstacleSelector);
            // newLowObstacle1 = thePoolManager.GetRandomObject();
            GameObject newLowObstacle1 = theObstacleObjectPools[obstacleSelector].GetPooledObject();
            //   Debug.Log("lowObstacleXPosition" + lowObstacleXPosition);  
            float lowObstacleXPosition1 = platformWidths[platformSelector] / 2;
            //  Debug.Log("lowObstacleXPosition1" + lowObstacleXPosition1);
            Vector3 height1 = new Vector3(0f, 2.5f, 20f);
            Vector3 lowObstaclePosition1 = new Vector3(0f, 2.5f, lowObstacleXPosition1);            // Raise the obstaklce up a bit
            newLowObstacle1.transform.position = transform.position + height1;             // Set its position to platform position
            newLowObstacle1.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
            newLowObstacle1.SetActive(true);

            obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);
            while (obstacleSelector == oldObstacleSelector)
            {
                obstacleSelector = Random.Range(0, theObstacleObjectPools.Length);

            }
            oldObstacleSelector = obstacleSelector;
            GameObject newLowObstacle2 = theObstacleObjectPools[obstacleSelector].GetPooledObject();
            //   Debug.Log("lowObstacleXPosition" + lowObstacleXPosition);  
            float lowObstacleXPosition2 = platformWidths[platformSelector] / 2;
            //    Debug.Log("lowObstacleXPosition1" + lowObstacleXPosition1);
            Vector3 height2 = new Vector3(0f, 2.5f, -20f);
            Vector3 lowObstaclePosition2 = new Vector3(0f, 2.5f, lowObstacleXPosition2);            // Raise the obstaklce up a bit
            newLowObstacle2.transform.position = transform.position + height2;             // Set its position to platform position
            newLowObstacle2.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
            newLowObstacle2.SetActive(true);



            // set it visable
            // }


            /*  if (Random.Range(0f, 100f) < randomLowObstacleThreshold)        // if random value below threshold spawn a low Obstacke
              {
                  newLowObstacle1 = thePoolManager.GetRandomObject();
                  float lowObstacleXPosition1 = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge
                  Vector3 lowObstaclePosition1 = new Vector3(0f, 2.5f, lowObstacleXPosition1 + 15f);            // Raise the obstaklce up a bit
                  newLowObstacle1.transform.position = transform.position + lowObstaclePosition1;             // Set its position to platform position
                  newLowObstacle1.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                  newLowObstacle1.SetActive(true);                                     // set it visable
              }

              if (Random.Range(0f, 100f) < randomLowObstacleThreshold)        // if random value below threshold spawn a low Obstacke
              {
                  newLowObstacle2 = thePoolManager.GetRandomObject();
                  float lowObstacleXPosition2 = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge
                  Vector3 lowObstaclePosition2 = new Vector3(0f, 2.5f, lowObstacleXPosition2 - 15f);            // Raise the obstaklce up a bit
                  newLowObstacle2.transform.position = transform.position + lowObstaclePosition2;             // Set its position to platform position
                  newLowObstacle2.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                  newLowObstacle2.SetActive(true);                                     // set it visable
              }
            */

            /*        if (Random.Range(0f, 100f) < randomHighObstacleThreshold)        // if random value below threshold spawn a high Obstacke
                    {
                        GameObject newHighObstacle = HighObstaclePool.GetPooledObject();      // get High obstacle from pool
                        float highObstacleXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge

                        Vector3 highObstaclePosition = new Vector3(highObstacleXPosition, 2.5f, 0f);            // Raise the obstaklce up a bit
                        newHighObstacle.transform.position = transform.position + highObstaclePosition;             // Set its position to platform position
                        newHighObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                        newHighObstacle.SetActive(true);                                     // set it visable
                    }
            */

            /*  if (Random.Range(0f, 100f) < randomLowObstacleThreshold)
              {
                  Transform spawnPoint = PickSpawnPoint(containerString, spawnPointString);
                  Vector3 newPosition = spawnPoint.transform.position;
                  newLowObstacle1 = thePoolManager.GetRandomObject();
                  float lowObstacleXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge
                  Vector3 lowObstaclePosition = new Vector3(0f, 2.5f, lowObstacleXPosition);            // Raise the obstaklce up a bit
                  newLowObstacle.transform.position = transform.position + lowObstaclePosition;             // Set its position to platform position
                  newLowObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
                  newLowObstacle.SetActive(true);
              }
            */

            //   transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);  // Determine the position to spawn new platform
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (platformWidths[platformSelector] / 2));


        }
    }

    /* private Transform PickSpawnPoint(string spawnPointContainerString, string spawnPointString)
      {
          // We get container game object and then  the spawnPointContainer and get it's children which
          // are all spawn points to create a spawn point. The benefit of this is so that we don't have
          // to manually attach any game objects to the script, however we're more likely to have our code break
          // if we were to rename or restructure the spawn points
             Debug.Log("Picking sapawn point");
          Transform container = transform.Find(spawnPointContainerString);
             Debug.Log("find string" + container.Find(spawnPointString));
          Transform spawnPointContainer = container.Find(spawnPointString);


          // Initially I first used GetComponentsInChildren, however it turns out that the function is
          // poorly named and for some reason that also includes the parent component, ie the spawnPointContainer.
          Transform[] spawnPoints = new Transform[spawnPointContainer.childCount];


          for (int i = 0; i < spawnPointContainer.childCount; i++)
          {
                     Debug.Log("*********************spawnPointContainer.childCount " + spawnPointContainer.childCount);
              //    Debug.Log("I value" + i);
              spawnPoints[i] = spawnPointContainer.GetChild(i);
          }


          // If we don't have any spawn points the rest of our code will crash, let's just leave a message
          // and quietly return
          if (spawnPoints.Length == 0)
          {
              Debug.Log("We have a path has no spawn points!");
          }


          // We randomly pick one of our spawn points to use
          int index = Random.Range(0, spawnPoints.Length);
          return spawnPoints[index];
      }
    */


}


