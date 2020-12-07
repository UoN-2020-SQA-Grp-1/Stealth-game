using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialise : MonoBehaviour
{
    public GameObject[] NPCs;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < NPCs.Length; ++i)
        {
            string tag = "waypoints" + i;
            GameObject[] points = GameObject.FindGameObjectsWithTag(tag);
            Instantiate(NPCs[i], points[0].transform.position, Quaternion.identity);
        }
    }
}
