using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float moveSpeed;
    float horizontalInput;
    float verticalInput;

    public Transform orientation;
    Vector3 moveDirection;

    private bool canPlayWalkSFX = true;

    Rigidbody rb;
    public InventoryObject inventory;
    private Canvas UI_VISIBLE_CANVAS;
    [SerializeField] private AudioSource sfxAudio;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        AudioManagerSO.PlaySFXLoop("bg_02", transform.position, 0.25f);
        UI_VISIBLE_CANVAS = GameObject.Find("Inventory").GetComponent<Canvas>();

    }

    // Update is called once per frame
    void Update()
    {
        PlayerSound();
        MyInput();
        SpeedControl();

        OpenInventory();
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

    private void OpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.E) && UI_VISIBLE_CANVAS.enabled)
        {
            UI_VISIBLE_CANVAS.enabled = false;
            Camera.main.GetComponent<PlayerCam>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            UI_VISIBLE_CANVAS.enabled = true;
            Camera.main.GetComponent<PlayerCam>().enabled = false;
            Time.timeScale = 0f;
        }
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
        yield return new WaitUntil(() => (MathF.Abs(verticalInput) + MathF.Abs(horizontalInput)) == 0);
        SFX.Stop();
        canPlayWalkSFX = true;

    }

}
