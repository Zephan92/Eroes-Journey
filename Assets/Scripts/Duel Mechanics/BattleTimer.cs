using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BattleTimer : MonoBehaviour
{
    private DuelController dc;
    public int battleDuration = 90;
    private int timeRemaining;
    private bool tickEngineStarted = false;
    public Text timer;

    void Start()
    {
        dc = GetComponent<DuelController>();
        timeRemaining = battleDuration;
        timer.text = convertToMinute(timeRemaining);
        
    }

    void Update() {
        if (dc.currentState == DuelStates.Start)
        {
            if (!tickEngineStarted)
            {
                tickEngineStarted = true;
                startTickEngine();
            }
        }
	}

	private void startTickEngine()
	{
        Invoke ("_tick", 1f);
	}

	private void _tick()
	{
        if(dc.currentState == DuelStates.Battle)
        { 
			timeRemaining--;
			timer.text = convertToMinute(battleDuration);
            if (timeRemaining <= 0)
            {
                dc.currentState = DuelStates.Decision;
            }
            else
            {
                Invoke("_tick", 1f);
            }
		} else {
            Invoke ("_tick", 1f);
		}
	}

	private string convertToMinute(int remaining)
	{
		int seconds = timeRemaining % 60;
		int minutes = (timeRemaining - seconds) / 60;
		string secStr = seconds.ToString().PadLeft(2,'0');
		return minutes + ":" + secStr;
	}
}
