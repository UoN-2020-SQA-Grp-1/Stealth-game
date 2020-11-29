using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;

public class PlayerMovement : MonoBehaviour
{ 
    public CharacterController controller;

    public float MovementSpeed = 12f;
    public IInputReader InputReader;
    public PlayerMovement()
    {
        InputReader = new InputReader();
    }

    // Update is called once per frame
    void Update()
    {
        float x = InputReader.getMoveSide();
        float z = InputReader.getMoveForwards();

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * MovementSpeed * Time.deltaTime);
    }
}
