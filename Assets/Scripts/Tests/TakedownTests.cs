using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using NSubstitute;
using Assets.Lib;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class TakedownTests : Base
    {
        [UnityTest]
        public IEnumerator TestTakedownInRange()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inputReader.getButtonDown("Submit").Returns(true);
            GameObject npc = GameObject.FindGameObjectWithTag("NPC");

            npc.transform.rotation = Quaternion.Euler(0,0,0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            npc.transform.position = player.transform.position + new Vector3(0, 0, 2);

            yield return null;


            yield return new WaitForSeconds(2);

            Assert.IsEmpty(GameObject.FindGameObjectsWithTag("NPC"));
        }

        [UnityTest]
        public IEnumerator TestTakedownOutRange()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inputReader.getButtonDown("Submit").Returns(true);
            GameObject npc = GameObject.FindGameObjectWithTag("NPC");

            npc.transform.rotation = Quaternion.Euler(0, 0, 0);
            player.transform.rotation = Quaternion.Euler(0, 0, 0);

            npc.transform.position = player.transform.position + new Vector3(0, 0, 5);

            yield return null;


            yield return new WaitForSeconds(2);

            Assert.IsNotEmpty(GameObject.FindGameObjectsWithTag("NPC"));
        }

        [UnityTest]
        public IEnumerator TestTakedownOffAxis()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            inputReader.getButtonDown("Submit").Returns(true);
            GameObject npc = GameObject.FindGameObjectWithTag("NPC");

            npc.transform.rotation = Quaternion.Euler(0, 0, 0);
            player.transform.rotation = Quaternion.Euler(0, 90, 0);

            npc.transform.position = player.transform.position + new Vector3(0, 0, 2);

            yield return null;


            yield return new WaitForSeconds(2);

            Assert.IsNotEmpty(GameObject.FindGameObjectsWithTag("NPC"));
        }
    }
}
