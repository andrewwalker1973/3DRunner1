using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HiScoreMenu : MonoBehaviour
{
    public string mainMenuLevel;

    public void RestartGame()
    {
      //  FindObjectOfType<GameManager>().ResetToBegining();
    }

    public void QuitToMain()
    {
        SceneManager.LoadScene(mainMenuLevel);
    }
}
