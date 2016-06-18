using UnityEngine;
using System.Collections;

public class NPCEventController : MonoBehaviour {

    public bool eventActivated = false;
    public bool eventResolved = false;
    public bool showSprite = false;
    public float transitionTime = 0.3f;
    public EventDialog[] dialog;
    public Direction[] movementDirection;
    public int[] movementDistance;
    public float[] movementEndWait;
    

    private GameObject _npc;
    private bool _showingSprite = false;
    private bool _inEvent = false;
    private bool[] _sectionFinished;
    private int _currentSection = 0;
    private bool currentlyInSection = false;

    public void Start()
    {
        gameObject.GetComponentInChildren<Renderer>().enabled = false;
        gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
        _npc = gameObject;
    }

    public void Awake()
    {
        _sectionFinished = new bool[movementDirection.Length];
        for (int i = 0; i < movementDirection.Length; i++)
        {
            _sectionFinished[i] = false;
        }
    }

    public void Update()
    {
        if (showSprite && !_showingSprite || eventActivated)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = true;
            gameObject.GetComponentInChildren<BoxCollider>().enabled = true;
            showSprite = true;
            _showingSprite = true;
            _inEvent = true;
        }
        else if(!showSprite && _showingSprite)
        {
            gameObject.GetComponentInChildren<Renderer>().enabled = false;
            gameObject.GetComponentInChildren<BoxCollider>().enabled = false;
            _showingSprite = false;
        }
    }

    public void FixedUpdate()
    {
        if (_inEvent)
        {
            if (!_sectionFinished[_currentSection])
            {
                if (!_npc.GetComponent<NPCOverworldController>().Moving && !currentlyInSection)
                {
                    _npc.GetComponent<NPCOverworldController>().setDestinationEvent(movementDirection[_currentSection], movementDistance[_currentSection], movementEndWait[_currentSection], gameObject);
                    currentlyInSection = true;
                }
            }

            if (_sectionFinished[_sectionFinished.Length - 1])
            {
                _inEvent = false;
                eventActivated = false;
                eventResolved = true;
            }
        }
    }

    public void EndWait()
    {
        currentlyInSection = false;
        _sectionFinished[_currentSection] = true;
        if (_currentSection < _sectionFinished.Length - 1)
            _currentSection++;
    }
}
