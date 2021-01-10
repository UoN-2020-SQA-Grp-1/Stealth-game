using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Assets.Lib;

public class Sight : MonoBehaviour
{
    [Inject]
    private ITextDisplayer TextDisplayer;

    public int FieldOfView = 45;
    public float ViewDistance = 10f;
    public float StandingSightRange = 10f;
    public float CrouchingSightRange = 4f;
    // The rate at which it checks it's sight 
    public float DetectionRate = 1.0f;
    public int FramesSeenBeforeReset = 100;

    // To keep track of where the player is
    private Transform PlayerTransform;
    private Vector3 RayDirection;
    private float ElapsedTime = 0.0f;
    private int FramesSeen = 0;

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
                        float visibility = player.IsCrouched ? CrouchingSightRange : StandingSightRange;
                        //Debug.Log("Visibility = " + visibility);
                        if (Vector3.Distance(PlayerTransform.position, gameObject.transform.position) < visibility)
                        {
                            if (++FramesSeen >= FramesSeenBeforeReset)
                            {
                                RestartLevel();
                            }
                        }
                    }
                }
            }
        }
    }

    private void RestartLevel()
    {
        FramesSeen = 0;
        TextDisplayer.ShowText("You were seen! Finish the level without being seen");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
        EnemyTakeDown enemyTakeDown = player.GetComponent<EnemyTakeDown>();
        playerMovement.DisableInput();
        enemyTakeDown.DisableInput();
        StartCoroutine("WaitFor");
    }
    private IEnumerator WaitFor()
    {
        yield return new WaitForSeconds(3);
        TextDisplayer.HideText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
