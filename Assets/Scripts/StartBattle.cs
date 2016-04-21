using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartBattle : MonoBehaviour {
    public Button button;
	// Use this for initialization
	void Start () {
        button.onClick.AddListener(() => { startBattle(); });
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void startBattle()
    {
        SceneManager.LoadScene(1);
    }
}
