using UnityEngine;
using System.Collections;

public class NPCOverworldController : MonoBehaviour {

    public int pixelToUnits = 16;
    public Direction npcDirection = Direction.None;
    public Direction lookingDirection = Direction.Down;
    public BoxCollider[] boundaries = new BoxCollider[4];

    private Animator _animator;
    private Vector3 _move;
    private Vector2 _destination;
    private BoxCollider _boundary;
    private GameObject _eventGameObject;
    private float _eventTransitionTime;

    public bool Left
    {
        get { return _animator.GetBool("Left"); }
        set { _animator.SetBool("Left", value); }
    }
    public bool Right
    {
        get { return _animator.GetBool("Right"); }
        set { _animator.SetBool("Right", value); }
    }
    public bool Up
    {
        get { return _animator.GetBool("Up"); }
        set { _animator.SetBool("Up", value); }
    }
    public bool Down
    {
        get { return _animator.GetBool("Down"); }
        set { _animator.SetBool("Down", value); }
    }
    public bool Moving
    {
        get { return _animator.GetBool("Moving"); }
        set { _animator.SetBool("Moving", value); }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _boundary = GetComponentInChildren<BoxCollider>();
        Down = true;
        Up = false;
        Left = false;
        Right = false;
        Moving = false;
        updateBoundaries(1.5f);
    }

    void Update()
    {
        switch (OverworldController.control.currentState)
        {
            case OverworldStates.EnteringOverworld:
                break;
            case OverworldStates.Menu:
                break;
            case OverworldStates.Transition:
                break;
            case OverworldStates.Wander:
                break;
        }
    }

    void FixedUpdate()
    {
        if (Moving)
        {
            updateBoundaries(0.5f);

            if (_destination == new Vector2(transform.position.x, transform.position.y))
            {
                if (_eventGameObject != null)
                {
                    Invoke("EndWait", _eventTransitionTime);
                }
                Moving = false;
                cleanBoundaries();
                updateBoundaries(1.5f);
            }
            else
            {
                movePlayer(npcDirection);
            }
        }
    }

    public void setDestinationEvent(Direction dir, int distance, float transitionTime, GameObject eGO)
    {
        _eventTransitionTime = transitionTime;
        _eventGameObject = eGO;
        Moving = true;
        switch (dir)
        {
            case Direction.Up:
                _destination = new Vector2(transform.position.x, transform.position.y + 16 * distance);
                break;
            case Direction.Down:
                _destination = new Vector2(transform.position.x, transform.position.y - 16 * distance);
                break;
            case Direction.Right:
                _destination = new Vector2(transform.position.x + 16 * distance, transform.position.y);
                break;
            case Direction.Left:
                _destination = new Vector2(transform.position.x - 16 * distance, transform.position.y);
                break;
            case Direction.None:
                _destination = new Vector2(transform.position.x, transform.position.y);
                Moving = false;
                Invoke("EndWait", _eventTransitionTime);
                break;
        }
        if (dir != Direction.None)
            lookingDirection = npcDirection = dir;
        else
            lookingDirection = dir;
        updateDirectionBools(npcDirection);
    }

    public void updateBoundaries(float colliderLength)
    {
        _boundary.size = new Vector3(colliderLength, colliderLength, 1);
    }

    public void cleanBoundaries()
    {
        for (int i = 0; i < boundaries.Length; i++)
            boundaries[i] = null;
    }

    public void updateDirectionBools(Direction dir)
    {
        Down = false;
        Up = false;
        Left = false;
        Right = false;

        switch (dir)
        {
            case Direction.Right:
                Right = true;
                break;

            case Direction.Left:
                Left = true;
                break;

            case Direction.Up:
                Up = true;
                break;

            case Direction.Down:
                Down = true;
                break;

            default:
                Down = true;
                break;
        }
    }

    public void movePlayer(Direction dir)
    {
        switch (dir)
        {
            case Direction.Right:
                _move = Vector3.right;
                break;

            case Direction.Left:
                _move = Vector3.left;
                break;

            case Direction.Up:
                _move = Vector3.up;
                break;

            case Direction.Down:
                _move = Vector3.down;
                break;

            default:
                _move = Vector3.zero;
                break;
        }

        transform.position += _move * pixelToUnits;
    }

    public bool checkForBoundary(Direction dir)
    {
        switch (dir)
        {
            case Direction.Right:
                if (boundaries[0] != null)
                    return true;
                break;

            case Direction.Left:
                if (boundaries[1] != null)
                    return true;
                break;

            case Direction.Up:
                if (boundaries[2] != null)
                    return true;
                break;

            case Direction.Down:
                if (boundaries[3] != null)
                    return true;
                break;

            default:
                return false;
        }
        return false;
    }

    private void EndWait()
    {
        _eventGameObject.GetComponent<NPCEventController>().EndWait();
        _eventGameObject = null;
    }
}