using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sprintSpeed = 5.5f; // Speed when holding Shift

    public Transform orientation;
    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private AudioSource sfxAudio;
    public Sound[] sfxSounds;

    private Rigidbody rb;

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

        // Check if Shift is held down
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = 5.0f; // Default speed
        }
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
            Debug.Log("Sound not available");
        }
        else if (!sfxAudio.isPlaying)
        {
            Debug.Log(name + " started");
            sfxAudio.clip = s.clip;
            sfxAudio.Play();
        }
    }

    private void StopSFX()
    {
        sfxAudio.Stop();
    }
}
