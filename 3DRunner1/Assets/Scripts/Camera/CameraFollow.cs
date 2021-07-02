using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform Target;
    private Vector3 Offset;
    private float PosY;
    public float SpeedFollow = 5f;

 

    // Start is called before the first frame update
    void Start()
    {
        Offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 followPos = Target.position + Offset;
        RaycastHit hit;
        if (Physics.Raycast(Target.position, Vector3.down, out hit, 3.5f)) // was 2.5f
        {
            PosY = Mathf.Lerp(PosY, hit.point.y, Time.deltaTime * SpeedFollow);
        }
        else
        {
            PosY = Mathf.Lerp(PosY, Target.position.y, Time.deltaTime * SpeedFollow);
        }
        followPos.y = Offset.y + PosY;
       
         transform.position = followPos;

    }
}
