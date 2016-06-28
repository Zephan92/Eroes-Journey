using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventDialog : MonoBehaviour {

    private Text[] dialogEvents;
    private string[,] dialog = new string[15,5];
    public bool inDialog = false;
    public bool inEvent = false;
    private int currentLine = 0;
    public int currentDialog = 0;
    private GameObject _eventGameObject;

    public void Start()
    {
        dialogEvents = GetComponents<Text>();
        for (int i = 0; i < dialogEvents.Length; i++)
        {
            string [] lines = dialogEvents[i].text.Split('`');
            for (int j = 0; j < lines.Length; j++)
                dialog[i,j] = lines[j];
        }
    }

    public void Update()
    {
        if (OverworldController.control.currentState == OverworldStates.Event)
        {//make this into a switch
            if (inDialog && Input.GetButtonDown("Submit"))
            {
                FetchNextLine();
            }
        }
    }

    public void StartDialog(int dialog, GameObject eGO)
    {
        _eventGameObject = eGO;
        _eventGameObject.GetComponent<NPCEventController>().npcTalking = true;
        currentDialog = dialog;
        currentLine = 0;
        inDialog = true;
        PrintDialog();
    }

    private void FetchNextLine()
    {

        if (currentLine == dialog.GetLength(1) -2)
        {
            EndDialog();
        }
        else
        {
            currentLine++;
            PrintDialog();
        }
    }

    public void EndDialog()
    {
        currentLine = 0;
        inDialog = false;
        _eventGameObject.GetComponent<NPCEventController>().npcTalking = false;
    }

    public void PrintDialog()
    {
        Debug.Log(dialog[currentDialog,currentLine]);
    }
}
