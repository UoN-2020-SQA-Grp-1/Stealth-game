using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;

public class PlayerMovement : MonoBehaviour
{ 
    public CharacterController controller;
    public IInputReader InputReader;
    public float RunningSpeed = 20f;
    public float CrouchingSpeed = 10f;
    public float MovementSpeed { get; private set; }
    public bool isCrouched { get; set; } = false;
    public MouseLook Mouselook;
    public PlayerMovement()
    {
        MovementSpeed = RunningSpeed;
        InputReader = new InputReader();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputReader.getButtonDown("Crouch"))
        {
            isCrouched = !isCrouched;
            MovementSpeed = isCrouched ? CrouchingSpeed : RunningSpeed;
            Mouselook.toggleCrouch();
        }

        float x = InputReader.getMoveSide();
        float z = InputReader.getMoveForwards();

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * MovementSpeed * Time.deltaTime);
    }
}
