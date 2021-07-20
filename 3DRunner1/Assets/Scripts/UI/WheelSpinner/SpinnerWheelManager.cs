using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpinnerWheelManager : MonoBehaviour
{

    [SerializeField] Button closeButton;
    [SerializeField] Button openButton;
    [SerializeField] GameObject spinnerCanvas;
    [SerializeField] Image rewardImage;


    private void Start()
    {
        openButton.onClick.RemoveAllListeners();
        openButton.onClick.AddListener(OnOpenButtonClick_spinner);

        closeButton.onClick.RemoveAllListeners();
        closeButton.onClick.AddListener(OnCloseButtonClick_spinner);

    }

    void OnCloseButtonClick_spinner()
    {
        openButton.gameObject.SetActive(true);
        spinnerCanvas.SetActive(false);
       
    }
    void OnOpenButtonClick_spinner()
    {
        spinnerCanvas.SetActive(true);
        openButton.gameObject.SetActive(false);
    }


}
