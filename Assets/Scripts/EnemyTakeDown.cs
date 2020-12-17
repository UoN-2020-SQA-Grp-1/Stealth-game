using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;

public class EnemyTakeDown : MonoBehaviour
{
    public IInputReader InputReader = new InputReader();
    public float TakeDownRange = 3f;

    // Update is called once per frame
    void Update()
    {
        if (InputReader.getButtonDown("Submit"))
        {
            RaycastHit hit;
            // Sends a ray from camera to the mouse position.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
