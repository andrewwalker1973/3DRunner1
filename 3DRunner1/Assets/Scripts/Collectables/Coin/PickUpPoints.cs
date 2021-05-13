using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPoints : MonoBehaviour
{
    public int scoreToGive;     // what point value to give
    private int coinScore = 1;

    private ScoreManager theScoreManager;       // reference the score manager

    // Sound setup for coin
    private AudioManager audioManager;
    public string coinSoundName;


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
        
        if(other.gameObject.CompareTag("Player"))       // if hit the player
        {
            audioManager.PlaySound(coinSoundName);
            theScoreManager.AddScore(scoreToGive);          // send point value to scoreManger
            theScoreManager.AddCoins(coinScore);          // send point value to scoreManger


            gameObject.SetActive(false);                    // set game object disable
     
        }
    }
}
