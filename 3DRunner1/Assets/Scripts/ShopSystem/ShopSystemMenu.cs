using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

public class ShopSystemMenu : MonoBehaviour
{

    public MainMenu theMainMenu;


    // Start is called before the first frame update
    void Start()
    {
        theMainMenu = FindObjectOfType<MainMenu>();


        
    }

    public void ExitShopMenu()
    {
        SceneManager.UnloadSceneAsync("ShopUI");
        
    }
}
