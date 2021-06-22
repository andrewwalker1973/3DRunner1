using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    // Infomation for Platforms
    public Transform platformGenerator;             // needs the platform generator game aboject
    private Vector3 platformStartPoint;             // Where to start creating platforms
   private PlatformDestroyer[] platformList;       // Create an array of platforms to disable when restarting the game


    // Information for Player
    public PlayerMotor thePlayer;              // reference to player object
    private Vector3 playerStartPoint;               // where does the player start
    public bool isRunning = false;                  // Start off with not running


    // Refernces to scripts needed
    private ScoreManager theScoreManager;           // reference the scoremanager script
    private PowerUpManager thepowerUpManager;       // referenc ethe powerup manager


    //UI Screen objects
    public HiScoreMenu theHiScoreMenu;                  // Refeence the Hi score menu

    


    //PowerUp Settings

   public bool powerUpReset;                       // bool to reset the powerup to turn them off on restart





    //public MainMenu theMainMenu;


    //public Text countDownDisplay;






    void Start()
    {

        platformStartPoint = platformGenerator.position;            // set the platform startpoint
        playerStartPoint = thePlayer.transform.position;             // set the player start point
        theScoreManager = FindObjectOfType<ScoreManager>();         // find the score Manager script
        thepowerUpManager = FindObjectOfType<PowerUpManager>();         // find the Powerup Manager script


    }



    public void RestartGame()           // function to be called from other scripts to restart the game
    {
       
    }



  

    public void StartGame()           // function to be called from other scripts to restart the game
    {
   
        theScoreManager.scoreIncreasing = false;         // Start increasing score
        thePlayer.gameObject.SetActive(true);              // disable the player
        isRunning = false;                              // Stop running
    }


 

}
