using UnityEngine;
using System.Collections;

public enum OverworldStates
{
    EnteringOverworld,
    Menu,
    Transition,
    Wander,
    Event,
}

public class OverworldController : MonoBehaviour {

    public static OverworldController control;
    public OverworldStates currentState = OverworldStates.EnteringOverworld;

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
            case OverworldStates.EnteringOverworld:
                control.currentState = OverworldStates.Transition;
                Invoke("TransitionToWander",0.4f);
                break;
            case OverworldStates.Menu:
                break;
            case OverworldStates.Transition:
                break;
            case OverworldStates.Wander:
                if (Input.GetButtonDown("Submit"))
                {
                    control.currentState = OverworldStates.Menu;
                    MenuManager.ShowMenu("Player Menu");
                }
                break;
            case OverworldStates.Event:
                break;
            default:
                break;
        }
    }

    private void TransitionToWander()
    {
        control.currentState = OverworldStates.Wander;
    }
}
