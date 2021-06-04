using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public Transform platformGenerator;             // needs the platform generator game aboject
    private Vector3 platformStartPoint;             // Where to start creating platforms

    public PlayerMotor thePlayer;              // reference to player object
    private Vector3 playerStartPoint;               // where does the player start

    private PlatformDestroyer[] platformList;       // Create an array of platforms to disable when restarting the game

    private ScoreManager theScoreManager;           // reference the scoremanager script

    public DeathMenu theDeathScreen;                // refernec the death screen
    public HiScoreMenu theHiScoreMenu;
    public bool powerUpReset;                       // bool to reset the powerup to turn them off on restart

    public bool RunbuttonPressed = false;
    public Button theRunbutton;

   


    void Start()
    {

        platformStartPoint = platformGenerator.position;            // set the platform startpoint
        playerStartPoint = thePlayer.transform.position;             // set the player start point
        theScoreManager = FindObjectOfType<ScoreManager>();         // find the score Manager script

    }

    

    public void RestartGame()           // function to be called from other scripts to restart the game
    {
        theScoreManager.scoreIncreasing = false;         // stop increasing score
        thePlayer.gameObject.SetActive(false);              // disable the player
        if (theScoreManager.highScoreAchieved == true)
        {
            theHiScoreMenu.gameObject.SetActive(true);
        }
        else
        {
            theDeathScreen.gameObject.SetActive(true);          // bring up the death Menu screen
        }



}

  

    public void ResetToBegining()               // AW Dont want to reset. want to continue
    {
        theDeathScreen.gameObject.SetActive(false);          // stop up the death Menu screen
        theHiScoreMenu.gameObject.SetActive(false);
        theScoreManager.highScoreAchieved = false;

        platformList = FindObjectsOfType<PlatformDestroyer>();      // generate a list of all active platfomrs and disable
        for (int i = 0; i < platformList.Length; i++)
        {
            platformList[i].gameObject.SetActive(false);
        }

        thePlayer.transform.position = playerStartPoint;            // reset player to start point
        platformGenerator.position = platformStartPoint;            // reset platfomr generation to start point
        thePlayer.gameObject.SetActive(true);                       // re-enable the player
        theScoreManager.scoreCount = 0;                             // reset score back to 0
        theScoreManager.scoreIncreasing = true;                     // let score being increasing again
        powerUpReset = true;                                        // reset powerup duration to 0 to turn them off

        
    }

    
}
