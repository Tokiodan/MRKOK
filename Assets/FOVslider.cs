using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FOVslider : MonoBehaviour
{

    public UserPreference userPreference;
    public Camera mainCamera;

    

    private Slider fovSlider;

    void Start()
    {
        userPreference = Resources.Load<UserPreference>("UserPreference");
        fovSlider = GetComponent<Slider>();
        fovSlider.value = userPreference.FOV;


        fovSlider.onValueChanged.AddListener(UpdateFOV);
    }

    public void UpdateFOV(float value)
    {
        userPreference.FOV = value; 
    }
}