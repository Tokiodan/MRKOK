using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float defaultMoveSpeed;
    public float sprintSpeedMultiplier;
    public float crouchSpeedMultiplier = 0.6f;
    public float jumpForce = 10f;
    private bool isGrounded;
    public float groundCheckDistance = 1.1f;

    private float baseMoveSpeed; // This will hold the default speed
    public float MoveSpeed { get; private set; } // Public property for moveSpeed

    float horizontalInput;
    float verticalInput;

    public Transform orientation;
    Vector3 moveDirection;

    private bool canPlayWalkSFX = true;
    private bool isCrouched = false;

    Rigidbody rb;
    public InventoryObject inventory;
    public Canvas UI_VISIBLE_CANVAS;
    [SerializeField] private AudioSource sfxAudio;

    public Vector3 normalScale = new Vector3(1, 1, 1);
    public Vector3 crouchScale = new Vector3(1, 0.5f, 1);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        AudioManagerSO.PlaySFXLoop("bg_02", transform.position, 0.25f);
        UI_VISIBLE_CANVAS = GameObject.Find("Inventory").GetComponent<Canvas>();
        baseMoveSpeed = defaultMoveSpeed; // Store the default speed
        MoveSpeed = baseMoveSpeed; // Initialize moveSpeed
    }

    void Update()
    {
        PlayerSound();
        MyInput();
        SpeedControl();

        OpenInventory();
        SaveQuit();
    }

    void FixedUpdate()
    {
        MovePlayer();
        SprintCheck();
        CrouchCheck();
        JumpCheck();
        GroundCheck();
    }

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[30];
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

    private void SprintCheck()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isCrouched)
        {
            MoveSpeed = baseMoveSpeed * sprintSpeedMultiplier; // Set sprint speed
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            MoveSpeed = baseMoveSpeed; // Reset to base speed
        }
    }

    private void CrouchCheck()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isCrouched)
            {
                transform.localScale = normalScale;
                MoveSpeed = baseMoveSpeed; // Reset to base speed
                isCrouched = false;
            }
            else
            {
                transform.localScale = crouchScale;
                MoveSpeed = baseMoveSpeed * crouchSpeedMultiplier; // Set crouch speed
                isCrouched = true;
            }
        }
    }

    private void GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            if (hit.collider != null)
            {
                isGrounded = true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rb.AddForce(moveDirection * MoveSpeed * 10f, ForceMode.Force);
    }

    private void JumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouched)
        {
            Vector3 currentVelocity = rb.velocity;
            rb.velocity = new Vector3(currentVelocity.x, jumpForce, currentVelocity.z);
            isGrounded = false;
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > MoveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * MoveSpeed;
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

    private void SaveQuit()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
        }
    }
}