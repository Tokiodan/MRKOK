using UnityEngine;
using System.Collections;

public class MovementJustin : MonoBehaviour
{
    public float lookSpeedX = 2f;              // Speed of mouse look in the X direction
    public float lookSpeedY = 2f;              // Speed of mouse look in the Y direction
    public float moveSpeed = 5f;               // Speed of movement
    public float verticalLookLimit = 80f;      // Limit for vertical mouse look
    public float gravity = -9.81f;             // Gravity value

    private float rotY = 0f;                   // Vertical rotation
    private Vector3 moveDirection;             // Movement direction
    private CharacterController controller;    // Reference to the CharacterController

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Handle mouse look
        float mouseX = Input.GetAxis("Mouse X") * lookSpeedX;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeedY;

        rotY -= mouseY;
        rotY = Mathf.Clamp(rotY, -verticalLookLimit, verticalLookLimit);

        // Apply rotation
        transform.localRotation = Quaternion.Euler(rotY, transform.localRotation.eulerAngles.y + mouseX, 0);

        // Handle movement
        float moveDirectionY = moveDirection.y;
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        // Apply gravity
        moveDirection.y = moveDirectionY + gravity * Time.deltaTime;

        // Move the player
        controller.Move(moveDirection * Time.deltaTime);
    }

    public void AdjustSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
        Debug.Log("Player speed adjusted to: " + newSpeed);
    }

    public void StartSpeedBoost(float speedBoostAmount, float boostDuration){
        StartCoroutine(SpeedBoost(speedBoostAmount, boostDuration));
    }

    private IEnumerator SpeedBoost(float speedBoostAmount, float boostDuration)
    {
       
        float originalSpeed = moveSpeed;
        Debug.Log("Original speed: " + originalSpeed);

     
        AdjustSpeed(speedBoostAmount);
        Debug.Log("Speed increased to: " + speedBoostAmount);

        yield return new WaitForSeconds(boostDuration);

        AdjustSpeed(5f);
        Debug.Log("Speed reverted to: 5");
    }
}
