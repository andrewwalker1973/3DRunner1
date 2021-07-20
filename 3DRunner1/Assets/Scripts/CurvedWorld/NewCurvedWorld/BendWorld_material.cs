using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BendWorld_material : MonoBehaviour
{
    public Material curvedShader;
    
  
  
    // Update is called once per frame
    void Update()
    {

        curvedShader.SetVector("_QOffset", GlobalBendWorld.currentCurve);
    }

    
}
