using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SenSlider : MonoBehaviour
{
  public float defaultSensitivity = 1f; 
    public Camera mainCamera; 

    private Slider sensitivitySlider;

    void Start()
    {
        sensitivitySlider = GetComponent<Slider>(); 
        sensitivitySlider.value = defaultSensitivity; 

        
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    public void UpdateSensitivity(float value)
    {
        // Here you would apply the sensitivity value to your camera or player controller
        // For example:
        // PlayerController.Instance.mouseSensitivity = value; // Example of applying to a player controller

        // If you're adjusting the camera's sensitivity directly, you could do something like:
        // This is just a placeholder for your actual implementation
        Debug.Log($"Mouse Sensitivity set to: {value}");
    }
}