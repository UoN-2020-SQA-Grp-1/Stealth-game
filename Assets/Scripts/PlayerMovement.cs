using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    [Inject]
    private IInputReader _inputReader;
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
        NPCMask = LayerMask.GetMask("NPC");
        InputDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (InputDisabled)
            return;
        if (_inputReader.getButtonDown("Crouch"))
        {
            isCrouched = !isCrouched;
            MovementSpeed = isCrouched ? CrouchingSpeed : RunningSpeed;
            Mouselook.toggleCrouch();
        }

        float x = _inputReader.getMoveSide();
        float z = _inputReader.getMoveForwards();

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
        InputDisabled = true;
    }
}
