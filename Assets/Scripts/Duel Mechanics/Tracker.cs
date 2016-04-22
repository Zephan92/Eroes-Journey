using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour {
    private CharacterInformation ci;
    private Zones lastZone;
    private GameObject tracker;
    private int numRows = 1;
    public List<GameObject> zones;

    void Start ()
    {
        ci = gameObject.GetComponent<CharacterInformation>();
        lastZone = ci.Zone;
        
        if (gameObject.tag.Equals("Player 1"))
        {
            tracker = GameObject.FindGameObjectWithTag("Tracker 1");
        }
        else
        {
            tracker = GameObject.FindGameObjectWithTag("Tracker 2");
        }

        if (ci.Pendants.ContainsKey("Double Jump"))
        {
            numRows = 3;
        }
        else if (ci.Pendants.ContainsKey("Jump"))
        {
            numRows = 2;
        }

        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                GameObject tp = Instantiate(Resources.Load("Tracker Panel")) as GameObject;
                tp.transform.SetParent(tracker.transform);
                tp.transform.localPosition = new Vector3(c*50,r*-50,0);
                zones.Add(tp);
                Debug.Log("Added image");
            }
        }
        
        zones[convertZoneToInt(ci.Zone)].GetComponent<Image>().color = Color.blue;
    }

    void Update()
    {
        if (ci.Zone != lastZone)
        {
            updateTrackerDisplay();
            lastZone = ci.Zone;
        }
    }

    public void updateTrackerDisplay()
    {
        int zNum = convertZoneToInt(ci.Zone);

        for (int i = 0; i < zones.Count; i++)
            if(i == zNum)
                zones[i].GetComponent<Image>().color = Color.blue;
            else
                zones[i].GetComponent<Image>().color = Color.white;
    }

    public int convertZoneToInt(Zones currentZone)
    {
        int x;
        int y;
        if (currentZone.ToString().Contains("Ground"))
            if (ci.Pendants.ContainsKey("Double Jump"))
                y = 6;
            else if (ci.Pendants.ContainsKey("Jump"))
                y = 3;
            else
                y = 0;
        else if (currentZone.ToString().Contains("Air"))
            if (ci.Pendants.ContainsKey("Double Jump"))
                y = 3;
            else 
                y = 0;
        else
            y = 0;

        if (currentZone.ToString().Contains("Long"))
            if (gameObject.tag.Equals("Player 1"))
                x = 0;
            else
                x = 2;
        else if (currentZone.ToString().Contains("Middle"))
            x = 1;
        else
            if (gameObject.tag.Equals("Player 1"))
                x = 2;
            else
                x = 0;
        return x + y;
    }
}
