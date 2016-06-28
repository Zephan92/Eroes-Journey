using UnityEngine;
using System.Collections;

public class Changemap : MonoBehaviour {
    public GameObject MapEntry;
    public bool isEntry = true;
    public Direction EntryDirection = Direction.Down;
    private Transform player;
    public GameObject parentMap;
    public GameObject entryMap;
    private bool changingMaps = false;

    void OnTriggerStay(Collider other)
    {
        if (!isEntry || !(other.transform.position.x == transform.position.x && other.transform.position.y == transform.position.y) || changingMaps)
            return;
        changingMaps = true;
        player = other.transform.parent;
        OverworldController.control.currentState = OverworldStates.Transition;
        activateNewMap();
        Invoke("changePlayerPosition", 0.5f);
        Invoke("deactivateOldMap", 1f);
    }

    void changePlayerPosition()
    {
        player.position = new Vector3(MapEntry.transform.position.x, MapEntry.transform.position.y, player.position.z);
        player.GetComponent<OverworldPlayerController>().updateDirectionBools(EntryDirection);
        OverworldController.control.currentState = OverworldStates.Wander;
        changingMaps = false;
    }

    void activateNewMap()
    {
        entryMap.SetActive(true);
    }

    void deactivateOldMap()
    {
        parentMap.SetActive(false);
    }
}
