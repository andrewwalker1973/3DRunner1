using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;           //TextMesh Pro Text Field for score
    public TextMeshProUGUI hiScoreText;         //TextMesh Pro Text Field for Hiscore
    public TextMeshProUGUI coinScoreText;         //TextMesh Pro Text Field for Hiscore
    public TextMeshProUGUI crystalCountText;         //TextMesh Pro Text Field for Hiscore


    public float scoreCount;                    // What is the score count
    public float hiScoreCount;                  // what is the hi score
    public bool highScoreAchieved = false;
    private int coinScore;

    public float pointsPerSecond;               // how much to increase score by
    public bool scoreIncreasing;                // is score increasing ? dont want to increase while dead

    public bool shouldDouble;                   // if powerup double active;

    public int crystalCount;


    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))                // if highscore exists in playprefs
        {
            // pull from player prefs
            Debug.Log("Found player Pref");
            hiScoreCount = PlayerPrefs.GetFloat("HighScore", 0); // pull from player prefs and set to 0 if not found
        }
        if (PlayerPrefs.HasKey("Crystals"))                // if highscore exists in playprefs
        {
            // pull from player prefs
            Debug.Log("Found crystals Pref");
            crystalCount = PlayerPrefs.GetInt("Crystals", 0); // pull from player prefs and set to 0 if not found
        }

    }


    void Update()
    {
       
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;     // how much to increase by per second
        }
        

       // if (scoreCount > hiScoreCount)                      // if score > hghscore update highscore
      //  {
          //  hiScoreCount = scoreCount;                      // update highscore
            // PlayerPrefs.SetFloat("HighScore", hiScoreCount);            // AW save highscore to playerPrefs may not be the best place for it as this happens while player is running


      //  }

        scoreText.text = "Score : " + Mathf.Round (scoreCount);           // set the scorecout on screen rount to solid number
        hiScoreText.text = "HiScore : " + Mathf.Round(hiScoreCount);     // Set the hi score on screen
        coinScoreText.text = "Coins : " + Mathf.Round(coinScore);           // set the scorecout on screen rount to solid number
        crystalCountText.text = "Crystals : " + Mathf.Round(crystalCount);           // set the scorecout on screen rount to solid number

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
    }


    public void AddCrystals(int crystalsToAdd)
    {

        crystalCount += crystalsToAdd;      // Add coins 
        PlayerPrefs.SetInt("Crystals", crystalCount);            // AW save highscore to playerPrefs may not be the best place for it as this happens while player is runnin
                                                                    // AW need better place to put this. at end of run
    }


    public void AddEnemy(int enemyToAdd)
    {

        Debug.Log("Enemy increase" + enemyToAdd);
        scoreCount += enemyToAdd;      // Add score for killing enemy 
    }


    public void SaveHighScore()
    {
        
        if (scoreCount > hiScoreCount)                      // if score > hghscore update highscore
        {
            Debug.Log("Save high score");

            hiScoreCount = scoreCount;                      // update highscor
            highScoreAchieved = true;
            PlayerPrefs.SetFloat("HighScore", hiScoreCount);            // AW save highscore to playerPrefs may not be the best place for it as this happens while player is runnin

        }
    }
}
