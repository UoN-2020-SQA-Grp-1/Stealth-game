using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;

public class MouseLook : MonoBehaviour 
{ 
    public float MouseSensitivity = 100f;
    public Transform PlayerBody;
    public IInputReader InputReader;

    float xRotation = 0f;
    public MouseLook()
    {
        InputReader = new InputReader();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = InputReader.GetMouseX() * MouseSensitivity * Time.deltaTime;
        float mouseY = InputReader.GetMouseY() * MouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Make it so movement is relative to the rotation of the camera.
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); 
        PlayerBody.Rotate(Vector3.up * mouseX);
    }
}
