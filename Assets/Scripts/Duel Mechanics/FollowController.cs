using UnityEngine;
using System.Collections;

public class FollowController : MonoBehaviour {
    public GameObject controllerToFollow;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector3(controllerToFollow.transform.position.x, transform.position.y, transform.position.z);
	}
}
