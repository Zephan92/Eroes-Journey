using UnityEngine;
using System.Collections;

public enum GameStates
{//Different Game States
   MainMenu,
   Overworld,
   Animation,
   Event,
   Battle,
   Credits,
   MultiplayerMenu,
   MultiplayerMode,
}

public class MasterGameController : MonoBehaviour {

    public static MasterGameController control;
    public GameStates currentState = GameStates.MainMenu;

    void Awake()
    {
        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case GameStates.Animation:
                break;
            case GameStates.Battle:
                break;
            case GameStates.Credits:
                break;
            case GameStates.Event:
                break;
            case GameStates.MainMenu:
                break;
            case GameStates.MultiplayerMenu:
                break;
            case GameStates.MultiplayerMode:
                break;
            case GameStates.Overworld:
                if (Input.GetButtonDown("Submit"))
                {
                    if (OverworldController.control.currentState != OverworldStates.Menu)
                    {
                        OverworldController.control.currentState = OverworldStates.Menu;
                        MenuManager.ShowMenu("Player Menu");
                    }
                }
                break;
            default:
                break;
        }
    }
}
