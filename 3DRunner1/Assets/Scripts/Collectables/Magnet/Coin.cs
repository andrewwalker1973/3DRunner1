using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public Transform playerTransform;
    public float moveSpeed = 30f; // was 17
    CoinMove coinMoveScript;

    
    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        coinMoveScript = gameObject.GetComponent<CoinMove>();
        

    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "CoinDetector")
        {
           
            coinMoveScript.enabled = true;
        }
    }
    private void OnDisable()
    {
        coinMoveScript = gameObject.GetComponent<CoinMove>();
        coinMoveScript.enabled = false;
    }

}
