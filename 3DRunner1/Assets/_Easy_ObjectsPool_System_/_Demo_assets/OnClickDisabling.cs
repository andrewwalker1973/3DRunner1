using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OnClickDisabling : MonoBehaviour 
{
	//=======================================================================================================
	// Disables current object if it have been clicked
	void OnMouseDown () 
	{
		gameObject.SetActive(false);
	} 

	//--------------------------------------------------------------------------------------------------------
}