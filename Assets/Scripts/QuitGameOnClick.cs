using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class QuitGameOnClick : MonoBehaviour {

    public void QuitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
