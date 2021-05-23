using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEnable : MonoBehaviour
{
    private void OnEnable()
    {
        gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
