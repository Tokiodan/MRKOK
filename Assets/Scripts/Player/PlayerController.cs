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

    private bool canPlayWalkSFX = true;

    Vector3 moveDirection;

    [SerializeField] private AudioSource sfxAudio;

    Rigidbody rb;

    public InventoryObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        // sfxAudio = GetComponent<AudioSource>();
        AudioManagerSO.PlaySFXLoop("bg_02", transform.position, 0.25f);
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

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
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


        if (sfxAudio == null && canPlayWalkSFX && (MathF.Abs(verticalInput) + MathF.Abs(horizontalInput)) > 0)
        {
            canPlayWalkSFX = false;
            AudioSource a = AudioManagerSO.PlaySFXLoop("Walking", transform.position, 0.5f);
            StartCoroutine(WaitforStop(a));
        }
    }

    private IEnumerator WaitforStop(AudioSource SFX)
    {
        Debug.Log("waiting until standing still...");
        yield return new WaitUntil(() => (MathF.Abs(verticalInput) + MathF.Abs(horizontalInput)) == 0);
        SFX.Stop();
        canPlayWalkSFX = true;

    }

}
