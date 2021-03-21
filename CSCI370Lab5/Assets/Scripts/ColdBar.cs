using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColdBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int cold)
    {
        slider.maxValue = cold;
        slider.value = cold;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int cold)
    {
        slider.value = cold;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
