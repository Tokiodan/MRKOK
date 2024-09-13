using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


[CreateAssetMenu(menuName = "Audio/Sound Manager", fileName = "Sound Manager")]
public class AudioManagerSO : ScriptableObject
{
    // This is making the object sort of exist inside our programming world
    private static AudioManagerSO instance;
    public static AudioManagerSO Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<AudioManagerSO>("Sound Manager");
            }
            return instance;
        }
    }
    public AudioSource SoundObject;

    // Here a list of sounds can be added in the soundManagerSO with the names for the sounds and their clip.
    public Sound[] Sounds;

    // this will play a SFX clip and return the audiosource if you want to do anything with it.
    public static AudioSource PlaySoundSFXClip(string name, Vector3 soundPos, float volume)
    {
        Sound s = Array.Find(Instance.Sounds, x => x.name == name);
        AudioSource a = Instantiate(Instance.SoundObject, soundPos, quaternion.identity);

        a.clip = s.clip;
        a.volume = volume;
        a.Play();
        return a;
    }


    // this just lets you loop music
    public static AudioSource PlaySFXLoop(string name, Vector3 soundPos, float volume)
    {
        Sound s = Array.Find(Instance.Sounds, x => x.name == name);
        AudioSource a = Instantiate(Instance.SoundObject, soundPos, quaternion.identity);
        // a.GetComponent<SoundDestroyer>().enabled = false;
        a.clip = s.clip;
        a.volume = volume;
        a.Play();
        return a;
    }
}

