using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathItemGenerator : MonoBehaviour
{
    public float PowerupSpawnRate = 0.2f; // from 0 to 1
    private string containerString = "Container";
    private string spawnPointString = "Spawn Points"; // string to find our Spawn Points container
    private string powerupSpawnPointString = "Powerup Spawn Points"; // string to find our powerup spawn points container
    private int numberOfCoinsToGenerate = 2;  //was 5
    private int coinDistanceGap = 20;


    private GameObject newLowObstacle;
    public PoolManager thePoolManager;

    // Start is called before the first frame update
    void Start()
    {
        // SpawnCoin();
        // SpawnPowerUp();
        //StartCoroutine(resetSpawnpoints());
    }

    public void OnEnable()
    {

        //   Debug.Log("on enable running");
        SpawnCoin();
  //      SpawnPowerUp();
    }


    private IEnumerator resetSpawnpoints()
    {
        //    Debug.Log("Restet spawn points");
        yield return new WaitForSeconds(10f);
        SpawnCoin();
        StartCoroutine(resetSpawnpoints());
    }


  /*  private void SpawnCoin()
    {

        //  Debug.Log("SpawnCoin COin");
        Transform spawnPoint = PickSpawnPoint(containerString, spawnPointString);
       // Debug.Log("Spawn point" + spawnPoint);
        // We then create a loop of X items that are Y units apart from each other
        for (int i = 0; i < numberOfCoinsToGenerate; i++)
        {
            Vector3 newPosition = spawnPoint.transform.position;
          //  Debug.Log("newPosition " + newPosition);
            newPosition.z += i * numberOfCoinsToGenerate;
            Instantiate(ItemLoaderManager.Instance.Coin, newPosition, Quaternion.identity);
        }
    }
  */
    private void SpawnCoin()
    {

        //  Debug.Log("SpawnCoin COin");
        Transform spawnPoint = PickSpawnPoint(containerString, spawnPointString);
        // Debug.Log("Spawn point" + spawnPoint);
        // We then create a loop of X items that are Y units apart from each other
                   Vector3 newPosition = spawnPoint.transform.position;
            //  Debug.Log("newPosition " + newPosition);
          //  newPosition.z += i * numberOfCoinsToGenerate;
          //  Instantiate(ItemLoaderManager.Instance.Coin, newPosition, Quaternion.identity);
        newLowObstacle = thePoolManager.GetRandomObject();
        Debug.Log("random obj " + newLowObstacle);
        //  float lowObstacleXPosition = Random.Range(-platformWidths[platformSelector] / 2 + 1f, platformWidths[platformSelector] / 2 - 1f); // work out width of platform and raandomize position, but add or remove 1f to prevent it being on the edge
          Vector3 lowObstaclePosition = spawnPoint.transform.position;            // Raise the obstaklce up a bit
        //  newLowObstacle.transform.position = transform.position + lowObstaclePosition;             // Set its position to platform position
      //     newLowObstacle.transform.rotation = transform.rotation;             // Set the rotation to be same as platfomr
        newLowObstacle.SetActive(true);

    }

    private void SpawnPowerUp()
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
