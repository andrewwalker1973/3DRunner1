using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{

    private Transform lookAt;
    public Vector3 offset;
    public Vector3 rotation;

    public GameManager theGameManager;


    private void Start()
    {
        theGameManager = FindObjectOfType<GameManager>();
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
    } 

    private void LateUpdate()
    {
        if (!theGameManager.isRunning)
        {
            return;
        }

        Vector3 desiredPosition = lookAt.position + offset;
        desiredPosition.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPosition,  2f * Time.deltaTime );
        
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 2f *Time.deltaTime);

    }
}


