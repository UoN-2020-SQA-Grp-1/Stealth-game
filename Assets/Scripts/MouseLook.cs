using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;
using Zenject;

public class MouseLook : MonoBehaviour 
{ 
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;
    [Inject]
    private IInputReader _inputReader;
    public float CrouchCameraOffset = 3f;

    float xRotation = 0f;
    private bool isCrouched = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = _inputReader.getMouseX() * MouseSensitivity * Time.deltaTime;
        float mouseY = _inputReader.getMouseY() * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Make it so movement is relative to the rotation of the camera.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        PlayerBody.Rotate(Vector3.up * mouseX);
    }

    // Called by the player movement class when the crouch button is pressed.
    public void ToggleCrouch()
    {
        isCrouched = !isCrouched;
        Vector3 camPosition = transform.position;
        float y = isCrouched ? camPosition.y - CrouchCameraOffset : camPosition.y + CrouchCameraOffset;
        camPosition.y = y;
        transform.position = camPosition;
    }
}
