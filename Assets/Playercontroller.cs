using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Playercontroller : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float speed;
    public float sensitivity;
    private Rigidbody rb;
    public Transform orientation;
    private Vector3 moveDirection;
    private float xRotation;
    private float yRotation;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        //camera movement
        var c = Camera.main.transform;

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensitivity;

        yRotation -= mouseY;
        
        xRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -90f, 90f);


        c.rotation = Quaternion.Euler(yRotation, xRotation, 0);
        transform.rotation = Quaternion.Euler(0, -xRotation, 0);

        //player movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * -verticalInput + transform.right * -horizontalInput;
        rb.AddForce(moveDirection.normalized * speed * 10f, ForceMode.Force);
    }
}
