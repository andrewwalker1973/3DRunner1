using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SafeModePowerbar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxSafePower(int safepower)
    {
        slider.maxValue = safepower;
        slider.value = safepower;
    }
    public void SetSafePower(int safepower)
    {

        slider.value = safepower;
    }
}
