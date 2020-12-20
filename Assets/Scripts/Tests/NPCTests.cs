using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Zenject;
using Assets.Lib;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class NPCTests : Base
    {

        [UnityTest]
        public IEnumerator TestNPCWaypointMovement()
        {
            yield return LoadScene("TestScene");
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
            yield return LoadScene("TestScene");
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
