using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public List<Transform> Waypoints;
    private int WaypointsIndex;
    public Transform NextWaypoint => Waypoints[WaypointsIndex];
    public GameObject DeathAnimation;
    private NavMeshAgent Agent;
    // Each NPC needs a unique ID which is used to fetch its waypoints.
    // So a static int field is used to ensure uniqueness.
    public static int NPCCount { get; set; } = 0;
    public float AlertTime = 5f;
    private int ID;
    private float AlertTimeStamp;
    private bool IsInvestigating;

    /*
     * OnDestroy() instantiates an explosion when dying. But OnDestroy() is also called even 
     * when the application quits or when the level is reloaded, causing an exception because 
     * the explosion wasn't cleaned up properly. Therefore, OnDestroy() should check the state 
     * of these booleans before instantiating the explosion. Then IsQuitting is set to true in 
     * OnApplicationQuit() to prevent the explosion being instantiated. Both are required as 
     * OnApplicationQuit() is automatically called when reloading a level.
     */
    private bool IsQuitting = false;
    private bool IsDead = false;

    // Start is called before the first frame update
    void Start()
    {
        // Assigns the ID and then increments the NPCCount.
        ID = NPCCount++;
        Agent = GetComponent<NavMeshAgent>();
        // Prevents the NPC from stopping when reaching a waypoint.
        Agent.autoBraking = false;
        string tag = "waypoints" + ID;
        GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject p in points)
        {
            Waypoints.Add(p.transform);        
        }
        AlertTimeStamp = 0f;
        IsInvestigating = false;
        GoToNextWaypoint();
    }

    void GoToNextWaypoint()
    {
        if (Waypoints.Count == 0)
        {
            Debug.Log("No waypoints in waypoints array");
            return;
        }
        if (!IsInvestigating)
            WaypointsIndex = (WaypointsIndex + 1) % Waypoints.Count;
        else
            IsInvestigating = false;
        Agent.destination = Waypoints[WaypointsIndex].position;
    }

    void Update()
    {
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
        else if (AlertTimeStamp != 0 && Time.time - AlertTimeStamp >= AlertTime)
        {
            AlertTimeStamp = 0f;
            GoToNextWaypoint();
        }
    }

    public void Alert(Vector3 pos)
    {
        IsInvestigating = true;
        Agent.destination = pos;
        AlertTimeStamp = Time.time;
    }

    private void OnDestroy()
    {
        if (!IsQuitting && IsDead)
        {
            Instantiate(DeathAnimation, transform.position, Quaternion.identity);
        }
    }

    private void OnApplicationQuit()
    {
        IsQuitting = true;
    }

    public void Kill()
    {
        IsDead = true;
    }
}
