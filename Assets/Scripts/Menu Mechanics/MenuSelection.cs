using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuSelection : MonoBehaviour
{

    // Array of menu item control names.
    public Button [] menuOptions = new Button[4];
    public int selectedIndex = 0;
    // Default selected menu item (in this case, Tutorial).

    void Start()
    {
        menuOptions = GetComponentsInChildren<Button>();
        menuOptions[selectedIndex].Select();
    }

}
