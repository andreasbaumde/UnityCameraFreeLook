using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLook : MonoBehaviour
{
    new Camera camera;

    public float movementSpeed = 10.0f;
    public float turnSpeed = 3.0f;
    public Vector3 cameraSpawn;
    public bool cursorVisible = false;
    public bool lockCursor = true;

    float yaw = 0.0f;
    float pitch = 0.0f;
    float turnSpeedH;
    float turnSpeedV;
    float inputH = 0.0f;
    float inputV = 0.0f;
    bool buttonJumpDown = false;

    void Start()
    {
        camera = GetComponent<Camera>();

        // Camera object check
        if(camera == null)
        {
            Debug.LogError("FreeLook: No camera component was found on this gameobject.");
        }

        // Camera spawn
        if (cameraSpawn == null)
        {
            cameraSpawn = transform.position;
        }

        transform.position = cameraSpawn;

        // Turn speed
        turnSpeedH = turnSpeed;
        turnSpeedV = turnSpeed;

        // Cursor
        CursorSettings();
    }
	
	void Update()
    {
        yaw += turnSpeedH * Input.GetAxisRaw("Mouse X");
        pitch -= turnSpeedV * Input.GetAxisRaw("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        inputH = Input.GetAxisRaw("Horizontal");
        inputV = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = (transform.forward * inputV + inputH * transform.right).normalized;

        if(Input.GetKey(KeyCode.X))
        {
            moveDirection.y = -1.0f;
        }

        if(Input.GetButton("Jump"))
        {
            moveDirection.y = 1.0f;
        }

        transform.position = transform.position + moveDirection * movementSpeed * Time.deltaTime;
    }

    private void OnApplicationFocus(bool focus)
    {
        if(focus)
        {
            CursorSettings();
        }
    }

    private void CursorSettings()
    {
        // Cursor
        Cursor.visible = cursorVisible;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}