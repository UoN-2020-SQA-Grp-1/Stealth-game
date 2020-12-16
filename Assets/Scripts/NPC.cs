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
    private int ID;
    private int TempWaypoint = -1;

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
            // If a temporary waypoint has been added, i.e. if the NPC is moving
            // towards where they heard a player, it removes that waypoint again
            // once they've reached that temporary waypoint, so it can continue 
            // on its old patrol pattern.
            if (TempWaypoint != -1)
            {
                Debug.Log("Removing temp waypoint " + TempWaypoint);
                Waypoints.RemoveAt(TempWaypoint);
                TempWaypoint = -1;
            }
            GoToNextWaypoint();
        }
    }

    public void Alert(Transform player)
    {
        Debug.Log("NPC Alerted!");
        if (Waypoints.Count == 0)
            return;
        TempWaypoint = WaypointsIndex;
        Waypoints.Insert(TempWaypoint, player);
        GoToNextWaypoint();
    }
}
