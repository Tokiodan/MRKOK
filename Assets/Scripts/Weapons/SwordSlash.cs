using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public Transform SlashSpawn;              // Position and rotation for the spawned slash
    [SerializeField] GameObject SnowSlash;    // The SnowSlash particle effect to instantiate

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Instantiate the SnowSlash at the specified spawn position and rotation
            Instantiate(SnowSlash, SlashSpawn.position, SlashSpawn.rotation);
        }
    }
}
