using Zenject;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NSubstitute;

namespace Tests
{
    public class DisplayInstructionTests : Base
    {
        [UnityTest]
        public IEnumerator TestsActivatesUponEntry()
        {
            yield return LoadScene("TestScene");
            DisplayInstruction displayInst = StaticContext.Container.InstantiatePrefab(Resources.Load("TextTrigger"),
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    null).GetComponent<DisplayInstruction>();
            displayInst.DisplayText = "TestText";
            displayInst.EventType = DisplayInstruction.OnTriggerEvent.SetActive;
            displayInst.gameObject.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);


            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //Move towards trigger area 
            for (float i = -10; i < 0; i += 0.01f)
            {
                player.transform.position = new Vector3(i, 0, 0);
                yield return null;
            }

            textDisplayer.Received().ShowText("TestText");
        }

        [UnityTest]
        public IEnumerator TestsDeactivatesUponExit()
        {
            yield return LoadScene("TestScene");
            DisplayInstruction displayInst = StaticContext.Container.InstantiatePrefab(Resources.Load("TextTrigger"),
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    null).GetComponent<DisplayInstruction>();
            displayInst.DisplayText = "TestText";
            displayInst.EventType = DisplayInstruction.OnTriggerEvent.SetActive;
            displayInst.gameObject.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);


            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //Move towards trigger area 
            for (float i = -10; i < 0; i += 0.01f)
            {
                player.transform.position = new Vector3(i, 0, 0);
                yield return null;
            }

            textDisplayer.Received().ShowText("TestText");

            for (float i = 0; i < 10; i += 0.01f)
            {
                player.transform.position = new Vector3(i, 0, 0);
                yield return null;
            }

            textDisplayer.Received().HideText();
        }

        [UnityTest]
        public IEnumerator TestsDeactivatesUponEntryIfFlagSet()
        {
            yield return LoadScene("TestScene");
            DisplayInstruction displayInst = StaticContext.Container.InstantiatePrefab(Resources.Load("TextTrigger"),
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    null).GetComponent<DisplayInstruction>();
            displayInst.DisplayText = "TestText";
            displayInst.EventType = DisplayInstruction.OnTriggerEvent.SetActive;
            displayInst.gameObject.GetComponent<BoxCollider>().size = new Vector3(3, 3, 3);


            GameObject player = GameObject.FindGameObjectWithTag("Player");
            //Move towards trigger area 
            for (float i = -10; i < 0; i += 0.01f)
            {
                player.transform.position = new Vector3(i, 0, 0);
                yield return null;
            }

            textDisplayer.Received().ShowText("TestText");
        }
    }
}