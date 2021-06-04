using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpLineCoin : MonoBehaviour
{
    public GameObject jpooledObject;                                     // which object to pool
    public int pooledAmount;                                            // How many to pool

    List<GameObject> jpooledObjects;                                     // Define a list called pooledObjects
    public bool jumpcoinPoolOnline = false;



    void Start()
    {
        jpooledObjects = new List<GameObject>();                         // Create a list called pooledObjects of Gameobjects
        for (int i = 0; i < pooledAmount; i++)
        {
            GameObject obj = (GameObject)Instantiate(jpooledObject);     // create obj of platforms for the nuber specifed in pooledAmount;
            obj.SetActive(false);                                       // turn off by default;
            jpooledObjects.Add(obj);                                     // Add gameobject to pooledObjects List
        }


        jumpcoinPoolOnline = true;
    }




    public GameObject GetPooledObject()
    {


        for (int i = 0; i < jpooledObjects.Count; i++)                   // check each pooled object in list
        {
            if (!jpooledObjects[i].activeInHierarchy)                    // Check if NOT active in list
            {
                return jpooledObjects[i];                                // Send back to game if not active
            }
        }

        // If not platform available in List Create a new one
        GameObject obj = (GameObject)Instantiate(jpooledObject);     // create obj of platforms for the nuber specifed in pooledAmount;
        obj.SetActive(false);                                       // turn off by default;
        jpooledObjects.Add(obj);                                     // Add gameobject to pooledObjects List
        return obj;                                                 // Return the new game object to the game and it can be used going forward in the list
    }
}
