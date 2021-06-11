using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenu : MonoBehaviour
{
   
    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions
    public MainMenu theMainMenu;

    private void Start()
    {
        theGameManager = FindObjectOfType<GameManager>();
    }
    public void PlayGame()
    {
        theGameManager.isRunning = true;
        Debug.Log("Mainmenu set running true");
        theMainMenu.gameObject.SetActive(false);
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}
