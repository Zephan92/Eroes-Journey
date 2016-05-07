using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public GameObject character;
    public GameObject currentEnergy;
    private RectTransform chrt;
    private CharacterInformation characterInformation;
    private Energy energy;
    //private Dictionary<string, Pendant> curPendants;

    private float healthUnit;
    private RectTransform parentSize;

    void Start()
    {
        characterInformation = character.GetComponent<CharacterInformation>();
        //curPendants = characterInformation.Pendants;
        energy = characterInformation.Energy;

        chrt = currentEnergy.GetComponent<RectTransform>();
        parentSize = currentEnergy.transform.parent.GetComponent<RectTransform>();
        healthUnit = (1f / energy.TotalEnergy) * chrt.sizeDelta.x;

        InvokeRepeating("regenerateEnergy", 3.0f, 1.0f);
    }

    void Update()
    {
        chrt.localScale = new Vector2(parentSize.localScale.x * energy.CurrentEnergy / energy.TotalEnergy, parentSize.localScale.y);
        chrt.localPosition = Vector3.left * healthUnit * (energy.TotalEnergy - energy.CurrentEnergy) / 2;
    }

    public void regenerateEnergy()
    {
        if (energy.CurrentEnergy >= 0.0f && energy.CurrentEnergy <= energy.TotalEnergy)
        {
            if (energy.CurrentEnergy + 2.0f > energy.TotalEnergy)
                energy.CurrentEnergy = energy.TotalEnergy;
            else
                energy.CurrentEnergy += 2.0f;
        }
    }
}
