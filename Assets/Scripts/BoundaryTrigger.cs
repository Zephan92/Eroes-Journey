using UnityEngine;
using System.Collections;

public class BoundaryTrigger : MonoBehaviour {
    private BoxCollider [] boundaries;

    void Start()
    {
        boundaries = GetComponentInParent<OverworldPlayerController>().boundaries;
    }
    
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player: " + transform.position + "\nOther: " + other.transform.position);

        if (other.transform.position.x > transform.position.x)
        {
            boundaries[0] = other.GetComponent<BoxCollider>();
        }
        else if (other.transform.position.x < transform.position.x)
        {
            boundaries[1] = other.GetComponent<BoxCollider>();
        }
        else if (other.transform.position.y > transform.position.y)
        {
            boundaries[2] = other.GetComponent<BoxCollider>();
        }
        else if (other.transform.position.y < transform.position.y)
        {
            boundaries[3] = other.GetComponent<BoxCollider>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        for(int i = 0; i < boundaries.Length; i++)
            if (other.GetComponent<BoxCollider>() == boundaries[i])
                boundaries[i] = null;
    }
}
