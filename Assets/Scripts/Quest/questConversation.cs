using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestConversation : MonoBehaviour
{
    public GameObject questConversationObject;  // UI GameObject for conversation
    public TMP_Text textArea;  // TextMeshPro text area for the conversation
    private bool isPlayerInRange = false;
    private QuestIconCreator questIconCreator;  // Reference to the QuestIconCreator of the civilian
    private bool accepted = false;  // Track if the quest is accepted
    private QuestManager questManager; // Reference to the QuestManager
    private QuestTracker questTracker; // Reference to the QuestTracker
    private Quest currentQuest;  // Store the current quest for this conversation

    void Start()
    {
        questManager = FindObjectOfType<QuestManager>(); // Find the QuestManager in the scene
        questTracker = FindObjectOfType<QuestTracker>(); // Find the QuestTracker in the scene

        if (questConversationObject != null)
        {
            questConversationObject.SetActive(false); // Ensure quest conversation is initially inactive
        }
        else
        {
            Debug.LogError("questConversationObject is not assigned in the inspector!");
        }
    }

    void Update()
    {
        // Check if the player is in range and presses 'X', but only if the quest is not accepted
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.X) && !accepted)
        {
            ToggleQuestConversation(true);
            textArea.text = "Do you accept the quest?";  // Ask the player if they accept the quest
            Debug.Log("Quest conversation UI activated");
        }

        // Check for accept or decline inputs
        if (isPlayerInRange && questConversationObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Y)) // Accept the quest
            {
                AcceptQuest();
            }
            else if (Input.GetKeyDown(KeyCode.N)) // Decline the quest
            {
                DeclineQuest();
            }
        }
    }

    // Call this method when the accept key is pressed
    public void AcceptQuest()
    {
        accepted = true;  // Set accepted to true
        Debug.Log("Quest Accepted");

        if (questIconCreator != null)
        {
            questIconCreator.DestroyQuestIcon();  // Destroy the quest icon if accepted
        }

        // Track the quest in QuestTracker
        if (questManager != null && currentQuest != null)
        {
            questManager.AcceptQuest(currentQuest.questName); // Accept the quest
            questTracker.TrackQuest(currentQuest); // Track the accepted quest
        }

        ToggleQuestConversation(false);  // Close the conversation UI
    }

    // Call this method when the decline key is pressed
    public void DeclineQuest()
    {
        accepted = false;  // Set accepted to false
        Debug.Log("Quest Declined");
        ToggleQuestConversation(false);  // Close the conversation UI
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Ensure the tag is "Player"
        {
            isPlayerInRange = true;
            Debug.Log("Player is in range");

            // Find the QuestIconCreator attached to the civilian the player interacts with
            questIconCreator = GetComponent<QuestIconCreator>();

            if (questIconCreator == null)
            {
                Debug.LogWarning("No QuestIconCreator found on " + gameObject.name);
            }

            // Assign a quest based on the tag of this quest giver
            if (gameObject.CompareTag("QuestGiver1"))
            {
                currentQuest = questManager.GetQuest("Defeat the evil Djinn"); // Example quest for QuestGiver1
            }
            else if (gameObject.CompareTag("QuestGiver2"))
            {
                currentQuest = questManager.GetQuest("Collect Herbs"); // Example quest for QuestGiver2
            }
            else if (gameObject.CompareTag("QuestGiver3"))
            {
                currentQuest = questManager.GetQuest("Defeat the Goblin"); // Example quest for QuestGiver3
            }

            if (currentQuest != null)
            {
                Debug.Log($"Player entered range of {gameObject.name}, assigned quest: {currentQuest.questName}");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            ToggleQuestConversation(false);
            textArea.text = "";  // Clear text when player leaves the trigger
            questIconCreator = null;  // Clear the reference when the player leaves the civilian
        }
    }

    private void ToggleQuestConversation(bool state)
    {
        if (questConversationObject != null)
        {
            questConversationObject.SetActive(state);
            Debug.Log("Quest conversation UI state set to: " + state);
        }
    }
}
