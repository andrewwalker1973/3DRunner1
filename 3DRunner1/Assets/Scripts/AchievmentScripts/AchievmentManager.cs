using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class AchievmentManager : MonoBehaviour
{

    public GameObject achievmentPrefab;
    public Sprite[] sprites;
    public AchievmentButton activeButton;
    public ScrollRect scrollRect;
    public GameObject achievmentMenu;

    public GameObject visualAchievment;

    private int fadeTime = 2;

    
    
    public Sprite unlockedSprite;
    public Sprite lockedSprite;


    private static AchievmentManager instance;  //access all info in class
    private ScoreManager theScoreManager;       // reference the score manager

    public static AchievmentManager Instance
    {
        
        get {
            if (instance == null)
                {
                    instance = GameObject.FindObjectOfType<AchievmentManager>();
                 }
            return AchievmentManager.instance; 
        }
       
    }
    public Dictionary<string, Achievment> achievments = new Dictionary<string, Achievment>();

    [SerializeField] Button openButton;
    [SerializeField] Button closeButton;



    void Start()
    {
       


        activeButton = GameObject.Find("General_Button").GetComponent<AchievmentButton>();
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script

        // string parent, string title, string description,int points, int sprintIndex, int progress, string[] dependencies = null
        CreateAchievment("General","Beat High Score", "Beat High Score", 5,1,0); //
        CreateAchievment("General", "Beat High Score 10", "Beat High Score 10 times to unlock", 5, 0, 10); //

        CreateAchievment("General", "7 Daily Logins", "Login in 7 days to unlock", 5, 0,7);
        CreateAchievment("General", "30 Daily Logins", "Login in 30 days to unlock", 15, 0, 30);
        CreateAchievment("General", "60 Daily Logins", "Login in 60 days to unlock", 20, 0, 60);
        
        CreateAchievment("General", "Use 30 Score Boosters", "Use 30 Score Boosters to unlock", 15, 0, 30);
        CreateAchievment("General", "Use 20 Score Boosters", "Use 20 Score Boosters to unlock", 10, 0, 20);
        CreateAchievment("General", "Use 10 Score Boosters", "Use 10 Score Boosters to unlock", 5, 0, 20);

        CreateAchievment("General", "1000000 points in one run", "1000000 points in one run", 5, 0, 0);
        CreateAchievment("General", "500000 points in one run", "500000 points in one run", 5, 0, 0);
        CreateAchievment("General", "100000 points in one run", "100000 points in one run", 2, 0, 0);

        CreateAchievment("General", "Run 72000 Secs", "Run 72000 Secs", 5, 0, 0); //
        CreateAchievment("General", "Run 18000 Secs", "Run 18000 Secs", 2, 0, 0); //
        CreateAchievment("General", "Run 3600 Secs", "Run 3600 Secs", 2, 0, 0); // 

        CreateAchievment("General", "Collect 100000 Coins", "Collect 100000 Coins", 10, 0, 0); //
        CreateAchievment("General", "Collect 50000 Coins", "Collect 50000 Coins", 5, 0, 0); // 
        CreateAchievment("General", "Collect 5000 Coins", "Collect 5000 coins to unlock", 5, 0, 0); // 





        foreach (GameObject achievmentList in GameObject.FindGameObjectsWithTag("AchievmentList"))
        {
            achievmentList.SetActive(false);
        }

        activeButton.Click();
        achievmentMenu.SetActive(false);

        //Add Click Events
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(OnOpenAchievmentButtonClick);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnCloseAchievmentButtonClick);
     
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.I))
        {
            achievmentMenu.SetActive(!achievmentMenu.activeSelf);
        }
       */

        if (Input.GetKeyDown(KeyCode.W))
        {
            EarnAchievment("Press W");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            EarnAchievment("Press S");
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EarnAchievment("Press A");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            EarnAchievment("Press L");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            EarnAchievment("Press M");
        }
    }

    public void EarnAchievment(string title)
    {
        if (achievments[title].EarnAchievment())
        {
            // earn achievment
            GameObject achievment = (GameObject)Instantiate(visualAchievment);
            SetAchievmentInfo("EarnAchievment", achievment, title);


            StartCoroutine(FadeAchievment(achievment));
           string pointsToAllocate =  achievment.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = achievments[title].Points.ToString();
           int pointsToSave = System.Convert.ToInt32(pointsToAllocate);  // convert from string to int
            // int gemPoints = PlayerPrefs.GetInt("Crystals");  //get current saved crystals
            //   PlayerPrefs.SetInt("Crystals", gemPoints += new2);
            theScoreManager.SpinnerAddGems(pointsToSave);
            theScoreManager.SaveCrystalCount();
            theScoreManager.UpdateGemsTextUI();





        }
    }

    public IEnumerator HideAchievment(GameObject achievment)
    {
        yield return new WaitForSeconds(3);
        Destroy(achievment);
    }
    public void CreateAchievment(string parent, string title, string description,int points, int sprintIndex, int progress, string[] dependencies = null)
    {
        GameObject achievment = (GameObject)Instantiate(achievmentPrefab);

        Achievment newAchievment = new Achievment(title, description, points, sprintIndex, achievment, progress);  // add to disctionary
        achievments.Add(title, newAchievment);
        SetAchievmentInfo(parent, achievment, title,progress);

        if (dependencies != null )
        {
            foreach (string achievmentTitle in dependencies)
            {
                Achievment dependancy = achievments[achievmentTitle];
                dependancy.Child = title;
                newAchievment.AddDependancy(dependancy);

                // dependacy = press space <- child = press w
                //new achievment  press w --> press space
            }
        }
    }

    public void SetAchievmentInfo(string parent, GameObject achievment, string title,int progression= 0)
    {
        achievment.transform.SetParent(GameObject.Find(parent).transform);
        achievment.transform.localScale = new Vector3(1, 1, 1);
         string progress = progression > 0 ? " " + PlayerPrefs.GetInt("Progression" + title) + "/" + progression.ToString() : string.Empty;  

        int CurrentProgression = PlayerPrefs.GetInt("Progression" + title);

        if (progression > 0 )
        {
            // ectvate slider
            achievment.transform.GetChild(5).GetComponent<Slider>().gameObject.SetActive(true);
            achievment.transform.GetChild(5).GetComponent<Slider>().maxValue = progression;
            achievment.transform.GetChild(5).GetComponent<Slider>().value = CurrentProgression;


        }
      //  else if (progression == 0)
     //   {

    //      achievment.transform.GetChild(4).GetComponent<Slider>().gameObject.SetActive(false);

     //   }

         achievment.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = title + progress;
        achievment.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = achievments[title].Description;
        achievment.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = achievments[title].Points.ToString();
        achievment.transform.GetChild(4).GetComponent<Image>().sprite = sprites[achievments[title].SpriteIndex];
        
        
    }

    public void ChangeCategory(GameObject button)
    {
        AchievmentButton achievmentButton = button.GetComponent<AchievmentButton>();
        scrollRect.content = achievmentButton.AchievmentList.GetComponent<RectTransform>();
        achievmentButton.Click();
        activeButton.Click();
        activeButton = achievmentButton;
    }

    //Open | Close UI -------------------------------------------------------
    void OnOpenAchievmentButtonClick()
    {
        achievmentMenu.SetActive(!achievmentMenu.activeSelf);
    }

    void OnCloseAchievmentButtonClick()
    {
        Debug.Log("Close Menu");
        achievmentMenu.SetActive(!achievmentMenu.activeSelf);
    }

    private IEnumerator FadeAchievment(GameObject achievment)
    {
        CanvasGroup canvasGroup = achievment.GetComponent<CanvasGroup>();

        float rate = 1.0f / fadeTime;
        int startAlpha = 0;
        int endAlpha = 1;

        

        for (int i = 0; i < 2; i++)
        {
            float progress = 0.0f;
            while (progress < 1.0)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, progress);
                progress += rate * Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(2);
            startAlpha = 1;
            endAlpha = 0;
        }

        Destroy(achievment);
        
        



    }

    
}
