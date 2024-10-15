
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class VolumeSettings : MonoBehaviour
{
    public UserPreference userPreference = Resources.Load<UserPreference>("UserPreference");
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        userPreference.volume = volume;
    }
}


