using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;
using Zenject;

public class EnemyTakeDown : MonoBehaviour
{
    [Inject]
    private IInputReader _inputReader;
    public float TakeDownRange = 3f;

    // Update is called once per frame
    void Update()
    {
        if (_inputReader.getButtonDown("Submit"))
        {
            RaycastHit hit;
            // Sends a ray from camera to the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(new Vector3 (Screen.width, Screen.height) / 2);
            if (Physics.Raycast(ray, out hit, TakeDownRange))
            {
                // Checks that it collides with an NPC.
                BoxCollider npc = hit.collider.GetComponent<BoxCollider>();
                if (npc != null && npc.gameObject.tag == "NPC")
                {
                    npc.gameObject.GetComponent<NPC>().Kill();
                    Destroy(npc.gameObject);
                }
            }
        }
    }
}
