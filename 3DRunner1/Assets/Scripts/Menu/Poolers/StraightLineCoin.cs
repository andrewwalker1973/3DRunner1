using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightLineCoin : MonoBehaviour
{
    public GameObject pooledObject;                                     // which object to pool
    public int pooledAmount;                                            // How many to pool

    List<GameObject> pooledObjects;                                     // Define a list called pooledObjects
    public bool coinPoolOnline = false;



    void Start()
    {
        pooledObjects = new List<GameObject>();                         // Create a list called pooledObjects of Gameobjects
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(pooledObject);     // create obj of platforms for the nuber specifed in pooledAmount;
            obj.SetActive(false);                                       // turn off by default;
            pooledObjects.Add(obj);                                     // Add gameobject to pooledObjects List
        }


        coinPoolOnline = true;
    }




    public GameObject GetPooledObject()
    {

    
        for (int i = 0; i < pooledObjects.Count; i++)                   // check each pooled object in list
        {
            if (!pooledObjects[i].activeInHierarchy)                    // Check if NOT active in list
            {
                return pooledObjects[i];                                // Send back to game if not active
            }
        }

        // If not platform available in List Create a new one
        GameObject obj = (GameObject)Instantiate(pooledObject);     // create obj of platforms for the nuber specifed in pooledAmount;
        obj.SetActive(false);                                       // turn off by default;
        pooledObjects.Add(obj);                                     // Add gameobject to pooledObjects List
        return obj;                                                 // Return the new game object to the game and it can be used going forward in the list
    }
}
