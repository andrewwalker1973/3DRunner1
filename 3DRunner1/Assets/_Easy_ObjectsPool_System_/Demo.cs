//--------------------------------------------------------------------------------------------------------
// Just a script for Demo-scene
// Eextracts related objects from pool by name
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Demo : MonoBehaviour 
{
	public PoolManager poolManager;


	//=======================================================================================================
	void OnGUI () 
	{
		GUI.Label(new Rect (0, 0, 250,  25), "EXTRACT FROM POOL: " );

		if (poolManager)
		{
			if (GUI.Button (new Rect (0, 25, 120,  50), "BLOCKS")) 	poolManager.GetObjectByName("Cube"); 
			if (GUI.Button (new Rect (0, 75, 120,  50), "BOMBS")) 	poolManager.GetObjectByName("Bomb"); 
			if (GUI.Button (new Rect (0, 125, 120,  50), "SPIKES"))	poolManager.GetObjectByName("Spike"); 
			if (GUI.Button (new Rect (0, 200, 120,  50), "RANDOM"))	poolManager.GetRandomObject();
			if (GUI.Button (new Rect (0, 270, 120,  50), "EVERYTHING"))
				while (poolManager.Pool.Count > 0) 
					poolManager.GetObjectByID(0);
		}
		else 
			GUI.Label(new Rect (0, Screen.height-25, 250,  25), "!!! poolManager is missed !!!" );
	}

	//----------------------------------------------------------------------------------
}