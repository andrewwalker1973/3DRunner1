using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;


public class SceneLoader : MonoBehaviour
{

    public AssetReference shopScene;
    private string k_ShopSceneName = "ShopUI";
    private AsyncOperationHandle<SceneInstance> handle;
    public bool unloaded = true;

    // Start is called before the first frame update
    void Start()
    {
        // load shop scene from addressables
      //  Addressables.LoadSceneAsync(shopScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) )
        {
            unloaded = true;
            UnloadShopScene();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            Debug.Log("Y pressed");
            unloaded = false;
           LoadShopScene();
        }
    }

    public void UnloadShopScene1()
    {
        unloaded = true;
        UnloadShopScene();
    }
    public void SceneLoadComplete(AsyncOperationHandle<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log(obj.Result.Scene.name + " Successfully Loaded");
            handle = obj;

        }

    }

    public void UnloadShopScene()
    {
        Debug.Log("Handle " + handle);
        Addressables.UnloadSceneAsync(handle, true).Completed += op =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Successfully unloaded Shop Scene");
            }
        };
    }

    public void LoadShopScene()
    {
        //  Addressables.LoadSceneAsync(shopScene, UnityEngine.SceneManagement.LoadSceneMode.Additive).Completed += SceneLoadComplete;

        UnityEngine.SceneManagement.SceneManager.LoadScene(k_ShopSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
