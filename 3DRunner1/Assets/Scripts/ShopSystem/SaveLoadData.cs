using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShopUpgradeSystem
{

    public class SaveLoadData : MonoBehaviour
    {


        [SerializeField] private ShopUI shopUI;
        private bool canSave = false;
        //Method to initialize the SaveLoad Script
        public void Initialize()
        {
            //ClearData();
           // if (PlayerPrefs.GetInt("GameStartFirstTime") == 1)  //if PlayerPrefs of "GameStartFirstTime" value is 1, means we are playing the game again
          //  {
         //       LoadData();                                     //so we load the data
         //       LoadData_prefs();
         //   }
         //   else                                                //if its not 1, means we are playing the game 1st time
        //    {
                SaveData();                                     //save the data 1st

                string shopDataString = JsonUtility.ToJson(shopUI.shopData);
                System.IO.File.WriteAllText(Application.persistentDataPath + "/ShopData.json", shopDataString);

                PlayerPrefs.SetInt("GameStartFirstTime", 1);    //save the PlayerPrefs
                LoadData();
                LoadData_prefs();
          //  }
            canSave = true;
        }

        //this is Unity method which is called when game is crashed or in background or quit
        private void OnApplicationPause(bool pause)
        {
#if !UNITY_EDITOR
            if(canSave)
            {
                SaveData();
            }
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR
            //needed only in editor
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveData();
                SaveData_Prefs();
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                ClearData();
            }
#endif
        }

        /// <summary>
        /// Method used to save the data
        /// </summary>
        public void SaveData()
        {
            //convert the data to string
         /*   for (int i = 0; i < shopUI.carList.Length; i++)
            {
                Debug.Log(" Car nane " + shopUI.shopData.shopItems[i].carName);
                string CarName = shopUI.shopData.shopItems[i].carName;
                bool CarNameLocked = shopUI.shopData.shopItems[i].isUnlocked;
                //  PlayerPrefs.SetString(CarName, CarName);
                if (shopUI.shopData.shopItems[i].isUnlocked == false)
                {
                    Debug.Log("saveing 0  true");
                    PlayerPrefs.SetInt(CarName, 0);
                }
                else
                {
                    Debug.Log("Setting 1 false");
                    PlayerPrefs.SetInt(CarName, 1);
                }


            }
         */


            Debug.Log(" Car nane locked " + shopUI.shopData.shopItems[0].isUnlocked);


            // string shopDataString = JsonUtility.ToJson(shopUI.shopData);
            //  Debug.Log("Save:" + shopDataString);

            try
            {
                //save the string as json 
                //  System.IO.File.WriteAllText(Application.persistentDataPath + "/ShopData.json", shopDataString);
                Debug.Log("Data Saved");

            }
            catch (System.Exception e)
            {
                //if we get any error debug it
                Debug.Log("Error Saving Data:" + e);
                throw;
            }
        }

        public void SaveData_Prefs()
        {
            for (int i = 0; i < shopUI.carList.Length; i++)
            {
                Debug.Log(" Car nane " + shopUI.shopData.shopItems[i].carName);
                string CarName = shopUI.shopData.shopItems[i].carName;
                bool CarNameLocked = shopUI.shopData.shopItems[i].isUnlocked;
                //  PlayerPrefs.SetString(CarName, CarName);
                if (shopUI.shopData.shopItems[i].isUnlocked == false)
                {
                    Debug.Log("saveing 0  true");
                    PlayerPrefs.SetInt(CarName + "status", 0);
                }
                else
                {
                    Debug.Log("Setting 1 false");
                    PlayerPrefs.SetInt(CarName + "status", 1);
                }


            }
        }

        //Method used to load the data
        private void LoadData()
        {
            try
            {
                //get the text data from json and stro it in string
                string shopDataString = System.IO.File.ReadAllText(Application.persistentDataPath + "/ShopData.json");
                // Debug.Log("Load:" + shopDataString);
                shopUI.shopData = new shopDataString();
                //AW
                //  string shopDataString1 = JsonUtility.ToJson(shopUI.shopData);
                //  Debug.Log("Newly created" + shopDataString1);
                shopUI.shopData = JsonUtility.FromJson<shopDataString>(shopDataString); //create ShopData from json
                                                                                        // Debug.Log("Loaded Data " + shopDataString);

                Debug.Log("Data Loaded");


                /*for (int i = 0; i < shopUI.carList.Length; i++)
                {
                    Debug.Log(" Car nane load " + shopUI.shopData.shopItems[i].carName);
                    string CarName = shopUI.shopData.shopItems[i].carName;
                    bool CarNameLocked = shopUI.shopData.shopItems[i].isUnlocked;
                    //  PlayerPrefs.SetString(CarName, CarName);
                    
                        int carunlocked = PlayerPrefs.GetInt(CarName, 0);
                    Debug.Log("Car " + carunlocked);
                    if (carunlocked == 1)
                    {
                        Debug.Log("Setting true");
                        shopUI.shopData.shopItems[i].isUnlocked = true;
                    }
                    else
                    {
                        Debug.Log("Setting false");
                        shopUI.shopData.shopItems[i].isUnlocked = false;
                    }

               */


                //  }
            }
            catch (System.Exception e)
            {
                Debug.Log("Error Loading Data:" + e);
                throw;
            }

        }

        /// <summary>
        /// Method to clear all the save data
        /// </summary>
        public void ClearData()
        {
            Debug.Log("Data Cleared");
            PlayerPrefs.SetInt("GameStartFirstTime", 0);
        }


        private void LoadData_prefs()
        {
            
            
            for (int i = 0; i < shopUI.carList.Length; i++)
            {
                Debug.Log(" Car nane load " + shopUI.shopData.shopItems[i].carName);
                string CarName = shopUI.shopData.shopItems[i].carName;
                bool CarNameLocked = shopUI.shopData.shopItems[i].isUnlocked;
                //  PlayerPrefs.SetString(CarName, CarName);

                int carunlocked = PlayerPrefs.GetInt(CarName + "status", 0);
                Debug.Log("Car " + carunlocked);
                if (carunlocked == 1)
                {
                    Debug.Log("Setting true");
                    shopUI.shopData.shopItems[i].isUnlocked = true;
                }
                else
                {
                    Debug.Log("Setting false");
                    shopUI.shopData.shopItems[i].isUnlocked = false;
                }
            }
        }
    }
}
