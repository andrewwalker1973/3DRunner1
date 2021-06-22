using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickup : MonoBehaviour
{
    public int crystalToGive;     // what point value to give
    //private int coinScore = 1;

    private ScoreManager theScoreManager;       // reference the score manager

    // Sound setup for crystal
    private AudioManager audioManager;
    public string crystalSoundName;


    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script
        audioManager = AudioManager.instance;           //  audioData = GetComponent<AudioSource>();
        if (audioManager == null)
        {
            Debug.LogError("No audio manager found in scene");
        }
    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))       // if hit the player
        {
            audioManager.PlaySound(crystalSoundName);       //AW need a sound to play
            theScoreManager.AddCrystals(crystalToGive);
            


            gameObject.SetActive(false);                    // set game object disable

        }
    }
}
