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
    public float AudioRange = 20f;
    private LayerMask NPCMask;
    private bool InputDisabled;

    private void Start()
    {
        MovementSpeed = RunningSpeed;
        InputReader = new InputReader();
        NPCMask = LayerMask.GetMask("NPC");
        InputDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDisabled)
        {
            Debug.Log("Input disabled");
            return;
        }
        if (InputReader.getButtonDown("Crouch"))
        {
            isCrouched = !isCrouched;
            MovementSpeed = isCrouched ? CrouchingSpeed : RunningSpeed;
            Mouselook.toggleCrouch();
        }

        float x = InputReader.getMoveSide();
        float z = InputReader.getMoveForwards();

        Vector3 move = transform.right * x + transform.forward * z;
        if (!isCrouched && controller.velocity != Vector3.zero)
        {
            Collider[] NPCSinAudioRange = Physics.OverlapSphere(transform.position, AudioRange, NPCMask);
            foreach (Collider npc in NPCSinAudioRange)
            {
                npc.gameObject.GetComponent<NPC>().Alert(transform);
            }
        }
        controller.Move(move * MovementSpeed * Time.deltaTime);
    }

    public void DisableInput()
    {
        Debug.Log("Player input is now disabled");
        InputDisabled = true;
    }
}
