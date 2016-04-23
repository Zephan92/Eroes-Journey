using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tracker : MonoBehaviour {
    private GameObject characterPositionTracker;
    private GameObject characterAttackTracker;
    public List<GameObject> attackZones;
    public List<GameObject> positionZones;
    private GameObject opponent;
    private GameObject player;
    private CharacterInformation oci;
    private CharacterInformation cci;

    void Start ()
    {        
        if (tag.Equals("Player 1"))
        {
            characterPositionTracker = GameObject.FindGameObjectWithTag("Position Tracker 1");
            characterAttackTracker = GameObject.FindGameObjectWithTag("Attack Tracker 1");
        }
        else
        {
            characterPositionTracker = GameObject.FindGameObjectWithTag("Position Tracker 2");
            characterAttackTracker = GameObject.FindGameObjectWithTag("Attack Tracker 2");
        }
        player = gameObject;
        cci = player.GetComponent<CharacterInformation>();
        opponent = cci.Opponent;
        oci = opponent.GetComponent<CharacterInformation>();
    }

    public void instantiatiatePlayerPanels(GameObject player)
    {
        int numRows = 1;

        if (cci.Pendants.ContainsKey("Double Jump"))
        {
            numRows = 3;
        }
        else if (cci.Pendants.ContainsKey("Jump"))
        {
            numRows = 2;
        }

        for (int r = 0; r < numRows; r++)
        {
            for (int c = 0; c < 3; c++)
            {
                GameObject pp = Instantiate(Resources.Load("Player Position Panel")) as GameObject;
                pp.transform.SetParent(characterPositionTracker.transform);
                pp.transform.localPosition = new Vector3(c * 50, r * -50, 0);
                positionZones.Add(pp);

                GameObject tp = Instantiate(Resources.Load("Tracker Panel")) as GameObject;
                tp.transform.SetParent(characterAttackTracker.transform);
                tp.transform.localPosition = new Vector3(c * 50, r * -50, 0);
                attackZones.Add(tp);

            }
        }

        positionZones[convertZoneToInt(cci.Zone, player, cci)].GetComponent<Image>().color = Color.blue;
    }

    public void updateTrackerDisplays()
    {
        updateAttackDisplay(player, cci);
        updatePositionDiplay(player, cci);
        updateAttackDisplay(opponent, oci);
        updatePositionDiplay(opponent, oci);
    }

    public void updateAttackDisplay(GameObject targetPlayer, CharacterInformation targetCCI)
    {
        Tracker targetTracker = targetPlayer.GetComponent<Tracker>();
        GameObject targetOpponent = targetCCI.Opponent;
        CharacterInformation targetOCI = targetOpponent.GetComponent<CharacterInformation>();

        Weapon opponentCurrentWeapon = targetOCI.CurrentWeapon;

        int currentZone = convertZoneToInt(targetCCI.Zone, targetPlayer, targetCCI);
        int opponentCurrentZone = convertZoneToInt(targetOCI.Zone, targetOpponent, targetOCI);

        List<int> specialAttackToZones = new List<int>();

        foreach (SpecialAttack attack in opponentCurrentWeapon.Attacks.Values)
        {
            if (attack.FromZones.Contains(targetOCI.Zone))
                foreach (Zones z in attack.ToZones)
                    specialAttackToZones.Add(convertZoneToInt(z, targetPlayer, targetCCI));
        }

        for (int i = 0; i < targetTracker.attackZones.Count; i++)
        {
            if (specialAttackToZones.Contains(i))
            {
                if (convertZoneToInt(targetCCI.Zone, targetPlayer, targetCCI) == i)
                    targetTracker.attackZones[i].GetComponent<Image>().color = Color.yellow;
                else
                    targetTracker.attackZones[i].GetComponent<Image>().color = Color.red;
            }
            else
            {
                targetTracker.attackZones[i].GetComponent<Image>().color = Color.white;
            }
        }
                
    }

    public void updatePositionDiplay(GameObject targetPlayer, CharacterInformation targetCCI)
    {
        Tracker t = targetPlayer.GetComponent<Tracker>();

        int currentZone = convertZoneToInt(targetCCI.Zone, targetPlayer, targetCCI);

        for (int i = 0; i < t.attackZones.Count; i++)
            if (i == currentZone)
                t.positionZones[i].GetComponent<Image>().color = Color.blue;
            else
                t.positionZones[i].GetComponent<Image>().color = Color.clear;
    }

    public int convertZoneToInt(Zones currentZone, GameObject targetPlayer, CharacterInformation targetCCI)
    {
        int x;
        int y;
        if (currentZone.ToString().Contains("Ground"))
            if (targetCCI.Pendants.ContainsKey("Double Jump"))
                y = 6;
            else if (targetCCI.Pendants.ContainsKey("Jump"))
                y = 3;
            else
                y = 0;
        else if (currentZone.ToString().Contains("Air"))
            if (targetCCI.Pendants.ContainsKey("Double Jump"))
                y = 3;
            else 
                y = 0;
        else
            y = 0;

        if (currentZone.ToString().Contains("Long"))
            if (targetPlayer.tag.Equals("Player 1"))
                x = 0;
            else
                x = 2;
        else if (currentZone.ToString().Contains("Middle"))
            x = 1;
        else
            if (targetPlayer.tag.Equals("Player 1"))
                x = 2;
            else
                x = 0;
        return x + y;
        
    }
}
