using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePool : MonoBehaviour
{
    public GameObject[] columns;                                     // which object to pool
    public int pooledAmount = 5;                                            // How many to pool
    private int randomInt;
    //List<GameObject> pooledObjects;                                     // Define a list called pooledObjects
    public GameObject[] segmentArr;
    public GameObject columnprefab;



    void Start()
    {
        /* pooledObjects = new List<GameObject>();                         // Create a list called pooledObjects of Gameobjects
         for (int i = 0; i < pooledAmount; i++)
         {
             GameObject obj = (GameObject)Instantiate(pooledObjects[i]);     // create obj of platforms for the nuber specifed in pooledAmount;
             obj.SetActive(false);                                       // turn off by default;
             pooledObjects.Add(obj);                                     // Add gameobject to pooledObjects List
         }
        */


        columns = new GameObject[pooledAmount];
        for (int i = 0; i < pooledAmount; i++)
        {

            columns[i] = (GameObject)Instantiate(columnprefab);



        }
    }



  /*  public GameObject GetPooledObject()
    {
        for (int i = 0; i < columns.Length; i++)                   // check each pooled object in list
        {
            if (!columns[i].activeInHierarchy)                    // Check if NOT active in list
            {
                return columns[i];                                // Send back to game if not active
            }
        }
        */

        // If not platform available in List Create a new one
        //   GameObject obj = (GameObject)Instantiate(pooledObjects(obj));     // create obj of platforms for the nuber specifed in pooledAmount;
     //   GameObject obj = Instantiate(segmentArr[randomInt]) as GameObject;
      //  obj.SetActive(false);                                       // turn off by default;
       //    pooledObjects.Add(obj);                                     // Add gameobject to pooledObjects List
       //    return obj;   
        // Return the new game object to the game and it can be used going forward in the list

   // }
}
   

