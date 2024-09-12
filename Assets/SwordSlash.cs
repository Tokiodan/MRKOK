using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public Transform SlashSpawn;
    [SerializeField] GameObject SnowSlash;
   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var Swordhit = SnowSlash = Instantiate(SnowSlash, SlashSpawn.position, SlashSpawn.rotation);
        }
    }
}
