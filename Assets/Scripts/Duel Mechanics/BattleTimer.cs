using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BattleTimer : MonoBehaviour
{
    public int battleDuration = 90;
    private int timeRemaining;
    private bool tickEngineStarted = false;
    public Text timer;

    void Start()
    {
        timeRemaining = battleDuration;
        timer.text = convertToMinute(timeRemaining);
        
    }

    void Update() {
        if (DuelController.control.currentState == DuelStates.Start)
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
        if(DuelController.control.currentState == DuelStates.Battle)
        { 
			timeRemaining--;
			timer.text = convertToMinute(battleDuration);
            if (timeRemaining <= 0)
            {
                DuelController.control.currentState = DuelStates.Decision;
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
