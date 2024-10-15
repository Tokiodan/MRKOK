using System;
using UnityEngine;

public class LevelUpEffectManager : MonoBehaviour
{
    [Header("Particle Effect")]
    [Tooltip("Particle effect to play when the player levels up.")]
    [SerializeField] private ParticleSystem levelUpParticleEffect;

    [Header("Player")]
    [Tooltip("Reference to the player object.")]
    [SerializeField] private Transform playerTransform;

    [Header("Audio")]
    [Tooltip("Sound to play when the player levels up.")]
    [SerializeField] private AudioClip levelUpSound;

    [Tooltip("AudioSource to play the level-up sound.")]
    [SerializeField] private AudioSource audioSource;

    [Header("Button Click Sound")]
    [Tooltip("Sound to play when a button is clicked.")]
    [SerializeField] private AudioClip buttonClickSound;

    [Tooltip("AudioSource to play the button click sound.")]
    [SerializeField] private AudioSource buttonAudioSource;

    private ExperienceManager expManager; // Reference to the ExperienceManager

    void Start()
    {
        // Find the ExperienceManager component
        expManager = FindObjectOfType<ExperienceManager>();

        if (expManager != null)
        {
            // Subscribe to the OnLevelUp event
            expManager.OnLevelUp += PlayLevelUpEffect;
        }
        else
        {
            Debug.LogError("ExperienceManager not found in the scene.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to avoid memory leaks
        if (expManager != null)
        {
            expManager.OnLevelUp -= PlayLevelUpEffect;
        }
    }

    private void PlayLevelUpEffect(int newLevel)
    {
        // Play particle effect if assigned
        if (levelUpParticleEffect != null && playerTransform != null)
        {
            // Spawn position: slightly below the player's feet (adjust Y value for fine-tuning)
            Vector3 spawnPosition = playerTransform.position + new Vector3(0f, -1f, 0f);

            // Set the rotation to ensure it faces upward (0 on X, 0 on Z, up on Y), with a -90f adjustment
            Quaternion groundRotation = Quaternion.Euler(-90f, 0f, 0f);

            // Instantiate the particle system at the position below the player with the adjusted rotation
            ParticleSystem particleInstance = Instantiate(levelUpParticleEffect, spawnPosition, groundRotation);

            // Make the particle system follow the player
            particleInstance.transform.SetParent(playerTransform);

            // Enable unscaled time to make the particle system ignore Time.timeScale
            var mainModule = particleInstance.main;
            mainModule.useUnscaledTime = true;

            particleInstance.Play();

            // Destroy the particle system after 6 seconds (real-time)
            Destroy(particleInstance.gameObject, 6f);
        }
        else
        {
            Debug.LogError("Level up particle effect or player transform is not assigned.");
        }

        // Play the sound when leveling up if the sound and audio source are assigned
        if (levelUpSound != null && audioSource != null)
        {
            // Play level-up sound using PlayOneShot so that it doesn't interfere with other sounds
            audioSource.PlayOneShot(levelUpSound);
        }
        else
        {
            Debug.LogError("Level up sound or audio source is not assigned.");
        }
    }

    // Method to play button click sound
    public void PlayButtonClickSound()
    {
        if (buttonClickSound != null && buttonAudioSource != null)
        {
            // Play button click sound using PlayOneShot
            buttonAudioSource.PlayOneShot(buttonClickSound);
        }
        else
        {
            Debug.LogError("Button click sound or audio source is not assigned.");
        }
    }
}
