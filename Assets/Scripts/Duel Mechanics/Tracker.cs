using System;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour {
    private Image[] zoneImages;
	// Use this for initialization
	void Start () {
        zoneImages = GetComponentsInChildren<Image>();
	}

    // Update is called once per frame
    public void changeTrackerZone(Zones zone)
    {
        foreach (Image z in zoneImages)
        {
            z.color = Color.white;
            if(z.GetComponent<ChangeCharacterZone>().zone == zone)
                z.color = new Color32(0, 120, 230, 255);
        }
    }
}
