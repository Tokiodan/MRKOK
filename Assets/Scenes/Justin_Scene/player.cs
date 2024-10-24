using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float player_HP = 100.0f; // Start-HP van de speler
    public int currentLevel = 1;

    // Functie om schade toe te brengen aan de speler
    public void TakeDamage(float damage)
    {
        player_HP -= damage;

        // Controleer of de speler geen HP meer heeft
        if (player_HP <= 0)
        {
            Die();
        }
    }

    // Functie die wordt aangeroepen wanneer de speler doodgaat
    void Die()
    {
        // Hier kun je code toevoegen voor wat er gebeurt als de speler dood is
        Debug.Log("Player is dead!");
        // Bijvoorbeeld de speler deactiveren of naar een 'Game Over' scherm gaan
        gameObject.SetActive(false);
    }
}
