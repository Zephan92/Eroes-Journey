using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EventDialog : MonoBehaviour {

    private Text[] dialogEvents;
    private string[,] dialog = new string[15,5];
    public bool inDialog = false;
    private int currentLine = 0;
    public int currentDialog = 0;
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

            if (!inDialog && Input.GetButtonDown("Submit"))
            {
                StartDialog(currentDialog);
            }
        }
    }

    public void StartDialog(int dialog)
    {
        currentDialog = dialog;
        inDialog = true;
        PrintDialog();
    }

    private void FetchNextLine()
    {
        if (dialog[currentDialog,currentLine + 1] == null)
        {
            //end dialog
        }
        else
        {
            currentLine++;
            PrintDialog();
        }
    }

    public void PrintDialog()
    {
        Debug.Log(dialog[currentDialog,currentLine]);
    }
}
