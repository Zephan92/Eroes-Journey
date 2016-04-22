using System;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour {
    private CharacterInformation ci;
    private Zones lastZone;
    private GameObject tracker;
    private int numRows = 1;
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
        else
        {
            for (int r = 0; r < numRows; r++)
            {
                for (int c = 0; c < numRows; c++)
                {

                }
            }
           GameObject tp = Instantiate(Resources.Load("Tracker Panel")) as GameObject;
        }
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
        Debug.Log("Changing Display");
    }
}
