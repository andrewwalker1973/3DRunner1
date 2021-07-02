using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveForward : MonoBehaviour
{
    private bool enemyRunNow = false;

    public float speed;
    

    void Update()
    {

        if (enemyRunNow)
        {
           
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Player"))
        {

            enemyRunNow = true;

        }
    }
}
