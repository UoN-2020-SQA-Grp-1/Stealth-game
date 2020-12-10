using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;

namespace Tests
{
    public class NPCTests1
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("TestScene");
        }

        [UnityTest]
        public IEnumerator TestNPCWaypointMovement()
        {
            GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoints0");

            yield return null; //Wait for NPCs to be initialised
            NPC[] npcs = GameObject.FindObjectsOfType<NPC>();
            Debug.Log("Position before is: " + npcs[0].transform.position);
            Debug.Log("Expected position before is: " + waypoints[0].transform.position);
            Assert.AreNotEqual(npcs[0].transform.position, waypoints[0].transform.position);

            yield return new WaitForSeconds(2); //Wait for NPC to move past waypoint 0 and towards waypoint 1
            Debug.Log("Position after is: " + npcs[0].transform.position);
            Debug.Log("Expected position after is: " + waypoints[1].transform.position);
            Assert.AreNotEqual(npcs[0].transform.position, waypoints[0].transform.position);
        }

        [UnityTest]
        public IEnumerator TestNPCWaypointCycles()
        {
            GameObject[] waypoints = GameObject.FindGameObjectsWithTag("waypoints0");
            const float timeBetweenWaypoints = 3.5f;

            yield return null; //Wait for NPCs to be initialised
            NPC[] npcs = GameObject.FindObjectsOfType<NPC>();
            Assert.AreEqual(waypoints[0].transform, npcs[0].NextWaypoint);

            yield return new WaitForSeconds(timeBetweenWaypoints); //Wait for NPC to move past waypoint 0 and towards waypoint 1
            Assert.AreEqual(waypoints[1].transform, npcs[0].NextWaypoint);
        }
    }
}
