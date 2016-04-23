using UnityEngine;
using System.Collections.Generic;

public enum WeaponClass
{
    Key,
    Common,
    Uncommon,
    Rare,
    Mythic,
    Legendary,
}

public class Weapon
{
    public string Name;
    public int Power;
    public int Energy;
    public WeaponClass Rarity;
    public Dictionary<string, SpecialAttack> Attacks;

    public Weapon(string n, int p, int e, WeaponClass wc, Dictionary<string, SpecialAttack> a)
    {
        Name = n;
        Power = p;
        Energy = e;
        Rarity = wc;
        Attacks = a;
    }
}

public class WeaponLibrary : MonoBehaviour {
    public static Dictionary<string, Weapon> WeaponLibraryTable = new Dictionary<string, Weapon>
    {
        {"Beacon", new Weapon("Beacon",10,20,WeaponClass.Key,
            new Dictionary<string, SpecialAttack>
            {
                {"Burst", SpecialAttackLibrary.SpecialAttackLibraryTable["Short Attack"] },
                {"Blast", SpecialAttackLibrary.SpecialAttackLibraryTable["Middle Attack"]},
            }
        )},
        {"Apple Picker", new Weapon("Apple Picker",25,65,WeaponClass.Key,
             new Dictionary<string, SpecialAttack>
            {
               {"Catapult", SpecialAttackLibrary.SpecialAttackLibraryTable["Long Attack"]},
               {"Detonate",  SpecialAttackLibrary.SpecialAttackLibraryTable["Middle Attack"]},
            }
        )},
    };
}
