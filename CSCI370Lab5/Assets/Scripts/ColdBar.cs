using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMinCold(int cold)
    {
        slider.maxValue = cold;
        slider.value = cold;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetCold(float cold)
    {
        Debug.Log(cold);
        slider.value = cold;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ChangeCold(float cold)
    {
        slider.value = slider.value + cold;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
