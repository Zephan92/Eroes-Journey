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
            dialogEvents[i].text = SanitizeDialog(dialogEvents[i].text);
            string [] lines = dialogEvents[i].text.Split('\n');
            for (int j = 0; j < lines.Length; j++)
            {
                dialog[i, j] = lines[j];
                if (lines[j].Length > 59)
                {
                    Debug.LogError("Dialog " + i + ":" + j + " on \"" + name + "\" is too long!\n" 
                        + "\""+ dialog[i, j].Substring(0,59) + "\"");
                }
            }
                
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

        if (currentLine == dialog.GetLength(1) - 1 || dialog[currentDialog, currentLine + 1] == null)
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

    private string SanitizeDialog(string dialog)
    {
        dialog = dialog.Replace("\n\n\n\n", "\n");
        dialog = dialog.Replace("\n\n\n", "\n");
        dialog = dialog.Replace("\n\n", "\n");
        return dialog;
    }
}
