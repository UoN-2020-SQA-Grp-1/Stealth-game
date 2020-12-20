using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using Zenject;
using Assets.Lib;
using NSubstitute;

namespace Tests
{
    [TestFixture]
    public class NPCTests : Base
    {
        private bool EqualXZ(Vector3 l, Vector3 r)
        {
            return l.x == r.x && l.z == r.z;
        }

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

        [UnityTest]
        public IEnumerator TestNPCDeviatesToDetection()
        {
            yield return LoadScene("TestScene");

            NPC npc = GameObject.FindObjectsOfType<NPC>()[0];

            var alertPoint = new Vector3(-49, 0.5f, -60);
            npc.Alert(alertPoint);

            yield return null;

            Assert.AreEqual(npc.GetComponent<NavMeshAgent>().destination.x, alertPoint.x);
            Assert.AreEqual(npc.GetComponent<NavMeshAgent>().destination.z, alertPoint.z);
        }

        [UnityTest]
        public IEnumerator TestNPCReturnsToPath()
        {
            yield return LoadScene("TestScene");

            NPC npc = GameObject.FindObjectsOfType<NPC>()[0];
            var nav = npc.GetComponent<NavMeshAgent>();

            var normalWaypoint = new Vector3 (nav.destination.x, nav.destination.y, nav.destination.z);
            var alertPoint = new Vector3(-49, 0.5f, -60);
            npc.Alert(alertPoint);

            yield return null;

            yield return new WaitUntil(() => !EqualXZ(nav.destination, alertPoint));
            Assert.AreEqual(nav.destination, normalWaypoint);
        }
    }
}
