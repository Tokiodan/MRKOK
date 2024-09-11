using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;

    public Transform orientation;
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    AudioSource sfxAudio;

    public Sound[] sfxSounds;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        sfxAudio = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerSound();
        MyInput();
        SpeedControl();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }



    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void PlayerSound()
    {
        if ((MathF.Abs(verticalInput) + MathF.Abs(horizontalInput)) > 0)
        {
            MovementSFX("Grass walking");
        }
        else
        {
            StopSFX();
        }
    }

    private void MovementSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("non available sound");
        }
        else if (!sfxAudio.isPlaying)
        {
            Debug.Log(name + " started");
            sfxAudio.clip = s.clip;
            sfxAudio.Play();
        }
    }

    private void QuickSFX()
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("non available sound");
        }
        else if (!sfxAudio.isPlaying)
        {
            sfxAudio.PlayOneShot(s.clip);
        }
    }

    private void StopSFX()
    {
        sfxAudio.Stop();
    }
}
