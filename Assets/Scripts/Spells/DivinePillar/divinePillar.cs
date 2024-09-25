using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class divinePillar : MonoBehaviour
{
    public GameObject aoeIndicatorPrefab; // Voor de indicator
    public GameObject aoeLightPrefab; // Voor de lichtstraal
    public float cooldownDuration = 5f; // Duur van de cooldown in seconden
    private float cooldownTimer = 0f; // Timer om cooldown bij te houden
    private GameObject currentIndicator;
    private Vector3 targetPosition;
    private bool isPlacingAoE = false;

    void Update()
    {
        HandleAoEInput();
        UpdateCooldownTimer();
    }

    void HandleAoEInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha4) && !isPlacingAoE && cooldownTimer <= 0f)
        {
            // Maak de indicator aan
            currentIndicator = Instantiate(aoeIndicatorPrefab, Vector3.zero, Quaternion.identity);
            isPlacingAoE = true;
        }

        if (isPlacingAoE && currentIndicator != null)
        {
            // Volg de muispositie om de indicator te verplaatsen
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // Update de indicator positie maar sla de targetpositie op
                targetPosition = new Vector3(hit.point.x, -0.99f, hit.point.z);
                Debug.Log(hit.collider.gameObject.name);
                currentIndicator.transform.position = targetPosition; // Houdt de indicator boven de grond
            }
        }

        if (Input.GetKeyUp(KeyCode.Alpha4) && currentIndicator != null)
        {
            // Bevestig de aanval
            // Verwijder de indicator
            Destroy(currentIndicator);
            isPlacingAoE = false;

            // Creëer de gele lichtstraal
            GameObject aoeLight = Instantiate(aoeLightPrefab, targetPosition, Quaternion.identity);
            Destroy(aoeLight, 3f); // Verwijder de lichtstraal na 3 seconden

            // Start cooldown
            cooldownTimer = cooldownDuration;
        }
    }

    void UpdateCooldownTimer()
    {
        // Verminder de cooldown timer als deze groter is dan 0
        if (cooldownTimer > 0f)
        {
            cooldownTimer -= Time.deltaTime;
        }
    }
}
