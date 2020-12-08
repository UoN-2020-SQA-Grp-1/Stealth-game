using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public List<Transform> Waypoints;
    private int WaypointsIndex;
    private NavMeshAgent Agent;
    // Each NPC needs a unique ID which is used to fetch its waypoints.
    // So a static int field is used to ensure uniqueness.
    public static int NPCCount { get; set; } = 0;
    private int ID;

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
        GoToNextWaypoint();
    }

    void GoToNextWaypoint()
    {
        if (Waypoints.Count == 0)
        {
            Debug.Log("No waypoints in waypoints array");
            return;
        }
        Agent.destination = Waypoints[WaypointsIndex].position;
        WaypointsIndex = (WaypointsIndex + 1) % Waypoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Agent.pathPending && Agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }
}
