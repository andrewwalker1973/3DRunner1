using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DoubleModePowerBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxDoublePower(int doublepower)
    {
        slider.maxValue = doublepower;
        slider.value = doublepower;
    }
    public void SetDoublePower(int doublepower)
    {

        slider.value = doublepower;
    }
}
