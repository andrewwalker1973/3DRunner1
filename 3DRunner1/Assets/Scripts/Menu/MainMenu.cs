using UnityEngine;
using UnityEngine.UI;



public class MainMenu : MonoBehaviour
{
   
    public GameManager theGameManager;                              // Reference the GameManager script to call fucntions
    public MainMenu theMainMenu;
    private ScoreManager theScoreManager;       // reference the score manager
    public GameObject inGameUIScreen;
    public Button shopButton;
   
    public Camera mainCamera;
    public Camera shopCamera;

   // private string ShopSceneName = "ShopUI";


    private void Start()
    {
        theGameManager = FindObjectOfType<GameManager>();
        theScoreManager = FindObjectOfType<ScoreManager>();         // find score manager script


 






    }
    public void PlayGame()
    {
        theGameManager.isRunning = true;
        theMainMenu.gameObject.SetActive(false);
        theScoreManager.LoadPlayervalues();
        inGameUIScreen.SetActive(true);
    }


    public void QuitGame()
    {
        Application.Quit();
    }


    public void OpenShopButton()
    {
        mainCamera.gameObject.SetActive(false);
        shopCamera.gameObject.SetActive(true);
    }


}
