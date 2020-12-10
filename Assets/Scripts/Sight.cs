using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sight : MonoBehaviour
{
    public int FieldOfView = 45;
    public int ViewDistance = 10;
    // The rate at which it checks it's sight 
    public float DetectionRate = 1.0f;

    // To keep track of where the player is
    private Transform PlayerTransform;
    private Vector3 RayDirection;
    private float ElapsedTime = 0.0f;

    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= DetectionRate)
        {
            RaycastHit hit;
            RayDirection = PlayerTransform.position - transform.position;
            // Calculates whether the angle of sight between NPC and player is within the FOV.
            if (Vector3.Angle(RayDirection, transform.forward) < FieldOfView)
            {
                // If the ray hits something
                if (Physics.Raycast(transform.position, RayDirection, out hit, ViewDistance))
                {
                    // Ensures that the ray actually collides with the player, i.e. it prevents 
                    // seeing the player if there's a wall between them. 
                    PlayerMovement player = hit.collider.GetComponent<PlayerMovement>();
                    if (player != null)
                    {
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    }
                }
            }
        }
    }
}
