using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic_attack : MonoBehaviour
{
    public float damage = 10.0f; // De schade die de vijand veroorzaakt
    public GameObject player; // Referentie naar het speler-object

    private player playerScript; // Verwijzing naar het player-script om HP te beheren

    // Start is called before the eerste frame update
    void Start()
    {
        // Zorg ervoor dat we toegang krijgen tot het player-script op het speler-object
        playerScript = player.GetComponent<player>();
    }

    // Dit wordt aangeroepen wanneer de vijand iets raakt
    void OnCollisionEnter(Collision collision)
    {
        // Controleer of de vijand de speler raakt
        if (collision.gameObject == player)
        {
            // Verminder de HP van de speler met de hoeveelheid schade
            playerScript.TakeDamage(damage);
        }
    }
}
