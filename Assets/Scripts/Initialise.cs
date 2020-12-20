using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Assets.Lib;

public class Initialise : MonoBehaviour
{
    public GameObject[] NPCs;
    public GameObject Player;

    [Inject]
    private DiContainer Container;
    // Start is called before the first frame update
    void Start()
    {
        // Resets the static NPCCount used for assigning their IDS
        // Ensures they start from ID 0 again when switching levels
        GameObject startPoint = GameObject.FindGameObjectWithTag("startPoint");
        if (startPoint == null)
        {
            Debug.Log("Start point not set!");
        }
        Container.InstantiatePrefab(Player, startPoint.transform.position, Quaternion.identity, null);
        NPC.NPCCount = 0;
        for (int i = 0; i < NPCs.Length; ++i)
        {
            string tag = "waypoints" + i;
            GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
            Container.InstantiatePrefab(NPCs[i], points[0].transform.position, Quaternion.identity, null);
        }
    }
}
