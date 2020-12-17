using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevel : MonoBehaviour
{
    public enum EndLevelAction
    {
        NextLevel,
        GameFinished
    }
    public EndLevelAction Action;
    public string NextLevel;
    public Text TextUI;

    private void OnTriggerEnter(Collider other)
    {
        if (Action == EndLevelAction.NextLevel)
        {
            SceneManager.LoadScene(NextLevel);
        }
        else
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            //playerMovement.InputDisabled = true;
            playerMovement.DisableInput();
            TextUI.text = "You have beaten the game, congratulations!";
            TextUI.gameObject.SetActive(true);
        }
    }
}
