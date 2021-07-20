using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
   
    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions
    public MainMenu theMainMenu;
    private ScoreManager theScoreManager;       // reference the score manager
    public GameObject inGameUIScreen;

    private void Start()
    {
        theGameManager = FindObjectOfType<GameManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script
    }
    public void PlayGame()
    {
        theGameManager.isRunning = true;
        Debug.Log("Mainmenu set running true");
        theMainMenu.gameObject.SetActive(false);
        theScoreManager.LoadPlayervalues();
        inGameUIScreen.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
