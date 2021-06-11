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
   // private ReturnToMainScreenfromPlayTimer theReturnToMainScreenfromPlayTimer;


    //UI Screen objects
    public ContinueMenu theContinueScreen;                // refernec the death screen
    public HiScoreMenu theHiScoreMenu;                  // Refeence the Hi score menu
   // public MainMenu theMainMenu;                        // Refenec the main menu scene
     public TextMeshProUGUI countDownDisplay;           // Count down display Text
    public float ReturnToMainScreenDuration;            // How long before we return to main screen
    public Image ReturnToMainScreenfillImage;           // Radial image for fill bar
    public Button CrystalContinueBuyButton;             // button to click to spend cyrsyatls to continue
    


    //PowerUp Settings

    public bool powerUpReset;                       // bool to reset the powerup to turn them off on restart

    //UI Continue functions
    private bool continueSelected = false;          // Has the continue button been pressed
    public int TotalcountDownTime;                  // How long to wait for countdown to main menu
    private int countDownTime;                      // Int for contdown
    public Image OnContinueTimer;                   // Image to show screen timer
    public string mainMenuLevel;                    // refeence for main menu
    public int CrystaltoContinue = 0;               // How many crystals to continue
    public TextMeshProUGUI CrystaltoContinueText;           // Crystals to spend display Text
    private int continueCounter = 0;                    // How many times are we restarting
    public TextMeshProUGUI CrystalTotalText;           // Crystals to spend display Text




    //public MainMenu theMainMenu;


    //public Text countDownDisplay;






    void Start()
    {

        platformStartPoint = platformGenerator.position;            // set the platform startpoint
        playerStartPoint = thePlayer.transform.position;             // set the player start point
        theScoreManager = FindObjectOfType<ScoreManager>();         // find the score Manager script
       // theReturnToMainScreenfromPlayTimer = FindObjectOfType<ReturnToMainScreenfromPlayTimer>();

        ReturnToMainScreenfillImage.fillAmount = 1f;                    // Set fill amount to full

    }



    public void RestartGame()           // function to be called from other scripts to restart the game
    {
       // theScoreManager.scoreIncreasing = false;         // stop increasing score
      //  thePlayer.gameObject.SetActive(false);              // disable the player
      //  if (theScoreManager.highScoreAchieved == true)
     //   {
     //       theHiScoreMenu.gameObject.SetActive(true);
     //   }
     //   else
     //   {
        //    theDeathScreen.gameObject.SetActive(true);          // bring up the death Menu screen
     //   }



    }



  /*  public void ResetToBegining()               // AW Dont want to reset. want to continue
    {
       
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
*/

    public void StartGame()           // function to be called from other scripts to restart the game
    {
   
        theScoreManager.scoreIncreasing = false;         // Start increasing score
        thePlayer.gameObject.SetActive(true);              // disable the player
        isRunning = false;                              // Stop running
    }


    public void PlayerDiedContinueOption()
    {

        isRunning = false;          // Stop runnig
        theScoreManager.scoreIncreasing = false;         // stop increasing score
        thePlayer.gameObject.SetActive(false);              // disable the player
        theContinueScreen.gameObject.SetActive(true);          // bring up the contunue Menu screen
        CrystalTotalText.text = theScoreManager.crystalCount.ToString();   //Display total number of crustals we have
        Debug.Log("Crystals in hand " + theScoreManager.crystalCount);
        CrystalsToContinue();                                   // display amiunt of crystaks to contunue

        // determine if we have enogh crystals
        if (CrystaltoContinue > theScoreManager.crystalCount)
        {
            // Disable the crystal purchase option
            CrystalContinueBuyButton.gameObject.SetActive(false);

        }
        OnContinueTimer.gameObject.SetActive(true);             // Start the countdoewn on retunr to main menu
        StartCoroutine(ReturnToMainScreenTimer(ReturnToMainScreenDuration));    // Set the duration of the timer


    }

    public void ContinueGame()
    {
        // code to check for crystal purchase
        // code for reactivate player
        // code for safe mode for 3 seconds
        // countdown timer to show when starting
        // code to check how many times they restarted
        // Slow the speed down a bit 
        // Reactivate the game time
        theScoreManager.crystalCount = theScoreManager.crystalCount - CrystaltoContinue;
        continueSelected = true;                                // Selected continue, so stop return to main menu
        theContinueScreen.gameObject.SetActive(false);          // stop  the contunue Menu screen
        OnContinueTimer.gameObject.SetActive(false);            // disable the countdown timer
        StartCoroutine(CountDownToStart());         // show countd down on screen     
    }

    IEnumerator StopSafeModeRoutine()
    {
        // AW would be nice to have a visual refernce for this
        yield return new WaitForSeconds(5f);            // Wait 5 seconds
        thePlayer.IsNotSafe();                          // Turn safe mode off
        continueSelected = false;                       // rest continue back to false so that next check will be false

    }


    IEnumerator CountDownToStart()
    {
        countDownTime = TotalcountDownTime;         // Set the countdown timer
        countDownDisplay.gameObject.SetActive(true);    // Display the  countdown timer
       // Time.timeScale = 1f;                        // Set time back to normal
        while (countDownTime > 0)
        {

            countDownDisplay.text = countDownTime.ToString();       // dispaly text and convert to string
            yield return new WaitForSeconds(1f);                    // Wait for 1 sec
            countDownTime--;                                        // decrease by 1 sec
        }

        countDownDisplay.text = "GO !";                         // Set display to GO
        countDownDisplay.text = countDownTime.ToString();       // dispaly text and convert to string
        countDownDisplay.gameObject.SetActive(false);           // Disable the countdown display game object
        isRunning = true;                                        // Start runnig
        thePlayer.IsSafe();                                     // Set safe mode for player
        thePlayer.gameObject.SetActive(true);                     // enable the player



        StartCoroutine(StopSafeModeRoutine());                  // Stop Safe Mode after a few sec


    }


    public IEnumerator ReturnToMainScreenTimer(float ReturnToMainScreenDuration)        // Return to main screen timer
    {

        float startTime = Time.time;                // what si the time
        float time = ReturnToMainScreenDuration;        // how long to wait before opening main screen
        float value = 0;                                // set value to 0

        while (Time.time - startTime < ReturnToMainScreenDuration)      // while time is less than duration
        {
            time -= Time.deltaTime;                                     // decrease time
            value = time / ReturnToMainScreenDuration;                  
            ReturnToMainScreenfillImage.fillAmount = value;             // Update fill amount
            yield return null;                                          // break out of while loop

        }

        if (!continueSelected)              // if continue not selected
        {

            SceneManager.LoadScene(mainMenuLevel);      // reload main menu
        }
    }

    private void CrystalsToContinue()
    {
        // count how many times we are restarting
        // incrrease count based on count
        Debug.Log("Restart counter" + continueCounter);
        continueCounter++;
        Debug.Log("CrystaltoContinue1" + CrystaltoContinue);
        if (continueCounter == 1)
        {
            // Set crystals by 2
            CrystaltoContinue = 1;
            CrystaltoContinueText.text = CrystaltoContinue.ToString();
        }

        if (continueCounter == 2)
        {
            // Set crystals by 2
            Debug.Log("counter1 " + CrystaltoContinue);
            CrystaltoContinue = 2;
            CrystaltoContinueText.text = CrystaltoContinue.ToString();
            Debug.Log("counter2 " + CrystaltoContinue);
        }
        if (continueCounter > 2)
        {
            // Set crystals by 4
            Debug.Log("counter1 " + CrystaltoContinue);
            CrystaltoContinue = CrystaltoContinue + 4;
            CrystaltoContinueText.text = CrystaltoContinue.ToString();
            Debug.Log("counter2 " + CrystaltoContinue);
        }

    }

}
