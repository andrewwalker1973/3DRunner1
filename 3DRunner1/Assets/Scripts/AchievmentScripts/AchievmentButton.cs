using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievmentButton : MonoBehaviour
{
    public GameObject AchievmentList;
    public Sprite neutral, highlight;
    private Image sprite;

    private void Awake()
    {
        sprite = GetComponent<Image>();

    }
  
    public void Click()
    {
        if (sprite.sprite == neutral)
        {
            sprite.sprite = highlight;
            AchievmentList.SetActive(true);

        }
        else
        {
            sprite.sprite = neutral;
            AchievmentList.SetActive(false);
        }
    }
}
