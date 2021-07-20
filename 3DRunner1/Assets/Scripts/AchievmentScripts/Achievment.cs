using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Achievment
{
    private ScoreManager theScoreManager;       // reference the score manager

    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
    private string description;
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

    private bool unlocked;
    public bool Unlocked
    {
        get { return unlocked; }
        set { unlocked = value; }
    }

    private int points;
    public int Points
    {
        get { return points; }
        set { points = value; }
    }
    private int spriteIndex;
    public int SpriteIndex
    {
        get { return spriteIndex; }
        set { spriteIndex = value; }
    }

    private GameObject achievmentRef;

    private List<Achievment> dependencies = new List<Achievment>();

    private string child;
    public string Child
    {
        get { return child; }
        set { child = value; }
    }


    private int currentProgression;
    private int maxProgression;
    public Achievment(string name, string description, int points, int spriteIndex, GameObject achievmentRef, int maxProgression)
    {
        this.name = name;   //catcht the defined name not the one in the acheivement fucntin
        this.description = description;
        this.unlocked = false;
        this.points = points;
        this.spriteIndex = spriteIndex;
        this.achievmentRef = achievmentRef;
        this.maxProgression = maxProgression;
        LoadAchievment();
    }

   
    public void AddDependancy(Achievment dependancy)
    {
        dependencies.Add(dependancy);
    }
    public bool EarnAchievment()
    {
        if (!unlocked && !dependencies.Exists(x => x.unlocked == false) && CheckProgress()) // checks all dependencies
        {

            achievmentRef.GetComponent<Image>().sprite = AchievmentManager.Instance.unlockedSprite;


            // unlocked = true;
            SaveAchievment(true);

            if (child !=null)
            {
                AchievmentManager.Instance.EarnAchievment(child);
            }
            return true;
        }
        return false;

    }


    public void SaveAchievment(bool value)
    {
        unlocked = value;
          int tmpPoints = PlayerPrefs.GetInt("Points");  // modify for my own stuff
      

        PlayerPrefs.SetInt(name, value ? 1 : 0);  // save locked or unlocked false = 0

          PlayerPrefs.SetInt("Progression" + Name, currentProgression);
       

        PlayerPrefs.Save();
    }

    public void LoadAchievment()
    {
        unlocked = PlayerPrefs.GetInt(name) == 1 ? true : false;  // load all tru false unlock from player prefs

      //  unlocked = PlayerPrefs.GetInt(description) == 1 ? true : false;
        if (unlocked)
        {
           // AchievmentManager.Instance.textPoints.text = "Points: " + PlayerPrefs.GetInt("Points");
          currentProgression = PlayerPrefs.GetInt("Progression" + Name);


           // Debug.Log("CurrentPr " + currentProgression + description);
            achievmentRef.GetComponent<Image>().sprite = AchievmentManager.Instance.unlockedSprite;

           achievmentRef.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = description + "aa  " + currentProgression + "/" + maxProgression;

        }
        else
        {

            currentProgression = PlayerPrefs.GetInt("Progression" + Name);


        }
       
    }

    public bool CheckProgress()
    {

        currentProgression++;

        achievmentRef.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Name + " " + currentProgression + "/" + maxProgression;


        // New slider code
        if (maxProgression > 0)
        {
            // ectvate slider

            achievmentRef.transform.GetChild(5).GetComponent<Slider>().maxValue = maxProgression;
            achievmentRef.transform.GetChild(5).GetComponent<Slider>().value = currentProgression;


        }

        SaveAchievment(false);
        if (maxProgression == 0)
        {
            return true;
        }
        if (currentProgression >= maxProgression)
        {
            return true;
        }
        return false;
    }
}
