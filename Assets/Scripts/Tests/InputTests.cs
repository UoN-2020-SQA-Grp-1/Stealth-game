using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.SceneManagement;
using Assets.Lib;
using NSubstitute;
using Zenject;

namespace Tests
{
    [TestFixture]
    public class InputTests : Base
    {
        public void assertSimilar(float expected, float actual, float tolerance)
        {
            if (Mathf.Abs(expected - actual) > tolerance)
            {
                throw new AssertionException(string.Format("Expected : {0}, Actual {1} (tolerance {2})", expected, actual, tolerance));
            }
        }

        [UnityTest]
        public IEnumerator TestMaximumViewUp()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

            inputReader.getMouseX().Returns(0);
            inputReader.getMouseY().Returns(1);

            yield return new WaitForSeconds(4);

            Assert.AreEqual(camera.transform.rotation.eulerAngles.x, 270);
        }

        [UnityTest]
        public IEnumerator TestMaximumViewDown()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");

            inputReader.getMouseX().Returns(0);
            inputReader.getMouseY().Returns(-1);

            yield return new WaitForSeconds(4);

            Assert.AreEqual(90, camera.transform.rotation.eulerAngles.x);
        }

        [UnityTest]
        public IEnumerator TestMoveForward()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            var startingPos = player.transform.position;

            inputReader.getMoveForwards().Returns(1);

            yield return new WaitForSeconds(1);
            Debug.Log("Move forward Before y = " + startingPos.y + ", after y = " + player.transform.position.y);
            assertSimilar(startingPos.x, player.transform.position.x, 0.001f);
            assertSimilar(startingPos.y, player.transform.position.y, 0.001f);
            Assert.Less(startingPos.z, player.transform.position.z);
        }

        [UnityTest]
        public IEnumerator TestMoveForwardRelative()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            var startingPos = player.transform.position;

            inputReader.getMoveForwards().Returns(1);

            yield return new WaitForSeconds(1);

            Debug.Log("Move forward relative Before y = " + startingPos.y + ", after y = " + player.transform.position.y);
            assertSimilar(startingPos.z, player.transform.position.z, 0.001f);
            assertSimilar(startingPos.y, player.transform.position.y, 0.001f);
            Assert.Less(startingPos.x, player.transform.position.x);
        }
        [UnityTest]
        public IEnumerator TestCrouchMovesCamera()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            var startingCamHeight = camera.transform.position;

            inputReader.getButtonDown("Crouch").Returns(true, false);
            yield return null;
            Assert.Greater(startingCamHeight.y, camera.transform.position.y);

            inputReader.getButtonDown("Crouch").Returns(true, false);
            yield return null;
            Assert.AreEqual(startingCamHeight.y, camera.transform.position.y);
        }

        [UnityTest]
        public IEnumerator TestCrouchMovementSpeed()
        {
            yield return LoadScene("TestScene");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            var moveSpeed = movement.MovementSpeed;

            Assert.AreEqual(moveSpeed, movement.RunningSpeed);

            inputReader.getButtonDown("Crouch").Returns(true);

            yield return new WaitForSeconds(0);
            Assert.Less(movement.MovementSpeed, moveSpeed);
        }
    }
}
