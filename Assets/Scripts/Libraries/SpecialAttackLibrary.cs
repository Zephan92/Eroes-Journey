using UnityEngine;
using System.Collections.Generic;

public class SpecialAttack
{
    public string Name;
    public float HealthDrainModifier;
    public float EnergyDrainModifier;
    public float HealthRecoilModifier;
    public float EnergyRecoilModifier;
    public List<Zones> FromZones;
    public List<Zones> ToZones;

    public SpecialAttack(string n, float hdm, float edm, float hrm, float erm, List<Zones> az, List<Zones> tz)
    {
        Name = n;
        HealthDrainModifier = hdm;
        EnergyDrainModifier = edm;
        HealthRecoilModifier = hrm;
        EnergyRecoilModifier = erm;
        FromZones = az;
        ToZones = tz;
    }
}

public class SpecialAttackLibrary : MonoBehaviour {
    public static Dictionary<string, SpecialAttack> SpecialAttackLibraryTable = new Dictionary<string, SpecialAttack>
    {
        {"Short Attack",new SpecialAttack
            (
            "Short Attack",0.5f,0,0,0.5f,
            new List<Zones> {Zones.GroundShort },
            new List<Zones> {Zones.GroundShort, Zones.GroundMiddle}
            )
        },
        {"Middle Attack",new SpecialAttack
            (
            "Middle Attack",1f,0,0,1f,
            new List<Zones> {Zones.GroundMiddle },
            new List<Zones> {Zones.GroundShort, Zones.GroundMiddle, Zones.GroundLong}
            )
        },
        {"Long Attack",new SpecialAttack
            (
            "Long Attack",1.5f,0,0,1.5f,
            new List<Zones> {Zones.GroundLong },
            new List<Zones> {Zones.GroundMiddle,Zones.GroundLong}
            )
        },
    };
}
