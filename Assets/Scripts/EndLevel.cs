using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;
using Assets.Lib;

public class EndLevel : MonoBehaviour
{
    [Inject]
    private ITextDisplayer textDisplayer;
    public enum EndLevelAction
    {
        NextLevel,
        GameFinished
    }
    public EndLevelAction Action;
    public string NextLevel;

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
            textDisplayer.ShowText("You have beaten the game, congratulations!");
        }
    }
}
