using System.Collections.Generic;
public enum PendantClass
{
    Key,

    DuelStats,
    DuelStyle,
    DuelDamage,

    Common,
    Uncommon,
    Rare,
    Mythic,
    Legendary,
}


public class Pendant
{
    public string Name;
    public int Level;
    public int Experience;
    public PendantClass PendantClass;

    public Pendant(string n, PendantClass pc)
    {
        Name = n;
        Level = 1;
        Experience = 0;
        PendantClass = pc;
    }
}

public class PendantLibrary {
    public static Dictionary<string,Pendant> PendantLibraryTable = new Dictionary<string, Pendant>
    {
        {"Life", new Pendant("Life", PendantClass.DuelStats)},
        {"Energy", new Pendant("Energy", PendantClass.DuelStats)},
        {"Magic Defense", new Pendant("Magic Defense", PendantClass.DuelStats)},
        {"Defense", new Pendant("Defense", PendantClass.DuelStats)},
        {"Speed", new Pendant("Speed", PendantClass.DuelStats)},

        {"Magic", new Pendant("Magic", PendantClass.DuelDamage)},
        {"Physical", new Pendant("Physical", PendantClass.DuelDamage)},

        {"Balanced", new Pendant("Balanced",PendantClass.DuelStyle)},
        {"Striker", new Pendant("Striker",PendantClass.DuelStyle)},
        {"Bruiser", new Pendant("Bruiser",PendantClass.DuelStyle)},
        {"Bouncer", new Pendant("Bouncer",PendantClass.DuelStyle)},
        {"Brash", new Pendant("Brash", PendantClass.DuelStyle)},
        {"Brute", new Pendant("Brute", PendantClass.DuelStyle)},
        {"Assassin", new Pendant("Assassin", PendantClass.DuelStyle)},
        {"Explosive", new Pendant("Explosive",PendantClass.DuelStyle)},
        {"Tank", new Pendant("Tank", PendantClass.DuelStyle)},
        {"Blitz", new Pendant("Blitz", PendantClass.DuelStyle)},

        {"Jump", new Pendant("Jump", PendantClass.Key) },
        {"Double Jump", new Pendant("Double Jump", PendantClass.Key) },
        //Power, Defense, Speed
        //1,1,1 Balanced
        //2,0,1 Striker
        //2,1,0 Bruiser
        //0,2,1 Bouncer
        //0,1,2 Brash
        //1,2,0 Brute
        //1,0,2 Assassin
        //3,0,0 Explosive
        //0,3,0 Tank
        //0,0,3 Blitz

    };
}
