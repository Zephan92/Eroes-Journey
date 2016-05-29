using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    public string StartMenuName;
    public static Menu CurrentMenu;
    private static Dictionary<string, Menu> MenuOptions = new Dictionary<string, Menu>();

    public void Awake()
    {
        foreach (Menu menu in GetComponentsInChildren<Menu>())
        {
            MenuOptions.Add(menu.name,menu);
        }
        CurrentMenu = MenuOptions[StartMenuName];
    }

    public void Start()
    {
        ShowMenu(CurrentMenu.name);
    }

    public static void ShowMenu(string cm)
    {
        if (CurrentMenu != null)
        {
            CurrentMenu.IsOpen = false;
        }

        CurrentMenu = MenuOptions[cm];
        CurrentMenu.IsOpen = true;
    }
}
