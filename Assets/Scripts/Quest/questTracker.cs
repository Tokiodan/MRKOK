using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestTracker : MonoBehaviour
{
    private Quest currentQuest; // The quest currently being tracked
    public TMP_Text questText;  // Reference to the TextMeshPro UI element for displaying the quest
    private QuestManager questManager; // Reference to the QuestManager

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>(); // Find the QuestManager in the scene
        questText.text = "No quest is currently being tracked.";
    }

    public void TrackQuest(Quest quest)
    {
        currentQuest = quest; // Set the current quest
        UpdateQuestDisplay(); // Update the display whenever a quest is tracked
        Debug.Log($"Tracking quest: {currentQuest?.questName}");
    }

    private void UpdateQuestDisplay()
    {
        if (questText != null)
        {
            if (currentQuest != null)
            {
                questText.text = $"Current Quest: {currentQuest.questName} - {currentQuest.description}";
            }
            else
            {
                questText.text = "No quest is currently being tracked.";
            }
        }
        else
        {
            Debug.LogWarning("Quest TextMeshPro reference is not assigned.");
        }
    }

    public void CheckForCompletedQuest()
    {
        if (currentQuest != null && currentQuest.isCompleted)
        {
            // Clear the current quest from the tracker
            currentQuest = null;
            UpdateQuestDisplay(); // Update the UI to reflect no current quest

            // Get a random quest from the QuestManager
            Quest newQuest = questManager.GetRandomQuest();
            if (newQuest != null)
            {
                TrackQuest(newQuest); // Track the new random quest
            }
        }
    }
}
