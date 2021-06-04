using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathItemGeneratorHighLowObstacle : MonoBehaviour
{

    private string containerString = "Container";
    private string singleLaneHighLowSpawnPointString = "Single Lane low_high Spawn Points"; // string to find our Spawn Points container for straight line coins
    public PlatformGenerator thePlatformGenerator;
    public StraightLineCoin theStraightLineCoin;


    // Start is called before the first frame update
    void Start()
    {
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theStraightLineCoin = FindObjectOfType<StraightLineCoin>();

    }

    public void OnEnable()
    {

        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theStraightLineCoin = FindObjectOfType<StraightLineCoin>();

        SpawnSingleLaneHighLowObstacle();

    }

    private IEnumerator resetSpawnpoints()
    {
        //    Debug.Log("Restet spawn points");
        yield return new WaitForSeconds(10f);

        SpawnSingleLaneHighLowObstacle();
        StartCoroutine(resetSpawnpoints());
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
