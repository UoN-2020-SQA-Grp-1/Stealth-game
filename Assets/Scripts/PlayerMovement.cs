using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;

public class PlayerMovement : MonoBehaviour
{ 
    public CharacterController controller;

    public float MovementSpeed = 12f;
    public IInputReader InputReader;

    private static float RUNNING_SPEED = 12f;
    private static float CROUCHING_SPEED = 4f;
    private bool isCrouched = false;
    public PlayerMovement()
    {
        InputReader = new InputReader();
    }

    // Update is called once per frame
    void Update()
    {
        if (InputReader.getButtonDown("Crouch"))
        {
            isCrouched = !isCrouched;
            MovementSpeed = isCrouched ? CROUCHING_SPEED : RUNNING_SPEED;
        }

        float x = InputReader.getMoveSide();
        float z = InputReader.getMoveForwards();

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * MovementSpeed * Time.deltaTime);

    }
}
