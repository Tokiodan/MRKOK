using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npcshop : MonoBehaviour
{
 public GameObject shopPanel; 
    private bool isShopOpen = false; 

    private void Update()
    {
        // Als je op B drukt gaat shop open in range van NPC
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (isShopOpen)
            {
                CloseShop();
            }
            else
            {
                OpenShop();
            }
        }
    }

    private void OpenShop()
    {
        shopPanel.SetActive(true);
        Time.timeScale = 0; // zet game op pauze
        isShopOpen = true; 
    }

    private void CloseShop()
    {
        shopPanel.SetActive(false);
        Time.timeScale = 1; // zet game niet meer op pauze
        isShopOpen = false; 
    }
}