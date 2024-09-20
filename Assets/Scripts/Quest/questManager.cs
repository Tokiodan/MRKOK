using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    private List<Quest> quests = new List<Quest>();
    public GameObject questMenuUI;  // The UI element for the quest menu
    public GameObject buttonPrefab;  // The button prefab to instantiate for each quest
    public Transform buttonParent;  // The parent transform where buttons will be added
    public TMP_Text questDescriptionText;  // The TextMeshPro text area for displaying selected quest description

    private Quest currentQuest;  // The currently selected quest
    private int currentQuestIndex = -1;  // Index of the currently selected quest

    public bool playerFoundDog = false;
    public int playerHerbsCollected = 0;
    public bool goblinBossDefeated = false;

    void Start()
{
    // Example: Quest 1 - Find a missing dog
    quests.Add(new Quest("Find the Lost Dog", 
        "Help find the missing dog in the village.", 
        () => playerFoundDog));  // clearCondition is checking a boolean flag

    // Example: Quest 2 - Collect 5 herbs
    quests.Add(new Quest("Collect Herbs", 
        "Gather herbs from the forest.", 
        () => playerHerbsCollected >= 5));  // clearCondition checks an integer value

    // Example: Quest 3 - Defeat a goblin boss
    quests.Add(new Quest("Defeat the Goblin", 
        "Defeat the goblin in the cave.", 
        () => goblinBossDefeated));  // clearCondition checks if boss is defeated
}

    // Call this to accept a quest
    public void AcceptQuest(string questName)
    {
        Quest quest = quests.Find(q => q.questName == questName);
        if (quest != null)
        {
            quest.isAccepted = true;
            Debug.Log($"Quest accepted: {quest.questName}");
            UpdateQuestMenu();  // Update quest menu after accepting the quest
        }
        else
        {
            Debug.LogWarning($"Quest not found: {questName}");
        }
    }

    // Get a quest by name
    public Quest GetQuest(string questName)
    {
        return quests.Find(q => q.questName == questName);
    }

    void Update()
    {
        // Toggle quest menu when 'M' is pressed
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleQuestMenu();
        }

        // Example inputs for quest conditions
        if (Input.GetKeyDown(KeyCode.F))  // Assuming 'F' is for finding the dog
        {
            playerFoundDog = true;
        }

        if (Input.GetKeyDown(KeyCode.H))  // Assuming 'H' is for collecting herbs
        {
            playerHerbsCollected += 1;
        }

        if (Input.GetKeyDown(KeyCode.G))  // Assuming 'G' is for defeating the goblin boss
        {
            goblinBossDefeated = true;
        }

        // Check for quests that can be completed
        List<Quest> questsToComplete = new List<Quest>();

        foreach (var quest in quests)
        {
            if (!quest.isCompleted && quest.isAccepted && quest.IsQuestCompleted())
            {
                questsToComplete.Add(quest);
            }
        }

        // Complete the quests outside of the loop to avoid modifying the list during iteration
        foreach (var quest in questsToComplete)
        {
            CompleteQuest(quest);
        }
    }

    
    private void ToggleQuestMenu()
    {
        // Toggle the active state of the quest menu UI
        if (questMenuUI != null)
        {
            bool isActive = questMenuUI.activeSelf; // Check if it's currently active
            questMenuUI.SetActive(!isActive); // Toggle the active state

            // Update the quest buttons when the menu becomes active
            if (!isActive) // Only update when the menu becomes active
            {
                UpdateQuestMenu();
                Cursor.lockState = CursorLockMode.None; // Unlock the cursor
                Cursor.visible = true; // Make the cursor visible
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked; // Lock the cursor back to the game
                Cursor.visible = false; // Hide the cursor
            }
        }
        else
        {
            Debug.LogWarning("Quest Menu UI is not assigned!");
        }
    }

    private void UpdateQuestMenu()
{
    // Clear existing buttons in the buttonParent (InnerImage)
    foreach (Transform child in buttonParent)
    {
        Destroy(child.gameObject);
    }

    // Loop through all accepted quests and instantiate buttons for each
    foreach (Quest quest in quests)
    {
        if (quest.isAccepted)
        {
            // Instantiate a new button from the prefab as a child of buttonParent
            GameObject buttonInstance = Instantiate(buttonPrefab, buttonParent);

            // Set the text of the button to display the quest name
            TMP_Text buttonText = buttonInstance.GetComponentInChildren<TMP_Text>();  // Assuming the button prefab has a TMP_Text component
            if (buttonText != null)
            {
                buttonText.text = quest.questName;
            }

            // Adjust RectTransform to stretch within the parent (InnerImage)
            RectTransform rectTransform = buttonInstance.GetComponent<RectTransform>();
            if (rectTransform != null)
            {
                rectTransform.anchorMin = new Vector2(0, 0);  // Bottom-left corner
                rectTransform.anchorMax = new Vector2(1, 1);  // Top-right corner
                rectTransform.offsetMin = new Vector2(0, 0);  // No offset on left/bottom
                rectTransform.offsetMax = new Vector2(0, 0);  // No offset on right/top
            }

            // Add a click listener to set the current quest when the button is clicked
            Button button = buttonInstance.GetComponent<Button>();  // Assuming the prefab has a Button component
            if (button != null)
            {
                Quest trackedQuest = quest;  // Capture the quest in the closure
                button.onClick.AddListener(() => SelectQuest(trackedQuest));  // Call SelectQuest when the button is clicked
            }
        }
    }
}

    private void SelectQuest(Quest quest)
    {
        currentQuest = quest;  // Set the current quest
        Debug.Log($"Current quest selected: {currentQuest.questName}");

        // Update the quest description display
        if (questDescriptionText != null)
        {
            questDescriptionText.text = $"{currentQuest.questName}: {currentQuest.description}";
        }

        // Optionally, update the quest tracker if needed
        FindObjectOfType<QuestTracker>().TrackQuest(currentQuest);
    }

    private void CompleteQuest(Quest quest)
    {
        quest.isCompleted = true;
        quests.Remove(quest); // Remove the quest from the list
        Debug.Log($"Quest '{quest.questName}' completed and removed from the list.");

        // Update the Quest Tracker
        FindObjectOfType<QuestTracker>().CheckForCompletedQuest();

        // Optionally update the UI to reflect the completed quest
        UpdateQuestMenu();
    }


    public Quest GetRandomQuest()
    {
        List<Quest> availableQuests = quests.FindAll(q => q.isAccepted && !q.isCompleted);
        if (availableQuests.Count > 0)
        {
            int randomIndex = Random.Range(0, availableQuests.Count);
            return availableQuests[randomIndex];
        }
        return null; // No available quests
    }

}
