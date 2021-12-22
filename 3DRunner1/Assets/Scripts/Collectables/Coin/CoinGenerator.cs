using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{

    public ObjectPooler coinPool;               // Pool of coins
   // public PoolManager thePoolManager;
    public float distanceBetweenCoins;

    public void SpawnCoins(Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPooledObject();          // add coin at start position
        coin1.transform.localPosition = Vector3.zero;
        coin1.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        coin1.transform.position = startPosition;
        coin1.SetActive(true);
        coin1.transform.GetChild(0).gameObject.SetActive(true);

       GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.localPosition = Vector3.zero;
        coin2.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        coin2.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z - distanceBetweenCoins);       // add coin to left of start position
       coin2.SetActive(true);
        coin2.transform.GetChild(0).gameObject.SetActive(true); 

    /*    GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.localPosition = Vector3.zero;
        coin3.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        coin3.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceBetweenCoins);       // add coin to right of tart positon
        coin3.SetActive(true);
        coin3.transform.GetChild(0).gameObject.SetActive(true);

        GameObject coin4 = coinPool.GetPooledObject();
        coin4.transform.localPosition = Vector3.zero;
        coin4.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        coin4.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceBetweenCoins + distanceBetweenCoins);       // add coin to right of tart positon
        coin4.SetActive(true);
        coin4.transform.GetChild(0).gameObject.SetActive(true);

        GameObject coin5 = coinPool.GetPooledObject();
        coin5.transform.localPosition = Vector3.zero;
        coin5.transform.GetChild(0).gameObject.transform.localPosition = Vector3.zero;
        coin5.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z - distanceBetweenCoins - distanceBetweenCoins);       // add coin to right of tart positon
               coin5.SetActive(true);
        coin5.transform.GetChild(0).gameObject.SetActive(true);
    */

    }

    /*   public void SpawnObstacle(Vector3 startPosition)
       {

           GameObject Obstacle1 = thePoolManager.GetRandomObject();
           Obstacle1.transform.position = startPosition;
           Obstacle1.SetActive(true);

           GameObject Obstacle2 = thePoolManager.GetRandomObject();
           Obstacle2.transform.position = new Vector3(startPosition.x , startPosition.y, startPosition.z - distanceBetweenCoins);       // add coin to left of start position
           Obstacle2.SetActive(true);

           GameObject Obstacle3 = thePoolManager.GetRandomObject();
           Obstacle3.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceBetweenCoins);
           Obstacle3.SetActive(true);

          /* GameObject Obstacle4 = thePoolManager.GetRandomObject();
           Obstacle4.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z + distanceBetweenCoins *2 );
           Obstacle4.SetActive(true);

           GameObject Obstacle5 = thePoolManager.GetRandomObject();
           Obstacle5.transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z - distanceBetweenCoins - distanceBetweenCoins);
           Obstacle5.SetActive(true);*/
    // }

}
