using UnityEngine;
using System.Collections;

public enum OverworldStates
{
    Menu,
    Transition,
    Wander,
}

public class OverworldController : MonoBehaviour {

    public static OverworldController control;
    public OverworldStates currentState = OverworldStates.Transition;

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
            case OverworldStates.Menu:
                break;
            case OverworldStates.Transition:
                break;
            case OverworldStates.Wander:
                break;
            default:
                break;
        }
    }
}
