using UnityEngine;
using System.Collections;

public class NPCEventController : MonoBehaviour {

    public bool eventActivated = false;
    public bool eventResolved = false;
    public bool hideSpriteAtEventEnd = true;
    public bool showSprite = false;
    public bool npcMoving = false;
    public bool npcTalking = false;

    public EventDialog[] dialog;
    public Direction[] movementDirection;
    public int[] movementDistance;
    public float[] movementEndWait;
    public int[] dialogText;

    private GameObject _npc;
    private bool _showingSprite = false;
    private bool _inEvent = false;
    private bool[] _sectionFinished;
    private int _currentSection = 0;
    private bool currentlyInSection = false;

    public void Start()
    {
        toggleSpriteVisibility(showSprite);
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
            toggleSpriteVisibility(true);
            showSprite = true;
            _inEvent = true;
        }
        else if(!showSprite && _showingSprite)
        {
            toggleSpriteVisibility(false);
        }

        if (currentlyInSection && !npcMoving && !npcTalking)
        {
            Invoke("EndWait", movementEndWait[_currentSection]);
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
                    currentlyInSection = true;
                    _npc.GetComponent<NPCOverworldController>().setDestinationEvent(movementDirection[_currentSection], movementDistance[_currentSection], movementEndWait[_currentSection], gameObject);                 
                    if(dialogText[_currentSection] >= 0)
                        dialog[dialogText[_currentSection]].StartDialog(dialogText[_currentSection], gameObject);
                }
            }

            if (_sectionFinished[_sectionFinished.Length - 1])
            {
                if(hideSpriteAtEventEnd)
                    showSprite = false;

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

    private void toggleSpriteVisibility(bool visibility)
    {
        foreach (Renderer childRenderer in gameObject.GetComponentsInChildren<Renderer>())
            childRenderer.enabled = visibility;
        foreach (BoxCollider childBoxCollider in gameObject.GetComponentsInChildren<BoxCollider>())
            childBoxCollider.enabled = visibility;
        _showingSprite = visibility;
    }

}
