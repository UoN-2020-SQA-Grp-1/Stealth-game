using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Lib;
using Zenject;

public class EnemyTakeDown : MonoBehaviour
{
    [Inject]
    private IInputReader InputReader;
    public float TakeDownRange = 3f;
    private bool InputDisabled = false;

    // Update is called once per frame
    void Update()
    {
        if (InputDisabled)
            return;
        if (InputReader.getButtonDown("Submit"))
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

    public void DisableInput()
    {
        InputDisabled = true;
    }
}
