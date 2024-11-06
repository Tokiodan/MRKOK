using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class saveFiles : MonoBehaviour
{
    public UserPreference userpref;
    public GameObject[] SaveButtons;

    void Start()
    {
        for (int i = 0; i < SaveButtons.GetLength(0); i++)
        {
            // changes last played text for each save button
            TextMeshProUGUI text = SaveButtons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
            if (userpref.saves[i].LastPlayed != "")
            {
                text.text = userpref.saves[i].LastPlayed;
            }
        }
    }
}
