using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownManager : MonoBehaviour
{
    private Dictionary<string, float> cooldownTimers = new Dictionary<string, float>();

    public bool IsOnCooldown(string abilityName)
    {
        if (cooldownTimers.ContainsKey(abilityName))
        {
            return cooldownTimers[abilityName] > Time.time;
        }
        return false;
    }

    public void StartCooldown(string abilityName, float cooldownDuration)
    {
        cooldownTimers[abilityName] = Time.time + cooldownDuration;
    }

    public float GetRemainingCooldown(string abilityName)
    {
        if (cooldownTimers.ContainsKey(abilityName))
        {
            return Mathf.Max(0f, cooldownTimers[abilityName] - Time.time);
        }
        return 0f;
    }
}
