using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FOVslider : MonoBehaviour
{

    public UserPreference userPreference = Resources.Load<UserPreference>("UserPreference");
   public Camera mainCamera; 

    private Slider fovSlider;

    void Start()
    {
        fovSlider = GetComponent<Slider>(); 
        fovSlider.value = userPreference.FOV; 

    
        fovSlider.onValueChanged.AddListener(UpdateFOV);
    }

    public void UpdateFOV(float value)
    {
        mainCamera.fieldOfView = value;
        userPreference.FOV = value;
    }
}