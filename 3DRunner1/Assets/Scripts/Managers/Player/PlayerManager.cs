using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public ShopUpgradeSystem.ShopUI theShopUI;
    private int Index;
    GameObject Player_prefab;
    GameObject Player_temp;
    public GameObject PLayer_parent;
   public PlayerMotor thePlayerMotor;
    public OnCollision theonCollision;

    public GameObject[] charList;


    // Start is called before the first frame update
    void Start()
    {
        theShopUI = FindObjectOfType<ShopUpgradeSystem.ShopUI>();
        thePlayerMotor = FindObjectOfType<PlayerMotor>();






        loadCurrentPlayer();
        
      



    }

    // Update is called once per frame
    void Update()
    {
       /* if (theShopUI.selectionUpdated)
        {
            // existing prefab
          Player_prefab = GameObject.Find("PLAYER1");
          
            Debug.Log("Player pfreb" + Player_prefab);
            Destroy(Player_prefab);
            loadCurrentPlayer();
            theShopUI.selectionUpdated = false;
        }
        */
    }

    public void ChangeSelectedPlayer()
    {
        if (theShopUI.selectionUpdated)
        {
            // existing prefab
            Player_prefab = GameObject.Find("PLAYER1");

            Debug.Log("Player pfreb" + Player_prefab);
            Destroy(Player_prefab);
            loadCurrentPlayer();
            theShopUI.selectionUpdated = false;
        }
    }
    public void loadCurrentPlayer()
    {
       

        if (PlayerPrefs.HasKey("SelectedItem"))
        {
            Debug.Log("The key  exists");
            Index = PlayerPrefs.GetInt("SelectedItem", 0);  //get the selectedIndex from PlayerPrefs
                                                            // Player_prefab = theShopUI.carList[Index];
            Player_prefab = charList[Index];

            Player_temp = Instantiate(Player_prefab, new Vector3(1, 1, 1), Quaternion.identity);
            //  Player_temp.transform.parent = PLayer_parent.transform;
            //  Player_temp.transform.position = new Vector3(0, 0, 0);
            Player_temp.name = "PLAYER1";
            Player_temp.SetActive(true);
            theonCollision.UpdateCharCollider();
            //thePlayerMotor.ResetAnimator();

        }
        else
            Debug.Log("The key  NOT exists");
        Debug.Log("Set defaut char");
        Player_prefab = charList[1];  // Set a default character
        Player_temp = Instantiate(Player_prefab, new Vector3(1, 1, 1), Quaternion.identity);
        //  Player_temp.transform.parent = PLayer_parent.transform;
        //  Player_temp.transform.position = new Vector3(0, 0, 0);
        Player_temp.name = "PLAYER1";
        Player_temp.SetActive(true);
        theonCollision.UpdateCharCollider();
        //thePlayerMotor.ResetAnimator();

    }
}
