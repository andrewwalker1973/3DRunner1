using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{

    private GameContinueManager theGameContinueManager;
    public string mainMenuLevel;

    private void Start()
    {
        theGameContinueManager = FindObjectOfType<GameContinueManager>();
    }

    public void ContinueGamewithCrystals()
    {
        theGameContinueManager.ContinueGameCrystals();

    }

    public void ContinueGamewithVideo()
    {
        theGameContinueManager.ContinueGameVideo();

    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;  // reset time after contunue screen
        SceneManager.LoadScene(mainMenuLevel);
    }
}
