using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Assets.Lib;
using NSubstitute;

namespace Tests
{
    public class InputTests
    {
        [SetUp]
        public void LoadScene()
        {
            SceneManager.LoadScene("TestScene");
        }

        public void assertSimilar(float a, float b)
        {
            Assert.IsTrue(Mathf.Abs(a - b) < 0.0001);
        }

        [UnityTest]
        public IEnumerator TestMaximumViewUp()
        {
            GameObject player = GameObject.Find("Player");
            GameObject camera = player.transform.Find("Main Camera").gameObject;

            var sub = Substitute.For<IInputReader>();
            sub.getMouseX().Returns(0);
            sub.getMouseY().Returns(1);

            player.GetComponent<PlayerMovement>().InputReader = sub;
            camera.GetComponent<MouseLook>().InputReader = sub;

            yield return new WaitForSeconds(4);

            Assert.AreEqual(camera.transform.rotation.eulerAngles.x, 270);
        }

        [UnityTest]
        public IEnumerator TestMaximumViewDown()
        {
            GameObject player = GameObject.Find("Player");
            GameObject camera = player.transform.Find("Main Camera").gameObject;

            var sub = Substitute.For<IInputReader>();
            sub.getMouseX().Returns(0);
            sub.getMouseY().Returns(-1);

            player.GetComponent<PlayerMovement>().InputReader = sub;
            camera.GetComponent<MouseLook>().InputReader = sub;

            yield return new WaitForSeconds(4);

            Assert.AreEqual(90, camera.transform.rotation.eulerAngles.x);
        }

        [UnityTest]
        public IEnumerator TestMoveForward()
        {
            GameObject player = GameObject.Find("Player");
            GameObject camera = player.transform.Find("Main Camera").gameObject;

            var startingPos = player.transform.position;

            var sub = Substitute.For<IInputReader>();
            sub.getMouseX().Returns(0);
            sub.getMouseY().Returns(0);
            sub.getMoveForwards().Returns(1);
            sub.getMoveSide().Returns(0);

            player.GetComponent<PlayerMovement>().InputReader = sub;
            camera.GetComponent<MouseLook>().InputReader = sub;

            yield return new WaitForSeconds(1);

            assertSimilar(startingPos.x, player.transform.position.x);
            assertSimilar(startingPos.y, player.transform.position.y);
            Assert.Less(startingPos.z, player.transform.position.z);
        }

        [UnityTest]
        public IEnumerator TestMoveForwardRelative()
        {
            GameObject player = GameObject.Find("Player");
            GameObject camera = player.transform.Find("Main Camera").gameObject;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            var startingPos = player.transform.position;

            var sub = Substitute.For<IInputReader>();
            sub.getMouseX().Returns(0);
            sub.getMouseY().Returns(0);
            sub.getMoveForwards().Returns(1);
            sub.getMoveSide().Returns(0);

            player.GetComponent<PlayerMovement>().InputReader = sub;
            camera.GetComponent<MouseLook>().InputReader = sub;

            yield return new WaitForSeconds(1);

            assertSimilar(startingPos.z, player.transform.position.z);
            assertSimilar(startingPos.y, player.transform.position.y);
            Assert.Less(startingPos.x, player.transform.position.x);
        }
    }
}
