using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public float sensX;
    public float sensY;

    public Transform orientation;

    public UserPreference userPreference = Resources.Load<UserPreference>("UserPreference");

    float xRotation;
    float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        sensX = userPreference.sensitivity;
        sensY = userPreference.sensitivity;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Camera.main.fieldOfView = userPreference.FOV;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * sensY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // cam rotate
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }


    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
