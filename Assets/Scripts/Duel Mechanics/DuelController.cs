using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public enum DuelStates
{
    PreBattle,
    Start,
    Battle,
    Paused,
    Decision,
    End,
}

public class DuelController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    private CharacterInformation player1Stats;
    private CharacterInformation player2Stats;
    public Text player1StatsText;
    public Text player2StatsText;
    public DuelStates currentState = DuelStates.PreBattle;
    private BattleTimer bt;

    private GameObject pausedUI, battleStartUI, KOUI, victoryUI, defeatUI, drawUI, readyUI,startUI;//UI's

    void Start()
    {
        bt = GetComponent<BattleTimer>();
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        player1Stats = player1.GetComponent<CharacterInformation>();
        player2Stats = player2.GetComponent<CharacterInformation>();
        pausedUI = GameObject.Find("Paused");
        battleStartUI = GameObject.Find("Battle Start");
        KOUI = GameObject.Find("KO");
        victoryUI = GameObject.Find("Victory");
        defeatUI = GameObject.Find("Defeat");
        drawUI = GameObject.Find("Draw");
        readyUI = GameObject.Find("Ready");
        startUI = GameObject.Find("Start");
        displayStats();
    }

    void Update()
    {
        if (currentState == DuelStates.PreBattle)
        {
            if (Input.GetButtonDown("Submit"))
            {
                currentState = DuelStates.Start;
                battleStartUI.GetComponent<CanvasGroup>().alpha = 0.0f;
                readyUI.GetComponent<CanvasGroup>().alpha = 1.0f;
            }
        }
        else if (currentState == DuelStates.Start)
        {
            Invoke("displayStartUI", 1.5f);
            Invoke("startBattle", 2f);
        }
        else if (currentState == DuelStates.Battle)
        {
                
            if (Input.GetButtonDown("Submit"))
            {
                currentState = DuelStates.Paused;
                pausedUI.GetComponent<CanvasGroup>().alpha = 1.0f;
            }

            if (player1Stats.Health.CurrentHealth <= 0 || player2Stats.Health.CurrentHealth <= 0)
            {
                KOUI.GetComponent<CanvasGroup>().alpha = 1.0f;
                currentState = DuelStates.Decision;
            }
        }
        else if (currentState == DuelStates.Decision)
        {
            float player1HealthRatio = player1Stats.Health.CurrentHealth / player1Stats.Health.TotalHealth;
            float player2HealthRatio = player2Stats.Health.CurrentHealth / player2Stats.Health.TotalHealth;
            if (player1HealthRatio == player2HealthRatio)
                Invoke("displayDraw", 2f);
            else if (player1HealthRatio >= player2HealthRatio)
                Invoke("displayVictory", 2f);
            else if (player1HealthRatio <= player2HealthRatio)
                Invoke("displayDefeat", 2f);
            currentState = DuelStates.End;
        }
        else if (currentState == DuelStates.End)
        {
            if (Input.GetButtonDown("Submit"))
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (currentState == DuelStates.Paused)
        {
            if (Input.GetButtonDown("Submit"))
            {
                currentState = DuelStates.Start;
                pausedUI.GetComponent<CanvasGroup>().alpha = 0.0f;
            }
        }
        else
        {
            Debug.Log("Error in DuelController State");
        }
    }

    private void startBattle()
    {
        currentState = DuelStates.Battle;
        startUI.GetComponent<CanvasGroup>().alpha = 0.0f;
    }

    private void displayStartUI()
    {
        readyUI.GetComponent<CanvasGroup>().alpha = 0.0f;
        startUI.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void displayDefeat()
    {
        KOUI.GetComponent<CanvasGroup>().alpha = 0.0f;
        defeatUI.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void displayVictory()
    {
        KOUI.GetComponent<CanvasGroup>().alpha = 0.0f;
        victoryUI.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void displayDraw()
    {
        KOUI.GetComponent<CanvasGroup>().alpha = 0.0f;
        drawUI.GetComponent<CanvasGroup>().alpha = 1.0f;
    }

    private void displayStats()
    {
        string player1Str;
        string player2Str;
        player1Str = "Player 1\n\n";
        player2Str = "Player 2\n\n";

        player1Str += "Type: " + player1Stats.Pendants["Duel Damage"].Name + "\n";
        player1Str += "Style: " + player1Stats.Pendants["Duel Style"].Name + "\n";

        player1Str += "Health: " + player1Stats.Health.TotalHealth.ToString() + "\n";
        player1Str += "Energy: " + player1Stats.Energy.TotalEnergy.ToString() + "\n";
        player1Str += "Defense: " + player1Stats.PhysicalDefense.ToString() + "\n";
        player1Str += "Magic Defense: " + player1Stats.MagicDefense.ToString() + "\n";

        player2Str += player2Stats.Pendants["Duel Damage"].Name + " :Type\n";
        player2Str += player2Stats.Pendants["Duel Style"].Name + " :Style\n";

        player2Str += player2Stats.Health.TotalHealth.ToString() + " :Health\n";
        player2Str += player2Stats.Energy.TotalEnergy.ToString() + " :Energy\n";
        player2Str += player2Stats.PhysicalDefense.ToString() + " :Defense\n";
        player2Str += player2Stats.MagicDefense.ToString() + " :Magic Defense\n";

        player1StatsText.text = player1Str;
        player2StatsText.text = player2Str;
    }
}

