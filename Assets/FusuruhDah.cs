using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusuruhDah : MonoBehaviour
{
    public Transform ParticleSpawnPoint;
    [SerializeField] GameObject FusuruhEffect;
    public float FusuruhSpeed = 1;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            var FusuruhDah = FusuruhEffect = Instantiate(FusuruhEffect, ParticleSpawnPoint.position, ParticleSpawnPoint.rotation);
            FusuruhDah.GetComponent<Rigidbody>().velocity = ParticleSpawnPoint.forward * FusuruhSpeed; 
        }
    }
}
