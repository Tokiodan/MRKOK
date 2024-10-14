using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float defaultMoveSpeed = 5f; // Normal movement speed
    public float sprintSpeedMultiplier = 1.5f; // Initial sprint speed multiplier
    public float crouchSpeedMultiplier = 0.6f; // Crouch speed multiplier
    public float jumpForce = 10f; // Jump force
    private bool isGrounded; // Check if the player is on the ground
    public float groundCheckDistance = 1.1f; // Distance to check if grounded

    private float baseMoveSpeed; // This will hold the default speed
    public float MoveSpeed { get; private set; } // Public property for moveSpeed

    float horizontalInput;
    float verticalInput;

    public Transform orientation; // Player orientation
    Vector3 moveDirection;

    private bool canPlayWalkSFX = true; // Sound effects control
    private bool isCrouched = false; // Check if the player is crouching

    Rigidbody rb; // Rigidbody component
    public InventoryObject inventory; // Reference to the inventory
    public Canvas UI_VISIBLE_CANVAS; // Reference to the UI
    [SerializeField] private AudioSource sfxAudio; // Sound effects audio source

    public Vector3 normalScale = new Vector3(1, 1, 1); // Normal scale for the player
    public Vector3 crouchScale = new Vector3(1, 0.5f, 1); // Scale when crouching

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        UI_VISIBLE_CANVAS = GameObject.Find("Inventory").GetComponent<Canvas>();
        baseMoveSpeed = defaultMoveSpeed; // Store the default speed
        MoveSpeed = baseMoveSpeed; // Initialize moveSpeed
    }

    void Update()
    {
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
        // Check if the player is holding the shift key to sprint
        if (Input.GetKey(KeyCode.LeftShift) && !isCrouched)
        {
            MoveSpeed = baseMoveSpeed * sprintSpeedMultiplier; // Set sprint speed
        }
        else
        {
            MoveSpeed = baseMoveSpeed; // Reset to base speed when not sprinting
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

    public void UpgradeSprintSpeed()
    {
        // Increase the sprint speed multiplier each time this method is called
        sprintSpeedMultiplier += 0.1f; // Increase by 0.1 (adjust as needed)
        Debug.Log($"Sprint Speed Multiplier upgraded to: {sprintSpeedMultiplier}"); // Debug log
    }
}
