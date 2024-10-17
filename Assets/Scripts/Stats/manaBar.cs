using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetSlider(float mana)
    {
        slider.value = mana;  // Ensure this updates the slider's value correctly
    }

    public void SetSliderMax(float maxMana)
    {
        slider.maxValue = maxMana;  // Ensure the max value is set correctly
        slider.value = maxMana;     // Set the current value to the max initially
    }
}

