using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NiobiumStudios;
using UnityEngine.UI;



public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;           //TextMesh Pro Text Field for score
    public TextMeshProUGUI hiScoreText;         //TextMesh Pro Text Field for Hiscore
    public TextMeshProUGUI coinScoreText;         //TextMesh Pro Text Field for Hiscore
    public TextMeshProUGUI crystalCountText;         //TextMesh Pro Text Field for Hiscore

    [SerializeField] TextMeshProUGUI metalsText;
    [SerializeField] TextMeshProUGUI TotalCoinScoreText;
    [SerializeField] TextMeshProUGUI gemsText;

    public Image spinnerNotification;
    private GameManager theGameManager;
    private AchievmentManager theAchievmentManager;


    public float scoreCount;                    // What is the score count
    public float hiScoreCount;                  // what is the hi score
    public bool highScoreAchieved = false;
    private int coinScore;
    private int crystal_score;
    public int totalCoinScore;

    public float pointsPerSecond;               // how much to increase score by
    public bool scoreIncreasing;                // is score increasing ? dont want to increase while dead

    public bool shouldDouble;                   // if powerup double active;

    public int crystalCount;

    private string rewardUnit;
    private int rewardAmount;
    public int spinnerCredits;
    public float runningTime;
    private int beatHighScore;



    void Start()
    {
        spinnerNotification.gameObject.SetActive(false);

        theGameManager = FindObjectOfType<GameManager>();
        theAchievmentManager = FindObjectOfType<AchievmentManager>();


        LoadPlayervalues();
        UpdateCoinsTextUI();
        UpdateGemsTextUI();

        //AW debug number fro testing
        //   spinnerCredits = 2;
        //  SaveSpinnerCount(spinnerCredits);

        



    }

    public void LoadPlayervalues()
    {
        if (PlayerPrefs.HasKey("HighScore"))                // if highscore exists in playprefs
        {
            // pull from player prefs
            hiScoreCount = PlayerPrefs.GetFloat("HighScore", 0); // pull from player prefs and set to 0 if not found

        }
        if (PlayerPrefs.HasKey("Crystals"))                // if highscore exists in playprefs
        {
            // pull from player prefs
            crystalCount = PlayerPrefs.GetInt("Crystals", 0); // pull from player prefs and set to 0 if not found

            // AW tesmp to play with crystals in purchasing ---- remove
            //crystalCount = 10;
        }

        if (PlayerPrefs.HasKey("TotalCoinScore"))                // if highscore exists in playprefs
        {
            // pull from player prefs

            totalCoinScore = PlayerPrefs.GetInt("TotalCoinScore", 0); // pull from player prefs and set to 0 if not found


            // AW tesmp to play with coins in purchasing ---- remove
            //totalCoinScore = 4994;
        }

        if (PlayerPrefs.HasKey("TotalSpinnerCredits"))                
        {

            spinnerCredits = PlayerPrefs.GetInt("TotalSpinnerCredits", 0); // pull from player prefs and set to 0 if not found

        }

        if (PlayerPrefs.HasKey("RunningTime"))      // how long the player has been running
        {

            runningTime = PlayerPrefs.GetFloat("RunningTime", 0); // pull from player prefs and set to 0 if not found

           // runningTime = 71980;
        }
        if (PlayerPrefs.HasKey("BeatHighScore"))      // how long the player has been running
        {

            beatHighScore = PlayerPrefs.GetInt("BeatHighScore", 0); // pull from player prefs and set to 0 if not found


        }
    }

    void Update()
    {
       
        if (scoreIncreasing && theGameManager.isRunning)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;     // how much to increase by per second
        }
        

      

        scoreText.text = "Score : " + Mathf.Round (scoreCount);           // set the scorecout on screen rount to solid number
        hiScoreText.text = "HiScore : " + Mathf.Round(hiScoreCount);     // Set the hi score on screen
        coinScoreText.text = "" + Mathf.Round(coinScore);           // set the scorecout on screen rount to solid number
        crystalCountText.text = "" + Mathf.Round(crystal_score);           // set the scorecout on screen rount to solid number

        if (spinnerCredits > 0)
        {
            spinnerNotification.gameObject.SetActive(true);
        }
       if (spinnerCredits == 0)
        {
            spinnerNotification.gameObject.SetActive(false);
        }


        // check for achievemets on running time
        if (runningTime > 3600 && runningTime < 3601)
        {
            theAchievmentManager.EarnAchievment("Run 3600 Secs");
        }
        if (runningTime > 72000 && runningTime < 72001)
        {
            theAchievmentManager.EarnAchievment("Run 72000 Secs");
        }
        if (runningTime > 18000 && runningTime < 18001)
        {
           theAchievmentManager.EarnAchievment("Run 18000 Secs");
        }
    }

    public void AddScore(int pointsToAdd)
    {
        if (shouldDouble)               // if we are doubling points
        {
            pointsToAdd = pointsToAdd * 2;  // double the points
        }
        scoreCount += pointsToAdd;      // otherwise normal add
     }

    public void AddCoins(int coinsToAdd)
    {
        
        coinScore += coinsToAdd;      // Add coins 
        totalCoinScore += coinsToAdd;    // Add to total coin score
        if (totalCoinScore == 5000)
        { 
            theAchievmentManager.EarnAchievment("Collect 5000 Coins");
        }

        if (totalCoinScore == 50000)
        {
            theAchievmentManager.EarnAchievment("Collect 50000 Coins");
        }

        if (totalCoinScore == 100000)
        {
            theAchievmentManager.EarnAchievment("Collect 100000 Coins");
        }



        //  GameData.Coins += totalCoinScore;
    }

    public void SpinnerAddCoins(int coinsToAdd)
    {

        totalCoinScore += coinsToAdd;    // Add to total coin score
                                         //  GameData.Coins += totalCoinScore;
    }

    public void AddCrystals(int crystalsToAdd)
    {
        crystal_score += crystalsToAdd;
        crystalCount += crystalsToAdd;      // Add coins 
        PlayerPrefs.SetInt("Crystals", crystalCount);            // AW save highscore to playerPrefs may not be the best place for it as this happens while player is runnin
                                                                    // AW need better place to put this. at end of run
    }
    public void SpinnerAddGems(int crystalsToAdd)
    {

        crystalCount += crystalsToAdd;          // Add to total coin score
                                            //  GameData.Coins += totalCoinScore;
        Debug.Log("Total Coin count when collectin" + totalCoinScore);
    }


    public void SpinnerAddSpins(int spinsToAdd)
    {

        spinnerCredits += spinsToAdd;          // Add to total spins count
                                                
        
    }

    public void AddEnemy(int enemyToAdd)
    {

        scoreCount += enemyToAdd;      // Add score for killing enemy 
    }


    public void SaveHighScore()
    {
        
        if (scoreCount > hiScoreCount)                      // if score > hghscore update highscore
        {
            Debug.Log("Save high score");

            hiScoreCount = scoreCount;                      // update highscor
            highScoreAchieved = true;
           
            beatHighScore += 1;
            
                theAchievmentManager.EarnAchievment("Beat High Score");
                theAchievmentManager.EarnAchievment("Beat High Score 10");
         


            PlayerPrefs.SetFloat("HighScore", hiScoreCount);            // AW save highscore to playerPrefs may not be the best place for it as this happens while player is runnin
            PlayerPrefs.SetInt("BeatHighScore", beatHighScore);

            SaveRunningTime(runningTime);
        }
    }

    public void SaveCrystalCount()
    {
        GameData.Gems = crystalCount;
    }

    public void SaveSpinnerCount(int spinCredits)
    {
        
        PlayerPrefs.SetInt("TotalSpinnerCredits", spinCredits);

    }
    public void SaveSpinnerCountTotal()
    {

        PlayerPrefs.SetInt("TotalSpinnerCredits", spinnerCredits);

    }
    public void SaveTotalCoinCount()
    {
        GameData.Coins = totalCoinScore;       
    }

   public void UpdateCoinsTextUI()
    {
        TotalCoinScoreText.text = GameData.Coins.ToString();
    }

    public void UpdateGemsTextUI()
    {
        gemsText.text = GameData.Gems.ToString();

    }

    void OnEnable()
    {
        DailyRewards.instance.onClaimPrize += OnClaimPrizeDailyRewards;
    }

    void OnDisable()
    {
        DailyRewards.instance.onClaimPrize -= OnClaimPrizeDailyRewards;
    }

    // this is your integration function. Can be on Start or simply a function to be called
    public void OnClaimPrizeDailyRewards(int day)
    {
        //This returns a Reward object
        Reward myReward = DailyRewards.instance.GetReward(day);

        // And you can access any property
        print(myReward.unit);   // This is your reward Unit name
        print(myReward.reward); // This is your reward count
        rewardUnit = myReward.unit;
        rewardAmount = myReward.reward;
        if (rewardUnit == "Coins")
        {
            Debug.Log("Coins got" + rewardAmount);

            SpinnerAddCoins(rewardAmount);
            SaveTotalCoinCount();
            UpdateCoinsTextUI();
        }

        if (rewardUnit == "Gems")
        {
            Debug.Log("Gems got" + rewardAmount);

            SpinnerAddGems(rewardAmount);
            SaveCrystalCount();
            UpdateGemsTextUI();
        }
        if (rewardUnit == "Spinner")
        {
            spinnerCredits++;
            SaveSpinnerCount(spinnerCredits);

        }

        // var rewardsCount = PlayerPrefs.GetInt("MY_REWARD_KEY", 0);
        //  rewardsCount += myReward.reward;

        //  PlayerPrefs.SetInt("MY_REWARD_KEY", rewardsCount);
        //  PlayerPrefs.Save();
    }


    public void SaveRunningTime(float runningTime)
    {


        PlayerPrefs.SetFloat("RunningTime", runningTime); // pull from player prefs and set to 0 if not found
    }


    
    
}



