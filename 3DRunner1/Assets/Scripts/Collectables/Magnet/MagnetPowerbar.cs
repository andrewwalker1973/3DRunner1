using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetPowerbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxMagentPower(int magnetpower)
    {
        slider.maxValue = magnetpower;
        slider.value = magnetpower;
    }
    public void SetMagnetPower(int magnetpower)
    {
        slider.value = magnetpower;
    }

}
