using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour
{

    public ObjectPooler coinPool;               // Pool of coins
    public float distanceBetweenCoins;

    public void SpawnCoins (Vector3 startPosition)
    {
        GameObject coin1 = coinPool.GetPooledObject();          // add coin at start position
        coin1.transform.position = startPosition;
        coin1.SetActive(true);

        GameObject coin2 = coinPool.GetPooledObject();
        coin2.transform.position = new Vector3(startPosition.x - distanceBetweenCoins, startPosition.y, startPosition.z);       // add coin to left of start position
        coin2.SetActive(true);

        GameObject coin3 = coinPool.GetPooledObject();
        coin3.transform.position = new Vector3(startPosition.x + distanceBetweenCoins, startPosition.y, startPosition.z);       // add coin to right of tart positon
        coin3.SetActive(true);
    }


}
