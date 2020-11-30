using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialise : MonoBehaviour
{
    public GameObject[] npcs;
    private const int NUM_NPCS = 3;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NUM_NPCS; ++i)
        {
            string tag = "waypoints" + i;
            GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
            Instantiate(npcs[i], points[0].transform.position, Quaternion.identity);
        }
        // GameObject[] obs = GameObject.FindGameObjectsWithTag("waypoints1");
        // Instantiate(ob, obs[0].transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
