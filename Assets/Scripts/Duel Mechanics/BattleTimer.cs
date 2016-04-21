using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class BattleTimer : MonoBehaviour
{
    private DuelController dc;
    public int battleDuration = 90;
    private int timeRemaining;
    private bool tickEngineStarted = false;
    private CharacterInformation player1Stats;
    private CharacterInformation player2Stats;
    private Health player1Health;
    private Health player2Health;
    public Text timer;

    void Start()
    {
        dc = GameObject.FindGameObjectWithTag("GameController").GetComponent<DuelController>();
        player1Stats = dc.player1.GetComponent<CharacterInformation>();
        player2Stats = dc.player2.GetComponent<CharacterInformation>();
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
        else if (dc.currentState == DuelStates.Battle)
        {
            player1Health = player1Stats.Health;
            player2Health = player2Stats.Health;
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
