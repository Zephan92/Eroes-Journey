using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public GameObject character;
    public GameObject currentHealth;
    private RectTransform chrt;
    private CharacterInformation characterInformation;
    private Health health;
    private Dictionary<string, Pendant> curPendants;
    
    private float healthUnit;
    private RectTransform parentSize;

    void Start () {
        characterInformation = character.GetComponent<CharacterInformation>();
        curPendants = characterInformation.Pendants;
        health = characterInformation.Health;

        chrt = currentHealth.GetComponent<RectTransform>();
        parentSize = currentHealth.transform.parent.GetComponent<RectTransform>();
        healthUnit = (1f / health.TotalHealth) * chrt.sizeDelta.x;    
    }
	
	void Update () {
        chrt.localScale = new Vector2(parentSize.localScale.x * health.CurrentHealth / health.TotalHealth, parentSize.localScale.y);
        chrt.localPosition = Vector3.left * healthUnit * (health.TotalHealth - health.CurrentHealth) / 2;

        
        if (health.CurrentHealth / health.TotalHealth < 2f/3 && health.CurrentHealth / health.TotalHealth >= 1f / 4)
        {//if health is lower than 66% bar color is yellow
            Image im = currentHealth.GetComponent<Image>();
            im.color = Color.yellow;
        }
        else if (health.CurrentHealth / health.TotalHealth < 1f / 4)
        {//if health is lower than 25% bar color is yellow
            Image im = currentHealth.GetComponent<Image>();
            im.color = Color.red;
        }
    }
}
