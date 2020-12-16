using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearing : MonoBehaviour
{
    public float RunningAudioRange = 20f;
    public float CrouchingAudioRange = 0f;
    public CharacterController controller;
    private SphereCollider AudioCollider;
    private bool isCrouched = false;
    private bool AudioRangeExpanding = false;
    // Start is called before the first frame update
    void Start()
    {
        AudioCollider = GetComponent<SphereCollider>(); 
    }

    private void Update()
    {
        if (!AudioRangeExpanding)
            return;
        AudioCollider.radius += 1;
        if (AudioCollider.radius == RunningAudioRange)
            AudioRangeExpanding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player is standing still, he can't be heard regardless of 
        // if he's standing.
        if (controller.velocity == Vector3.zero)
            return;
        if (other.CompareTag("NPC"))
            other.gameObject.GetComponent<NPC>().Alert(transform);
    }

    public void toggleCrouch()
    {
        isCrouched = !isCrouched;
        if (isCrouched)
            AudioCollider.radius = CrouchingAudioRange;
        else
            AudioRangeExpanding = true;
    }
}
