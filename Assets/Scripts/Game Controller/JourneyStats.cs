using UnityEngine;
using System.Collections;

public class JourneyStats : MonoBehaviour
{

    public static JourneyStats Stats;

    void Awake()
    {
        if (Stats == null)
        {
            Stats = this;
        }
        else if (Stats != this)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i<events.Length; i++)
            events[i] = false;
    }

    void Start()
    {
        events[0] = true;
    }


    //Player Stats

    //Inventory

    //Events

    public bool[] events = new bool[2];
    //First Map
    //0. Mountain Speech
    //1. 
}
