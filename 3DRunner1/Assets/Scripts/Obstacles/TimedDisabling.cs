//--------------------------------------------------------------------------------------------------------
// Example script.
// The simple script, that disables/destruct current object after lifeTime time
//--------------------------------------------------------------------------------------------------------
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TimedDisabling : MonoBehaviour 
{
	public float lifeTime = 3;  				// After this time object will be destroyed
	public bool onlyIfNotVisible = false;   	// Disable only when object is no longer visible by any camera
	public bool alsoDestroy = false;			// Script will also destroy the object
	public bool randomize = false;			  	// Randomize lifeTime in randomized_LifeTime_limits
	public Vector2 randomizedLifeLimits;  		// Limits for lifeTime randomize				 

	// Important internal variables, please don't change them blindly
	bool ReadyForDisabling = false;
	float TimeForDisabling;


	//=======================================================================================================
	// Setup Time when object will be disabled
	void Start () 
	{
		if(randomize)
		{
			// Random.seed = Time.time;
			TimeForDisabling = Time.time + Random.Range(randomizedLifeLimits.x, randomizedLifeLimits.y);
			randomize = false;
		}
		else
			TimeForDisabling = lifeTime + Time.time;
	} 

	//---------------------------------------------------------------------------------------------------------	
	// Reset Time when object will be disabled if object had re-enabled
	void OnEnable () 
	{
		Start();
	}

	//---------------------------------------------------------------------------------------------------------	
	// Check visibility to allow destroying (only if onlyIfNotVisible=true)
	void OnBecameInvisible () 
	{
		if (onlyIfNotVisible)
			ReadyForDisabling = true;
	}

	//----------------
	void OnBecameVisible () 
	{
		if (onlyIfNotVisible) 
			ReadyForDisabling = false;
	}

	//---------------------------------------------------------------------------------------------------------	
	// Disable/Destroy the object if it lifeTime has expired
	void Update () 
	{
		if (Time.time > TimeForDisabling  &&  ReadyForDisabling) 
		{
			gameObject.SetActive (false);
			
			if (alsoDestroy) 
				Destroy (gameObject); 
		}

	}

	//---------------------------------------------------------------------------------------------------------

}