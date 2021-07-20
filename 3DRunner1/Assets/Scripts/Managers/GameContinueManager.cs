using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameContinueManager : MonoBehaviour
{
    //UI Continue functions
    private bool continueSelected = false;          // Has the continue button been pressed
    public int TotalcountDownTime;                  // How long to wait for countdown to main menu
    private int countDownTime;                      // Int for contdown
    private int safeModeTime = 5;
    public Image OnContinueTimer;                   // Image to show screen timer
    public string mainMenuLevel;                    // refeence for main menu
    public int CrystaltoContinue = 0;               // How many crystals to continue
    public TextMeshProUGUI CrystaltoContinueText;           // Crystals to spend display Text
    private int continueCounter = 0;                    // How many times are we restarting
    public TextMeshProUGUI CrystalTotalText;           // Crystals to spend display Text
    public Image PowerUpContinueGift;                   // Gift powerup when contiue with Vid
    public Sprite Magnetgift;
    public Sprite doublegift;
    public int giftSelector;
    public ContinueMenu theContinueScreen;                // refernec the death screen
    public Button CrystalContinueBuyButton;             // button to click to spend cyrsyatls to continue
    public float ReturnToMainScreenDuration;            // How long before we return to main screen
    public Image ReturnToMainScreenfillImage;           // Radial image for fill bar
    public TextMeshProUGUI countDownDisplay;           // Count down display Text
    public HiScoreMenu theHiScoreMenu;                  // Refeence the Hi score menu

    public GameObject SafeModeImage;
    public SafeModePowerbar SafePowerbar;
 //   public Slider safeslider;



    // Refernces to scripts needed
    private ScoreManager theScoreManager;           // reference the scoremanager script
    private PowerUpManager thepowerUpManager;       // referenc ethe powerup manager
    private GameManager theGameManager;
    public PlayerMotor thePlayer;              // reference to player object

    public void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();         // find the score Manager script
        thepowerUpManager = FindObjectOfType<PowerUpManager>();         // find the Powerup Manager script
        theGameManager = FindObjectOfType<GameManager>();
        ReturnToMainScreenfillImage.fillAmount = 1f;                    // Set fill amount to full
    }

    public void PlayerDiedContinueOption()
    {

        // AW Code to reset play to x = 0 
        // AW code to reset player to hightr y to drop into screen,, kinda prevents runing though ramps.

        theGameManager.isRunning = false;          // Stop runnig
        theScoreManager.scoreIncreasing = false;         // stop increasing score
        safeModeTime = 5;
        SafePowerbar.SetMaxSafePower(safeModeTime);
        thepowerUpManager.clearAllPowerUpDurations();
      //  thePlayer.gameObject.SetActive(false);              // disable the player
        theContinueScreen.gameObject.SetActive(true);          // bring up the contunue Menu screen
        CrystalTotalText.text = theScoreManager.crystalCount.ToString();   //Display total number of crustals we have
        CrystalsToContinue();                                   // display amiunt of crystaks to contunue

        // determine if we have enogh crystals
        if (CrystaltoContinue > theScoreManager.crystalCount)
        {
            // Disable the crystal purchase option
            CrystalContinueBuyButton.gameObject.SetActive(false);

        }
        OnContinueTimer.gameObject.SetActive(true);             // Start the countdoewn on retunr to main menu
        StartCoroutine(ReturnToMainScreenTimer(ReturnToMainScreenDuration));    // Set the duration of the timer
        giftSelector = 0;
        PowerUpContinueGift.gameObject.SetActive(false);

        if (continueCounter > 1)
        {
            // Display gift powerup
            PowerUpContinueGift.gameObject.SetActive(true);
            giftSelector = Random.Range(1, 3);
            if (giftSelector == 1)
            {
                //Magent as powerup
                PowerUpContinueGift.GetComponent<Image>().sprite = Magnetgift;
            }
            if (giftSelector == 2)
            {
                //Double points as powerup
                PowerUpContinueGift.GetComponent<Image>().sprite = doublegift;
            }

        }



    }
    public void ContinueGameCrystals()
    {
        // code to check for crystal purchase
        // Slow the speed down a bit 

        theScoreManager.crystalCount = theScoreManager.crystalCount - CrystaltoContinue;
        theScoreManager.SaveCrystalCount();
        theScoreManager.SaveTotalCoinCount();
       theScoreManager.SaveHighScore();                          // AW save high score but dont open screen until back at main menu
        theScoreManager.SaveRunningTime(theScoreManager.runningTime);
        giftSelector = 0;
        continueSelected = true;                                // Selected continue, so stop return to main menu
        theContinueScreen.gameObject.SetActive(false);          // stop  the contunue Menu screen
        OnContinueTimer.gameObject.SetActive(false);            // disable the countdown timer

        StartCoroutine(CountDownToStart());         // show countd down on screen     
    }

    public void ContinueGameVideo()
    {
        // code to check for crystal purchase

        // Slow the speed down a bit 
        // AW code her to play vids
        theScoreManager.SaveCrystalCount();
        theScoreManager.SaveTotalCoinCount();
        theScoreManager.SaveHighScore();                      // AW save high score but dont open screen until back at main menu
        theScoreManager.SaveRunningTime(theScoreManager.runningTime);
        continueSelected = true;                                // Selected continue, so stop return to main menu
        theContinueScreen.gameObject.SetActive(false);          // stop  the contunue Menu screen
        OnContinueTimer.gameObject.SetActive(false);            // disable the countdown timer
     /*   if (giftSelector == 1)
        {
            //Magent as powerup
            // thepowerUpManager.ActivatePowerUp(doublePoints, shieldMode, magnet, fasterMode, slowerMode, powerupLength);
            thepowerUpManager.ActivatePowerUp(false, false, true, false, false, 12f);
        }
        if (giftSelector == 2)
        {
            //Double as powerup
            PowerUpContinueGift.GetComponent<Image>().sprite = doublegift;
            thepowerUpManager.ActivatePowerUp(true, false, false, false, false, 12f);
        }
     */
        StartCoroutine(CountDownToStart());         // show countd down on screen     
    }
    IEnumerator StopSafeModeRoutine()
    {
        // AW would be nice to have a visual refernce for this
        // yield return new WaitForSeconds(5f);            // Wait 5 seconds
        while (safeModeTime > 0)
        {

            // countDownDisplay.text = countDownTime.ToString();       // dispaly text and convert to string
            SafePowerbar.SetSafePower(safeModeTime);         // Display progress bar and convert float to int
            Debug.Log("Safe time " + safeModeTime);
            yield return new WaitForSeconds(1f);                    // Wait for 1 sec
            safeModeTime--;                                        // decrease by 1 sec
        }

        SafeModeImage.SetActive(false);
        thePlayer.IsNotSafe();                          // Turn safe mode off
        continueSelected = false;                       // rest continue back to false so that next check will be false

    }


    IEnumerator CountDownToStart()
    {
        countDownTime = TotalcountDownTime;         // Set the countdown timer
        countDownDisplay.gameObject.SetActive(true);    // Display the  countdown timer
        thePlayer.SetPlayerIdle();
        while (countDownTime > 0)
        {

            countDownDisplay.text = countDownTime.ToString();       // dispaly text and convert to string
            yield return new WaitForSeconds(1f);                    // Wait for 1 sec
            countDownTime--;                                        // decrease by 1 sec
        }


        countDownDisplay.gameObject.SetActive(false);           // Disable the countdown display game object
        SafeModeImage.SetActive(true);
        thePlayer.IsSafe();                                     // Set safe mode for player
        theGameManager.isRunning = true;                                        // Start runnig
    //    safeslider.maxValue = safeModeTime;
    //    safeslider.value = safeModeTime;
   
        thePlayer.gameObject.SetActive(true);                     // enable the player

        if (giftSelector == 1)
        {
            //Magent as powerup
            // thepowerUpManager.ActivatePowerUp(doublePoints, shieldMode, magnet, fasterMode, slowerMode, powerupLength);
            thepowerUpManager.ActivatePowerUp(false, false, true, false, false, 12f);
        }
        if (giftSelector == 2)
        {
            //Double as powerup
            PowerUpContinueGift.GetComponent<Image>().sprite = doublegift;
            thepowerUpManager.ActivatePowerUp(true, false, false, false, false, 12f);
        }



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

            theScoreManager.SaveCrystalCount();
            theScoreManager.SaveTotalCoinCount();
            theScoreManager.SaveHighScore();
            theScoreManager.SaveRunningTime(theScoreManager.runningTime);
            // Check for HiScore
            if (theScoreManager.highScoreAchieved)
            {
                theContinueScreen.gameObject.SetActive(false);
                OnContinueTimer.gameObject.SetActive(false);
                theHiScoreMenu.gameObject.SetActive(true);
            }
            else
            {
                        SceneManager.LoadScene(mainMenuLevel);      // reload main menu // AW maybe a better way to di this.
            }
        }
    }

    private void CrystalsToContinue()
    {
        // count how many times we are restarting
        // incrrease count based on count
        continueCounter++;
        if (continueCounter == 1)
        {
            // Set crystals by 2
            CrystaltoContinue = 1;
            CrystaltoContinueText.text = CrystaltoContinue.ToString();
        }

        if (continueCounter == 2)
        {
            // Set crystals by 2
            CrystaltoContinue = 2;
            CrystaltoContinueText.text = CrystaltoContinue.ToString();
        }
        if (continueCounter > 2)
        {
            // Set crystals by 4
            CrystaltoContinue = CrystaltoContinue + 4;
            CrystaltoContinueText.text = CrystaltoContinue.ToString();
            
        }

    }

}
