using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public GameObject settingsMenu; // Drag your settings menu here
    public GameObject mainMenu; // Drag your main menu here

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoBackToMainMenu);
    }

    public void GoBackToMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
