using UnityEngine;
using System.Collections;

public class ChangeCharacterZone : MonoBehaviour {
    public Zones zone = Zones.GroundMiddle;
    public GameObject trackerObject;
    private Tracker tracker;

    void Start()
    {
        tracker = trackerObject.GetComponent<Tracker>();
    }

    void OnTriggerEnter(Collider other)
    {
       CharacterInformation ci =  other.GetComponent<CharacterInformation>();
        ci.Zone = zone;
        tracker.changeTrackerZone(zone);
    }
}
