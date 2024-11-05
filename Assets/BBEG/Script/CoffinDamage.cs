using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffinDamage : MonoBehaviour
{
    public BBEG bbeg;                  // Verwijzing naar de BBEG die schade moet ontvangen
    public int damageAmount = 10;      // Hoeveelheid schade die aan de BBEG wordt gedaan bij elke aanval

    private void OnTriggerEnter(Collider other)
    {
        // Controleer of het object dat de trigger raakt, een aanvalsspell of -projectiel is
        if (other.CompareTag("Attack"))
        {
            // Zorg ervoor dat de BBEG-referentie is ingesteld
            if (bbeg != null)
            {
                // Voer schade toe aan de BBEG
                bbeg.TakePhysicalDmg(damageAmount);
                Debug.Log("BBEG neemt " + damageAmount + " schade door aanval op het object.");
            }
            else
            {
                Debug.LogWarning("BBEG-referentie ontbreekt in MiasmaDamageRelay!");
            }
        }
    }
}
