using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialise : MonoBehaviour
{
    public GameObject[] NPCs;
    public GameObject player;
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
        //Debug.Log("Start position is " + startPoint.transform.position);
        Instantiate(player, startPoint.transform.position, Quaternion.identity);
        NPC.NPCCount = 0;
        for (int i = 0; i < NPCs.Length; ++i)
        {
            string tag = "waypoints" + i;
            GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
            Instantiate(NPCs[i], points[0].transform.position, Quaternion.identity);
        }
    }
}
