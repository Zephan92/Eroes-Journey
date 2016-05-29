using UnityEngine;
using System.Collections;

public class BoundaryTrigger : MonoBehaviour {
    private BoxCollider[] boundaries;

    void Start()
    {
        boundaries = GetComponentInParent<OverworldPlayerController>().boundaries;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Gateway"))
        {
            return;
        }
            

        if (other.transform.position - new Vector3(0,0, other.transform.position.z) == transform.position + new Vector3(16,0,0))
        {
            boundaries[0] = other.GetComponent<BoxCollider>();
        }
        else if (other.transform.position - new Vector3(0, 0, other.transform.position.z) == transform.position + new Vector3(-16, 0, 0))
        {
            boundaries[1] = other.GetComponent<BoxCollider>();
        }
        else if (other.transform.position - new Vector3(0, 0, other.transform.position.z) == transform.position + new Vector3(0, 16, 0))
        {
            boundaries[2] = other.GetComponent<BoxCollider>();
        }
        else if (other.transform.position - new Vector3(0, 0, other.transform.position.z) == transform.position + new Vector3(0, -16, 0))
        {
            boundaries[3] = other.GetComponent<BoxCollider>();
        }
    }
}
