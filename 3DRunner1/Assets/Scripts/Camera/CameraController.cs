using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    //Variables for camera follow next to player
    public PlayerController thePlayerObject;            // public object for the actual player gameobject
    private Vector3 lastPlayerPosition;                 // Vector3 to store the x,y,z of player before it moved forward
    private float distanceToMove;                       // float to determine how far the player actuall moved


    void Start()
    {
        thePlayerObject = FindObjectOfType<PlayerController>();         // find the player controller on the player object
        lastPlayerPosition = thePlayerObject.transform.position;        // get the last position of the player object at start of game

    }

   
    void Update()
    {
        distanceToMove = thePlayerObject.transform.position.x - lastPlayerPosition.x;       // determine how far to move the camera, distance from where the player is to where he was before move
        transform.position = new Vector3(transform.position.x + distanceToMove, transform.position.y, transform.position.z); // set the camera positon to new position based on above calculation
        lastPlayerPosition = thePlayerObject.transform.position;                        // Store the current position so that we can re-run code to move again.
    }
}
