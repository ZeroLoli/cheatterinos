using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

/// <summary>
/// Sets the time slider label value
/// </summary>
public class SliderLabelValue : MonoBehaviour
{
    public Slider slider;
    public Text text;

    void Start()
    {
        slider.onValueChanged.AddListener(ChangeValue);
        text.text = (Math.Round(slider.value * 2, MidpointRounding.AwayFromZero) / 2).ToString();
    }

    void ChangeValue(float value)
    {
        text.text = (Math.Round(value*2, MidpointRounding.AwayFromZero)/2).ToString();
    }
}
