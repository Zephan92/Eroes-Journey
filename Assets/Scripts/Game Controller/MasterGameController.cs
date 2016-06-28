using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public enum GameStates
{//Different Game States
   MainMenu,
   Overworld,
   Battle,
   Credits,
   MultiplayerMenu,
}

public class MasterGameController : MonoBehaviour {

    public static MasterGameController control;
    public GameStates currentState = GameStates.MainMenu;

    void Awake()
    {
        if (control == null)
        {
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case GameStates.Battle:
                break;
            case GameStates.Credits:
                break;
            case GameStates.MainMenu:
                break;
            case GameStates.MultiplayerMenu:
                break;
            case GameStates.Overworld:
                break;
            default:
                break;
        }
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerData data = new PlayerData();
        //add stuff to data
        //data.playerName = SaveFile.playerName;

        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat",FileMode.Open);

        PlayerData data = (PlayerData) bf.Deserialize(file);
        file.Close();
        //grab stuff from data
        //SaveFile.playerName = data.playerName;
    }
}

[Serializable]
public class PlayerData
{
    //probably need to update this to include dictionaries


}
