using UnityEngine;
using System.Collections;

public class OverworldEvent : MonoBehaviour
{
    public int eventNumber = 0;
    public bool eventActivated = false;
    public bool deactivateAfterEvent = true;

    public float transitionTime = 0.3f;
    public NPCEventController [] npcEventControllers;
    public Direction[] movementDirection;
    public int[] movementDistance;
    public float[] movementEndWait;

    private GameObject _player;
    private bool _inEvent = false;
    private bool[] _sectionFinished;
    private int _currentSection = 0;
    private bool currentlyInSection = false;
    public bool _eventFullyResolved = false;

    public void Awake()
    {
        _sectionFinished = new bool[movementDirection.Length];
        for (int i = 0; i < movementDirection.Length; i++)
        {
            _sectionFinished[i] = false;
        }
    }

    public void Start()
    {
        eventActivated = JourneyStats.Stats.events[eventNumber];
    }

    public void OnTriggerStay(Collider other)
    {
        if (_inEvent
            || !(other.transform.position.x == transform.position.x && other.transform.position.y == transform.position.y)
            || OverworldController.control.currentState == OverworldStates.Event
            || !eventActivated)
            return;

        OverworldController.control.currentState = OverworldStates.Event;
        _player = other.transform.parent.gameObject;
        _inEvent = true;
        for (int i = 0; i < npcEventControllers.Length; i++)
        {
            npcEventControllers[i].eventActivated = true;
        }
    }

    public void FixedUpdate()
    {
        if (JourneyStats.Stats.events[eventNumber])
            eventActivated = true;

        for (int i = 0; i < npcEventControllers.Length; i++)
        {
            if (!npcEventControllers[i].eventResolved)
                break;
            else if (npcEventControllers[i].eventResolved && npcEventControllers.Length - 1 == i)
            {
                _eventFullyResolved = true;
            }
        }
        if (_inEvent)
        {
            if (!_sectionFinished[_currentSection])
            {
                if (!_player.GetComponent<OverworldPlayerController>().Moving && !currentlyInSection)
                {
                    _player.GetComponent<OverworldPlayerController>().setDestinationEvent(movementDirection[_currentSection], movementDistance[_currentSection], movementEndWait[_currentSection], gameObject);
                    currentlyInSection = true;
                }
            }

            if (_sectionFinished[_sectionFinished.Length - 1] && _eventFullyResolved)
            {
                _inEvent = false;
                if (deactivateAfterEvent)
                {
                    JourneyStats.Stats.events[eventNumber] = eventActivated = false;
                }
                else
                {
                    for (int i = 0; i < _sectionFinished.Length; i++)
                        _sectionFinished[i] = false;
                    _currentSection = 0;
                }
                OverworldController.control.currentState = OverworldStates.Transition;
                _player.GetComponent<OverworldPlayerController>().updateBoundaries(0.5f);
                Invoke("TransitionToWander", transitionTime + 0.1f);//buffer to make sure it goes to next frame even if 0.0f
            }
        }
    }

    public void EndWait()
    {
        currentlyInSection = false;
        _sectionFinished[_currentSection] = true;
        if(_currentSection < _sectionFinished.Length - 1)
            _currentSection++;
    }

    private void TransitionToWander()
    {

        _player.GetComponent<OverworldPlayerController>().cleanBoundaries();
        _player.GetComponent<OverworldPlayerController>().updateBoundaries(1.5f);
        OverworldController.control.currentState = OverworldStates.Wander;
    }
}
