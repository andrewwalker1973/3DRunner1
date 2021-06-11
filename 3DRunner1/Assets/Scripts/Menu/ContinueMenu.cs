using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueMenu : MonoBehaviour
{

    private GameManager theGameManager;
    public string mainMenuLevel;

    private void Start()
    {
        theGameManager = FindObjectOfType<GameManager>();
    }

    public void ContinueGame()
    {
        theGameManager.ContinueGame();

    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;  // reset time after contunue screen
        SceneManager.LoadScene(mainMenuLevel);
    }
}
