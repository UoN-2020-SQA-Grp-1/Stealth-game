using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NSubstitute;
using Assets.Lib;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class EndLevelTests : Base
    {
        [UnityTest]
        public IEnumerator TestOtherObjectsDontActivate()
        {
            yield return LoadScene("TestScene");
            EndLevel endPoint = StaticContext.Container.InstantiatePrefab(Resources.Load("EndPOint"),
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    null).GetComponent<EndLevel>();
            endPoint.Action = EndLevel.EndLevelAction.GameFinished;

            var npc = StaticContext.Container.InstantiatePrefab(Resources.Load("NPC"),
                    new Vector3(-100, 0, 0),
                    Quaternion.identity,
                    null);
            yield return null;
            textDisplayer.DidNotReceive().ShowText(Arg.Any<string>());
        }

        [UnityTest]
        public IEnumerator TestGameFinishedDisplaysEndText()
        {
            yield return LoadScene("TestScene");

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            EndLevel endPoint = StaticContext.Container.InstantiatePrefab(Resources.Load("EndPOint"),
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    null).GetComponent<EndLevel>();
            endPoint.Action = EndLevel.EndLevelAction.GameFinished;

            player.transform.position = new Vector3(-10, 0, 0);

            yield return new WaitForSeconds(3);
            textDisplayer.DidNotReceive().ShowText(Arg.Any<string>());
            
            //Move towards trigger area 
            for(float i = -10; i < 0; i += 0.01f)
            {
                player.transform.position = new Vector3(i, 0, 0);
                yield return null;
            }
            textDisplayer.Received().ShowText(Arg.Any<string>());
        }

        [UnityTest]
        public IEnumerator TestNextLevelLoadsNextLevel()
        {
            yield return LoadScene("TestScene");

            GameObject player = GameObject.FindGameObjectWithTag("Player");

            EndLevel endPoint = StaticContext.Container.InstantiatePrefab(Resources.Load("EndPOint"),
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    null).GetComponent<EndLevel>();
            endPoint.Action = EndLevel.EndLevelAction.NextLevel;
            endPoint.NextLevel = "Level1";

            player.transform.position = new Vector3(-10, 0, 0);

            //Move towards trigger area 
            for (float i = -10; i < 0 && player != null; i += 0.01f)
            {
                player.transform.position = new Vector3(i, 0, 0);
                yield return null;
            }

            Assert.AreEqual(SceneManager.GetActiveScene().name, "Level1");
        }
    }
}