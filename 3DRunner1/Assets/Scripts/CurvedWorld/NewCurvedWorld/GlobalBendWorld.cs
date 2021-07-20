using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalBendWorld : MonoBehaviour
{

   
     Vector4 FlatArea = new Vector4(0f, 0f, 0f, 0f);
     Vector4 Curved = new Vector4(0f, -20f, 0f, 0f);
    Vector4 LeftCurved = new Vector4(-40f, -20f, 0f, 0f);
    Vector4 RightCurved = new Vector4(40f, -20f, 0f, 0f);
    public static Vector4 currentCurve;
    [SerializeField] bool TurnLeftbool = false;
    [SerializeField] bool TurnRightbool = false;
    [SerializeField] bool NormalCurveTransitionbool = false;

    float timeElapsed;
    float lerpDuration = 3;
 


    private void Start()
    {
        currentCurve = Curved;
    }

 
    public void Update()
    {
        if (TurnLeftbool)
        {
            Debug.Log("turn");
            if (timeElapsed < lerpDuration)
            {
                currentCurve = Vector4.Lerp(currentCurve, LeftCurved, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }
               StartCoroutine(RestbacktoNormalCurve());
        }

        if (TurnRightbool)
        {
            if (timeElapsed < lerpDuration)
            {
                currentCurve = Vector4.Lerp(currentCurve, RightCurved, timeElapsed / lerpDuration);
                timeElapsed += Time.deltaTime;
            }
            StartCoroutine(RestbacktoNormalCurve());
        }
     
    }

    public void TurnLeft()
    {
       TurnLeftbool = true;
        

    }

    public void TurnRight()
    {
        TurnRightbool = true;
    }

    IEnumerator RestbacktoNormalCurve()
    {
        
        
        yield return new WaitForSeconds(5f);
        TurnLeftbool = false;
        TurnRightbool = false;
        timeElapsed = 0;
       while (timeElapsed < lerpDuration)
        {
            currentCurve = Vector4.Lerp(currentCurve, Curved, timeElapsed / lerpDuration * Time.deltaTime);
            timeElapsed += Time.deltaTime;
        }

        timeElapsed = 0;
    }

   
}
