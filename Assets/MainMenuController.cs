using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    public void QuitGame()
    {
        Application.Quit();
    }

    public void NewGame()
    {
        MenuManager.CurrentMenu.IsOpen = false;
        SceneManager.LoadScene(1);
        MasterGameController.control.currentState = GameStates.Overworld;
    }

}
