using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class divinePillar : MonoBehaviour, MagicAttack
{
    public GameObject aoeIndicatorPrefab; // For the indicator
    public GameObject aoeLightPrefab; // For the light beam
    public float aoeDuration = 3f; // Duration of the AoE effect
    public int damageAmount = 20; // Amount of damage to deal

    private bool cooldownTimer = false; // Changed to a bool bcs I cba to make a new one -Z
    private GameObject currentIndicator;
    private Vector3 targetPosition;
    [SerializeField] private bool isPlacingAoE = false;
    private bool hasDealtDamage = false; // Ensure damage is only dealt once

    public void CastSpell()
    {
        // this is also something that should have been an uneccesary headache. -Z
        //we should think next time about how it intergrates with our main scene. -Z
        CoroutineManager.Instance.StartManagedCoroutine(iDontWantToLiveAnymore());
    }

    // NEVER. MAKE. ME. DO. THIS. AGAIN. -Z
    // So basically, every time we start a routine like this. it will change the variables within the prefab. EVERY. SINGLE. TIME.-Z
    // thus I had to make the Ienumerator make them reset properly EVERY. SINGLE. TIME. -Z
    // This fucking sucked to find out and I am so sad it took me this long. -Z
    public IEnumerator iDontWantToLiveAnymore()
    {
        // instantly exists out of Enum if this spell is already active
        if (currentIndicator != null)
        {
            yield break;
        }

        // I will lose my mind because of these variables I have to reset every time. -Z
        hasDealtDamage = false;
        isPlacingAoE = false;
        currentIndicator = null;


        while (cooldownTimer == false)
        {
            if (!isPlacingAoE)
            {
                isPlacingAoE = true;
                Debug.Log("indicator");
                // Create the indicator
                currentIndicator = Instantiate(aoeIndicatorPrefab, Vector3.zero, Quaternion.identity);
                hasDealtDamage = false; // Reset damage flag for a new cast
            }

            if (isPlacingAoE && currentIndicator != null)
            {
                Debug.Log("follow cam");
                // Follow the mouse position to move the indicator
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    // Update the indicator position but save the target position
                    targetPosition = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                    currentIndicator.transform.position = targetPosition; // Keep the indicator above the ground
                }

                // just an confirmation keybind. So we can double confirm the attack.
                if (Input.GetKeyDown(KeyCode.Y) && currentIndicator != null)
                {
                    Debug.Log("confirmed. attack now!");
                    // sets the cooldown after casting.
                    PlayerController.lastSpawnTime = Time.time;

                    // Confirm the attack
                    // Destroy the indicator
                    Destroy(currentIndicator);
                    //removes indicator object just incase.
                    currentIndicator = null;
                    isPlacingAoE = false;

                    // Create the light beam with a collider to handle damage
                    GameObject aoeLight = Instantiate(aoeLightPrefab, targetPosition, Quaternion.identity);

                    //Destroys the AOE attack after 3 seconds. -Z
                    // because the cooldownTimer is true the while loop will not keep going. -Z
                    cooldownTimer = true;
                    yield return new WaitForSeconds(3);
                    Destroy(aoeLight);

                }
            }
            // restarts the loop and makes way for the program to function without deleting itself. -Z
            yield return null;
        }
        // cooldownTimer is reset because we have to do this EVERY. SINGLE. TIME. in order for it to not fuck up the next time we use the spell. -Z
        cooldownTimer = false;
    }
}