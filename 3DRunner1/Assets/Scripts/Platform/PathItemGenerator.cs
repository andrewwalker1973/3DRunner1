using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathItemGenerator : MonoBehaviour
{
    public float PowerupSpawnRate = 0.2f; // from 0 to 1
    private string containerString = "Container";
    private string straightCoinSpawnPointString = "Straight Coin Spawn Points"; // string to find our Spawn Points container for straight line coins
    private string jumpCoinSpawnPointString = "Jump Coin Spawn Points"; // string to find our Spawn Points container for straight line coins
    private string singleLaneHighLowSpawnPointString = "Single Lane low_high Spawn Points"; // string to find our Spawn Points container for straight line coins
    private string powerupSpawnPointString = "Powerup Spawn Points"; // string to find our powerup spawn points container
    private int numberOfCoinsToGenerate = 2;  //was 5
    private int coinDistanceGap = 20;

    private CoinGenerator theCoinGenerator;      // reference the coin genertion script
   // public ObjectPooler[] thecrystalPools;                // the pool to reference for Crystals
    private int crystalSelector;
    public  PlatformGenerator thePlatformGenerator;
    public StraightLineCoin theStraightLineCoin;
    //public ObstaclePool[] theObstacleObjectPools;      // Refernce the object pooler script

    public JumpLineCoin theJumpLineCoin;

    private GameObject coin1;
    private GameObject coin2;

    private GameObject coinObject;
    private int obstacleSelector = 0;           // int to number the platforms
                                                //  public GameObject thecrystalPools;


    /*  private void OnEnable()
      {
         theStraightLineCoin = FindObjectOfType<StraightLineCoin>();
      }
    */
    // Start is called before the first frame update
    void Start()
    {
        // SpawnCoin();
        // SpawnPowerUp();
        //StartCoroutine(resetSpawnpoints());
          thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        // theCrystalPool = FindObjectOfType<ObjectPooler>();
      theStraightLineCoin = FindObjectOfType<StraightLineCoin>();
        theJumpLineCoin = FindObjectOfType<JumpLineCoin>();
       // theObstacleObjectPools = FindObjectsOfType<ObstaclePool>();





    }

    public void OnEnable()
    {
       theStraightLineCoin = FindObjectOfType<StraightLineCoin>();
       theJumpLineCoin = FindObjectOfType<JumpLineCoin>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        //   Debug.Log("on enable running");
        SpawnStraightCoin();
        SpawnJumpCoin();
        SpawnSingleLaneHighLowObstacle();
        //      SpawnPowerUp();
    }


    private IEnumerator resetSpawnpoints()
    {
        //    Debug.Log("Restet spawn points");
        yield return new WaitForSeconds(10f);
        SpawnStraightCoin();
        SpawnJumpCoin();
        SpawnSingleLaneHighLowObstacle();
        StartCoroutine(resetSpawnpoints());
    }


    private void SpawnStraightCoin()
    {     
        Transform spawnPoint = PickSpawnPoint(containerString, straightCoinSpawnPointString);
        Vector3 newPosition = spawnPoint.transform.position;      
        if (theStraightLineCoin.coinPoolOnline != false)
        {
            /*coin1 = theStraightLineCoin.GetPooledObject();
            // coin1.transform.localPosition = Vector3.zero;
            // coin1.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
            coin1.transform.position = newPosition + new Vector3(0f, 0f, 0f);
            coin1.SetActive(true);
            coin1.transform.GetChild(0).gameObject.SetActive(true);
            coin1.transform.GetChild(1).gameObject.SetActive(true);
            coin1.transform.GetChild(2).gameObject.SetActive(true);
            */
            thePlatformGenerator.SpawnStraightCoins(newPosition);
        }
       

        //  }
    }

    private void SpawnJumpCoin()
    {
        Transform spawnPoint = PickSpawnPoint(containerString, jumpCoinSpawnPointString);
        Vector3 newPosition = spawnPoint.transform.position;
        if (theStraightLineCoin.coinPoolOnline != false)
        {
            thePlatformGenerator.SpawnJumpCoins(newPosition);
        }


        //  }
    }

    private void SpawnSingleLaneHighLowObstacle()
    {
        Transform spawnPoint = PickSpawnPoint(containerString, singleLaneHighLowSpawnPointString);
        Vector3 newPosition = spawnPoint.transform.position;
        if (theStraightLineCoin.coinPoolOnline != false)
        {
            thePlatformGenerator.SpawnSingleLaneHighLowObstacles(newPosition);
        }
    }



    /*  private void SpawnPowerUp()
      {
          // We randomly generate a number and divide it by 100. If it is lower than the spawn rate chance we set,
          // then we create the powerup.
          Debug.Log("Start Spawn");
          bool generatePowerUp = Random.Range(0, 100) / 100f < PowerupSpawnRate;
          if (generatePowerUp)
          {
              Transform spawnPoint = PickSpawnPoint(containerString, powerupSpawnPointString);
              Vector3 newPosition = spawnPoint.transform.position;

              // Get our Power-ups and randomly pick one of them to show
              //    GameObject[] powerUps = ItemLoaderManager.Instance.PowerUps;
              //     int powerUpIndex = Random.Range(0, powerUps.Length);
              //      Instantiate(powerUps[powerUpIndex], newPosition, Quaternion.identity);
              //   Debug.Log("Creating power up an spawn point");
              Debug.Log("Power up spawn");
              newLowObstacle = thePoolManager.GetRandomObject();
            //  float lowObstacleXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge
            //  Vector3 lowObstaclePosition = new Vector3(0f, 2.5f, lowObstacleXPosition);            // Raise the obstaklce up a bit
              newLowObstacle.transform.position = transform.position ;             // Set its position to platform position
              newLowObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
              newLowObstacle.SetActive(true);
          }
      }

      */
    private Transform PickSpawnPoint(string spawnPointContainerString, string spawnPointString)
    {
        // We get container game object and then  the spawnPointContainer and get it's children which
        // are all spawn points to create a spawn point. The benefit of this is so that we don't have
        // to manually attach any game objects to the script, however we're more likely to have our code break
        // if we were to rename or restructure the spawn points
        //   Debug.Log("Picking sapawn point");
        Transform container = transform.Find(spawnPointContainerString);
        //   Debug.Log("find string" + container.Find(spawnPointString));
        Transform spawnPointContainer = container.Find(spawnPointString);
        if (spawnPointContainer is null)
        {
            Debug.Log("nulll;; ");
        }


        // Initially I first used GetComponentsInChildren, however it turns out that the function is
        // poorly named and for some reason that also includes the parent component, ie the spawnPointContainer.
        Transform[] spawnPoints = new Transform[spawnPointContainer.childCount];


        for (int i = 0; i < spawnPointContainer.childCount; i++)
        {
            //       Debug.Log("*********************spawnPointContainer.childCount " + spawnPointContainer.childCount);
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


}
