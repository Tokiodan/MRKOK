using UnityEngine;
using System.Collections;

public class TimeStop : Spell
{
    private bool timeStopped = false; 
    public float baseTimeStopDuration = 5.0f;   // Base duration for time stop
    public float durationIncrement = 1.0f;      // Duration increment per spell level
    private float timeStopEndTime = 0f;         // When the time stop will end

    // Store the original timeScale of the game
    private float originalTimeScale = 1.0f;

    // Reference to SpellManager to disable casting other spells during Time Stop
    private SpellManager spellManager;

    // Audio components
    public AudioClip timeStopAudioClip;         // Audio clip for TimeStop
    private AudioSource audioSource;            // AudioSource to play the audio

    void Start()
    {
        spellID = "TimeStop";
        spellManager = FindObjectOfType<SpellManager>();

        // Setup the AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = timeStopAudioClip;
    }

    public override void CastSpell(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        // If time is already stopped, don't allow another time stop spell
        if (timeStopped)
        {
            return;
        }

        // Play the time stop audio
        audioSource.Play();

        // Start the coroutine to handle time stop logic after the audio finishes
        StartCoroutine(WaitForAudioAndStopTime());
    }

    private IEnumerator WaitForAudioAndStopTime()
    {
        // Wait until the audio finishes playing
        yield return new WaitForSeconds(audioSource.clip.length);

        // Stop time after the audio has finished playing
        originalTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        timeStopped = true;

        // Calculate when the time stop will end, using the upgraded duration
        float timeStopDuration = baseTimeStopDuration + (durationIncrement * currentLevel);  // Scale duration with spell level
        timeStopEndTime = Time.realtimeSinceStartup + timeStopDuration;

        Debug.Log($"Time is stopped! Player can move freely for {timeStopDuration} seconds.");
    }

    void Update()
    {
        // If time is stopped, check if the duration has ended
        if (timeStopped && Time.realtimeSinceStartup >= timeStopEndTime)
        {
            ResumeTime();
        }
    }

    // Function to resume time
    private void ResumeTime()
    {
        Time.timeScale = originalTimeScale;
        timeStopped = false;

        Debug.Log("Time has resumed.");
        spellManager.ResumeTime();
    }

    // Public method to check if time is currently stopped
    public bool IsTimeStopped()
    {
        return timeStopped;
    }
}
