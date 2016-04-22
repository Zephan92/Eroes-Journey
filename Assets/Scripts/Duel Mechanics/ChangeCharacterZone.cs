using UnityEngine;
using System.Collections;

public class ChangeCharacterZone : MonoBehaviour {
    public Zones zone = Zones.GroundMiddle;

    void OnTriggerEnter(Collider other)
    {
       CharacterInformation ci =  other.GetComponent<CharacterInformation>();
       ci.Zone = zone;
    }
}
