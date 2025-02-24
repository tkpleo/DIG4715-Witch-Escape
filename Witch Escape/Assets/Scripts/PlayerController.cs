using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float jumpForce = 2.0f;
    public float groundCheckDistance = 1.5f;
    public float lookSensitivityX = 1f;
    public float lookSensitivityY = 1f;
    public float minYLookAngle = -90f;
    public float maxYLookAngle = 90f;
    public Transform playerCamera;
    private Vector3 velocity;
    private float verticalRotation = 0f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        Crouch();
    }

    private void Move() 
    {
        var dir = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (Input.GetAxis("Sprint") > 0)
        {
            this.transform.Translate(dir * (walkSpeed * sprintMultiplier) * Time.deltaTime);
        }
        else 
        {
            this.transform.Translate(dir * walkSpeed * Time.deltaTime);
        }
        

        if (playerCamera != null) 
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSensitivityX;
            float mouseY = Input.GetAxis("Mouse Y") * lookSensitivityY;

            verticalRotation -= mouseY;
            verticalRotation = Mathf.Clamp(verticalRotation, minYLookAngle, maxYLookAngle);

            playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);
        }
        
    }

    private void Jump() 
    {
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }

    bool isGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance))
        {
            return true;
        }
        return false;
    }

    private void Crouch() 
    { 
    
    }

    void OnTriggerStay(Collider _other) 
    {
        if (_other.tag == "DeadZone") 
        {
            gameObject.tag = "Player (Undetectable)";
        }
    }

    void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "DeadZone")
        {
            gameObject.tag = "Player (Detectable)";
        }
    }
}
