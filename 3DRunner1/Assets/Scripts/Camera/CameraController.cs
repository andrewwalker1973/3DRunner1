using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Camrea controls when in 2D
    /*  //Variables for camera follow next to player
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

      */



    /* public Transform target;

     private readonly float _distance = 15.0f;
     // private readonly float _height = 0.7f;
     private readonly float _height = 3f;
     private readonly float _heightOffset = 0.0f;
     private readonly float _heightDamping = 5.0f;
     private readonly float _rotationDamping = 3.0f;

     public bool IsMoving { set; get; }
     public Vector3 rotation = new Vector3(35, 0, 0);


     private void Start()
     {
         //attempt at origion root
         //  gameObject.transform.SetParent(OrigionRoot.transform,true);
     }

    // void LateUpdate()
    void Update()
     {
         if (target == null) return;

     //    if (!IsMoving)
     //        return;

         var position = transform.position;
         var targetPosition = target.position;

         //   if (!PlayerController.Dead)
         //  {
         var wantedRotationAngle = target.eulerAngles.y;
         var wantedHeight = targetPosition.y + _height;

         var currentRotationAngle = transform.eulerAngles.y;
         var currentHeight = position.y;

         currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, _rotationDamping * Time.deltaTime);
         currentHeight = Mathf.Lerp(currentHeight, wantedHeight, _heightDamping * Time.deltaTime);

         var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

         position = targetPosition;

         var distance = Vector3.forward * _distance;
         position -= currentRotation * distance;

         transform.position = new Vector3(position.x, currentHeight + _heightOffset, position.z);

         // 
         transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 2.0f * Time.deltaTime);

     }
    */

    private Transform lookAt;
    private  Vector3 startOffset;
    [SerializeField]private Vector3 moveVector;

    private float transition = 0.0f;
    private float animationDuration = 2.0f;
    private Vector3 animationOffset = new Vector3(0, 5, -13);

    private void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag ("Player").transform;
        startOffset = transform.position - lookAt.position;
    }

    private void Update()
    {
        moveVector = lookAt.position + startOffset;

        // X
        moveVector.x = 0;

        // Y
        moveVector.y = Mathf.Clamp(moveVector.y, 3, 5);   // clamp y to be between 3 and 5 height

        if (transition > 1.0f)
        {
            transform.position = moveVector;

        }
        else
        {
            // Animation at start of Game
            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / animationDuration;
            transform.LookAt(lookAt.position + Vector3.up);
        }

    }
}
