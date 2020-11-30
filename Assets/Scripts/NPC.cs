using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public GameObject ob;
    public List<Transform> waypoints;
    private int waypointsIndex;
    private NavMeshAgent agent;
    private static int npcCount = 0;
    private int id;

    // Start is called before the first frame update
    void Start()
    {
        id = npcCount++;
        agent = GetComponent<NavMeshAgent>();
        agent.autoBraking = false;
        string tag = "waypoints" + id;
        GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject p in points)
        {
            waypoints.Add(p.transform);        
        }
        GoToNextWaypoint();
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Count == 0)
        {
            Debug.Log("No waypoints in waypoints array");
            return;
        }
        agent.destination = waypoints[waypointsIndex].position;
        waypointsIndex = (waypointsIndex + 1) % waypoints.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            GoToNextWaypoint();
        }
    }
}
