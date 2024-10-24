using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public Slider healthSlider;

    // sets the sliders current health.
    public void SetSlider(float amount)
    {
        healthSlider.value = amount;
    }

    // sets the max amount of the slider.
    public void SetSliderMax(float amount)
    {
        healthSlider.maxValue = amount;
        SetSlider(amount);
    }

}