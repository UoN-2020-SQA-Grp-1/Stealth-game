using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public List<Transform> Waypoints;
    private int WaypointsIndex;
    public Transform NextWaypoint => Waypoints[WaypointsIndex];
    private NavMeshAgent Agent;
    // Each NPC needs a unique ID which is used to fetch its waypoints.
    // So a static int field is used to ensure uniqueness.
    public static int NPCCount { get; set; } = 0;
    public float AlertTime = 5f;
    private int ID;
    private float AlertTimeStamp;
    private bool IsInvestigating;

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
            //Debug.Log("NPC ID " + ID + " waypoint: " + p.transform.position);
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

    // Update is called once per frame
    void Update()
    {
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
        else if (AlertTimeStamp != 0 && Time.time - AlertTimeStamp >= AlertTime)
        {
            Debug.Log("NPC alert time expired, returning to patrol");
            AlertTimeStamp = 0f;
            GoToNextWaypoint();
        }
    }

    public void Alert(Transform player)
    {
        Debug.Log("NPC Alerted!");
        IsInvestigating = true;
        Agent.destination = player.position;
        AlertTimeStamp = Time.time;
    }
}
