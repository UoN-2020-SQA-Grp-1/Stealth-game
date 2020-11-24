using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour { 
    public float mouse_sensitivity = 100f;
    public Transform playerBody;

    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        var mouseX = Input.GetAxis("Mouse X")*mouse_sensitivity *Time.deltaTime;
        var mouseY = Input.GetAxis("Mouse Y") * mouse_sensitivity * Time.deltaTime;

        //xRotation += mouseY;
        xRotation-= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //make it so wasd is relative to the rotation of the camera
        playerBody.Rotate(Vector3.up*mouseX);
    }
}
