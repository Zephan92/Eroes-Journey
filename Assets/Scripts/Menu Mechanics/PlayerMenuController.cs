using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerMenuController : MonoBehaviour {

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Battle()
    {
        MenuManager.CurrentMenu.IsOpen = false;
        MasterGameController.control.currentState = GameStates.Battle;
        SceneManager.LoadScene(2);
    }

    public void ExitMenu()
    {
        MenuManager.CurrentMenu.IsOpen = false;
        Invoke("TransitionToWander", 0.4f);
    }

    private void TransitionToWander()
    {
        OverworldController.control.currentState = OverworldStates.Wander;
    }
}
