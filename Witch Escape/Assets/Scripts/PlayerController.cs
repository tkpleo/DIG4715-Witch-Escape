using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float sprintTimer = 3f;
    public float timeSinceSprint = 0f;
    public float sprintWaitTime = 5f;
    public float timeSprinting = 0f;
    public bool isSprinting = false;

    public float gravity = -9.8f;
    public float jumpForce = 2.0f;
    public float groundCheckDistance = 1.0003f;

    public float lookSensitivityX = 1f;
    public float lookSensitivityY = 1f;
    public float minYLookAngle = -90f;
    public float maxYLookAngle = 90f;
    private float verticalRotation = 0f;

    public Transform playerCamera;
    private Vector3 velocity;
    CharacterController characterController;
    AudioSource audioSource;

    private bool isCrouching = false;
    public float normalHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 2f;
    public Vector3 offset;
    public float upCheckDistance = 2f;

    public bool key = false;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        characterController = GetComponent<CharacterController>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        //if (isGrounded() && velocity.y < 0) 
        //{
        //    velocity.y = -1f;
       // }
       if (!isSprinting) 
       { 
            Move(); 
       }
        Sprint();
        Jump();
        Crouch();
    }

    private void Move() 
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        timeSinceSprint += 1 * Time.deltaTime;

        if (Input.GetButtonDown("Sprint") && timeSinceSprint >= sprintWaitTime)
        {
            isSprinting = true;
        }
        else 
        {
            characterController.Move(move * walkSpeed * Time.deltaTime);
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

    private void Sprint()
    {
        if (isSprinting)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            if (timeSprinting <= sprintTimer)
            {
                characterController.Move(move * walkSpeed * Time.deltaTime * sprintMultiplier);
                timeSprinting += 1 * Time.deltaTime;
            }
            else if (timeSprinting > sprintTimer)
            {
                timeSinceSprint = 0f;
                timeSprinting = 0f;
                isSprinting = false;
                characterController.Move(move * walkSpeed * Time.deltaTime);
            }
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
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
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


    bool isDownUnda()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, upCheckDistance))
        {
            return true;
        }
        return false;
    }

    private void Crouch() 
    {
        if (Input.GetButtonDown("Crouch")) 
        { 
            isCrouching = !isCrouching;
        }


        if (isCrouching == true)
        {
            characterController.height = characterController.height - crouchSpeed * Time.deltaTime;
            if (characterController.height > normalHeight)
            {
                transform.position = transform.position - offset * Time.deltaTime;
            }
            if (characterController.height <= crouchHeight) 
            {
                characterController.height = crouchHeight;
            }
        }
        else if (isCrouching == false && isDownUnda() == false)
        {
            characterController.height = characterController.height + crouchSpeed * Time.deltaTime;

            if (characterController.height < normalHeight)
            {
                transform.position = transform.position + offset * Time.deltaTime;
            }
            if (characterController.height >= normalHeight)
            {
                characterController.height = normalHeight;
            }
        }
    }

    void OnTriggerStay(Collider _other) 
    {
        if (_other.tag == "Deadzone") 
        {
            gameObject.tag = "Player (Undetectable)";
        }
    }

    void OnTriggerExit(Collider _other)
    {
        if (_other.tag == "Deadzone")
        {
            gameObject.tag = "Player (Detectable)";
        }
    }

    void OnTriggerEnter(Collider _other) 
    {
        if (_other.tag == "Enemy")
        {
            Destroy(this);
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
