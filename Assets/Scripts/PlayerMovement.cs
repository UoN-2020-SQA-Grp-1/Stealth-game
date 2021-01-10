using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;
using Zenject;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController Controller;
    [Inject]
    private IInputReader InputReader;
    public float RunningSpeed = 20f;
    public float CrouchingSpeed = 10f;
    public float MovementSpeed { get; private set; }
    public bool IsCrouched { get; set; } = false;
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
        if (InputReader.getButtonDown("Crouch"))
        {
            ToggleCrouch();
        }

        float x = InputReader.getMoveSide();
        float z = InputReader.getMoveForwards();

        Vector3 move = transform.right * x + transform.forward * z;
        if (!IsCrouched && Controller.velocity != Vector3.zero)
        {
            Collider[] NPCSinAudioRange = Physics.OverlapSphere(transform.position, AudioRange, NPCMask);
            foreach (Collider npc in NPCSinAudioRange)
            {
                npc.gameObject.GetComponent<NPC>().Alert(transform.position);
            }
        }
        Controller.Move(move * MovementSpeed * Time.deltaTime);
    }

    public void ToggleCrouch()
    {
        IsCrouched = !IsCrouched;
        MovementSpeed = IsCrouched ? CrouchingSpeed : RunningSpeed;
        Mouselook.ToggleCrouch();
    }

    public void DisableInput()
    {
        InputDisabled = true;
    }
}
