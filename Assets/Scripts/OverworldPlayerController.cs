using UnityEngine;
using System.Collections;

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
    None,
}

public class OverworldPlayerController : MonoBehaviour {
    private Animator _animator;
    public int pixelToUnits = 16;
    public float transitionTime = 0.4f;
    public Direction playerDirection = Direction.None;
    public Direction lookingDirection = Direction.Down;
    public BoxCollider[] boundaries = new BoxCollider[4];
    private Vector3 move;
    private Vector2 destination;

    public bool Left
    {
        get { return _animator.GetBool("left"); }
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
        Down = true;
        Up = false;
        Left = false;
        Right = false;
        Moving = false;
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") && !Moving)
        {
            if (Input.GetAxis("Horizontal") > 0)
            {
                destination = new Vector2(transform.position.x + 16, transform.position.y);
                playerDirection = Direction.Right;
            } 
            else
            {
                destination = new Vector2 (transform.position.x - 16, transform.position.y);
                playerDirection = Direction.Left;
            }
                
            lookingDirection = playerDirection;
            updateDirectionBools(playerDirection);
            if (!checkForBoundary(playerDirection))
            {
                Moving = true;
            }    
        }
        else if (Input.GetButton("Vertical") && !Moving)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                playerDirection = Direction.Up;
                destination = new Vector2(transform.position.x, transform.position.y + 16);
            }
                
            else
            {
                playerDirection = Direction.Down;
                destination = new Vector2(transform.position.x, transform.position.y - 16);
            }
                
            lookingDirection = playerDirection;
            updateDirectionBools(playerDirection);
            if (!checkForBoundary(playerDirection))
            {
                Moving = true;
            }
        }
        else if(!Moving)
        {
            playerDirection = Direction.None;
        }
    }

    void FixedUpdate() {
        if (Moving)
        {
            movePlayer(playerDirection);
            if (destination == new Vector2(transform.position.x, transform.position.y))
            {
                Moving = false;
            }
        }        
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
                move = Vector3.right;
                break;

            case Direction.Left:
                move = Vector3.left;
                break;

            case Direction.Up:
                move = Vector3.up;
                break;

            case Direction.Down:
                move = Vector3.down;
                break;

            default:
                move = Vector3.zero;
                break;
        }

        transform.position += move * pixelToUnits;
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
}
