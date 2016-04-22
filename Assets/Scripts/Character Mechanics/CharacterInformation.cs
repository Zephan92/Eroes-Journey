﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Zones
{
    GroundLong,
    GroundMiddle,
    GroundShort,
    AirLong,
    AirMiddle,
    AirShort,
    SkyLong,
    SkyMiddle,
    SkyShort,
}

public class Health
{
    public float TotalHealth;
    public float CurrentHealth;

    public Health(float totHealth, float curHealth)
    {
        TotalHealth = totHealth;
        CurrentHealth = curHealth;
    }
}

public class Energy
{
    public float TotalEnergy;
    public float CurrentEnergy;

    public Energy(float totEnergy, float curEnergy)
    {
        TotalEnergy = totEnergy;
        CurrentEnergy = curEnergy;
    }
}

public class CharacterInformation : MonoBehaviour
{
    public Dictionary<string, Pendant> Pendants = new Dictionary<string, Pendant>
    {
        {"Life", PendantLibrary.PendantLibraryTable["Life"]},
        {"Energy", PendantLibrary.PendantLibraryTable["Energy"]},
        {"Defense", PendantLibrary.PendantLibraryTable["Defense"]},
        {"Magic Defense", PendantLibrary.PendantLibraryTable["Magic Defense"]}, 
        {"Speed", PendantLibrary.PendantLibraryTable["Speed"]},

        {"Duel Damage", PendantLibrary.PendantLibraryTable["Magic"]},
        {"Duel Style", PendantLibrary.PendantLibraryTable["Blitz"]},

        //{"Jump", PendantLibrary.PendantLibraryTable["Jump"]},
        //{"Double Jump", PendantLibrary.PendantLibraryTable["Double Jump"]},
    };

    public Dictionary<string,Weapon> Weapons = new Dictionary<string, Weapon>()
    {
        {"Weapon 1", WeaponLibrary.WeaponLibraryTable["Beacon"] },
        {"Weapon 2", WeaponLibrary.WeaponLibraryTable["Apple Picker"] },
    };

    public Zones Zone = Zones.GroundMiddle;
    public float MagicDefense = 0;
    public float PhysicalDefense = 0;
    public Health Health = new Health(100, 100);
    public Energy Energy = new Energy(100, 100);

    public void updateHealth(float healthChange)
    {
        float oppNewHealth = Health.CurrentHealth - healthChange;
        
        if (oppNewHealth > 0 && oppNewHealth < Health.TotalHealth)
            Health.CurrentHealth = oppNewHealth;
        else if (oppNewHealth <= 0)
            Health.CurrentHealth = 0;
        else if (oppNewHealth >= Health.TotalHealth)
            Health.CurrentHealth = Health.TotalHealth;
    }

    public void updateEnergy(float energyChange)
    {
        float oppNewEnergy = Energy.CurrentEnergy - energyChange;
        if (oppNewEnergy > 0 && oppNewEnergy < Energy.TotalEnergy)
            Energy.CurrentEnergy = oppNewEnergy;
        else if (oppNewEnergy <= 0)
            Energy.CurrentEnergy = 0;
        else if (oppNewEnergy >= Energy.TotalEnergy)
            Energy.CurrentEnergy = Energy.TotalEnergy;
    }
}
