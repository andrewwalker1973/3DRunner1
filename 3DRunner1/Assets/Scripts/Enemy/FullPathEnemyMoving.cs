using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullPathEnemyMoving : MonoBehaviour
{
    public float speed;
    public float maxTransform;
    public float minTransform;

    void Update()
    {

        transform.Translate(speed * Time.deltaTime, 0, 0);
        checkDirection();


    }

    public void checkDirection()
    {

        /*if (transform.position.x < maxTransform)
        {

            Debug.Log("transform.position.x" + transform.position.x);
            Debug.Log("maxTransform" + maxTransform);
            Debug.Log("move  left");
            speed = speed * 1;
        }
        */
         if (transform.position.x >= maxTransform)
        {
            Debug.Log("move  right");
            speed = speed * -1;


        }

        else if (transform.position.x <= minTransform)
        {

            speed = speed * -1;
        }

    }
}
