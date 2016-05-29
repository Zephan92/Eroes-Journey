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
        SceneManager.LoadScene(2);
    }

    public void ExitMenu()
    {
        OverworldController.control.currentState = OverworldStates.Wander;
        MenuManager.CurrentMenu.IsOpen = false;
    }
}
