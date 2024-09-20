using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestIconCreator : MonoBehaviour
{
    public GameObject questIconPrefab;  // Prefab for questIcon
    private GameObject questIcon;       // Reference to the instantiated questIcon
    private bool isPlayerInRange = false;  // Check if the player is within the civilian's collider

    void Start()
    {
        // Instantiate the questIcon and position it above this civilian's head
        if (questIconPrefab != null)
        {
            questIcon = Instantiate(questIconPrefab, transform.position + new Vector3(0, 2.0f, 0), Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("No questIconPrefab assigned.");
        }
    }

    void Update()
    {
        // The quest icon will only be destroyed by the QuestConversation script
    }

    // Method to destroy the quest icon
    public void DestroyQuestIcon()
    {
        if (questIcon != null)
        {
            Debug.Log("Destroying quest icon for " + gameObject.name);
            Destroy(questIcon);  // Destroy the questIcon when the player accepts the quest
        }
        else
        {
            Debug.LogWarning("Quest icon not found for " + gameObject.name);
        }
    }

    // Detect when the player enters the civilian's collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;  // Set the flag to true when the player enters the civilian's range
            Debug.Log("Player entered range of " + gameObject.name);
        }
    }

    // Detect when the player exits the civilian's collider
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;  // Reset the flag when the player leaves the civilian's range
            Debug.Log("Player exited range of " + gameObject.name);
        }
    }
}
