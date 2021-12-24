using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SomeOtherClass : MonoBehaviour
{
    public Dictionary<string, BadGuy> badguys = new Dictionary<string, BadGuy>();

    void Start()
    {
        //This is how you create a Dictionary. Notice how this takes
        //two generic terms. In this case you are using a string and a
        //BadGuy as your two values.

        Debug.Log("Starting");

        BadGuy bg1 = new BadGuy("Harvey", 50);
        BadGuy bg2 = new BadGuy("Magneto", 100);

        //You can place variables into the Dictionary with the
        //Add() method.
        badguys.Add("gangster", bg1);
        badguys.Add("mutant", bg2);
        badguys.Add("mutant1", bg2);



        BadGuy magneto = badguys["mutant"];
        Debug.Log("Count " + badguys.Count);

        BadGuy temp = null;

        //This is a safer, but slow, method of accessing
        //values in a dictionary.
        if (badguys.TryGetValue("birds", out temp))
        {
            //success!
        }
        else
        {
            //failure!
        }
    }
}


    
