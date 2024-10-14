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

    private ExperienceManager experienceManager;

    void Start()
    {
        // Find the ExperienceManager component
        experienceManager = FindObjectOfType<ExperienceManager>();

        if (experienceManager != null)
        {
            // Subscribe to the OnLevelUp event
            experienceManager.OnLevelUp += PlayLevelUpEffect;
        }
        else
        {
            Debug.LogError("ExperienceManager not found in the scene.");
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from the event when the object is destroyed to avoid memory leaks
        if (experienceManager != null)
        {
            experienceManager.OnLevelUp -= PlayLevelUpEffect;
        }
    }

    private void PlayLevelUpEffect(int newLevel)
    {
        // Play particle effect if assigned
        if (levelUpParticleEffect != null && playerTransform != null)
        {
            // Spawn position: slightly below the player's feet (adjust Y value for fine-tuning)
            Vector3 spawnPosition = playerTransform.position + new Vector3(0f, -1f, 0f); // Adjust -1f for how far below the player it should be

            // Set the rotation to ensure it faces upward (0 on X, 0 on Z, up on Y), with a -90f adjustment
            Quaternion groundRotation = Quaternion.Euler(-90f, 0f, 0f); // Adjust rotation to face upward from the ground

            // Instantiate the particle system at the position below the player with the adjusted rotation
            ParticleSystem particleInstance = Instantiate(levelUpParticleEffect, spawnPosition, groundRotation);
            particleInstance.Play();

            // Destroy the particle system after 4 seconds
            Destroy(particleInstance.gameObject, 4f);
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
}
