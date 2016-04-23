using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    private GameObject opponent;
    private CharacterInformation charCI;
    private CharacterInformation oppCI;

	void Start () {
        charCI = gameObject.GetComponent<CharacterInformation>();
        opponent = charCI.Opponent;
        oppCI = opponent.GetComponent<CharacterInformation>();
    }
	
    public void dealDamageToOpponent(float recoilDamage, float energyCost, float oppDamge, float oppEnergyChange)
    {
        if (charCI.Health.CurrentHealth - recoilDamage > 0 && charCI.Energy.CurrentEnergy - energyCost > 0)
        {
            charCI.updateHealth(recoilDamage);
            charCI.updateEnergy(energyCost);
            oppCI.updateHealth(oppDamge);
            oppCI.updateEnergy(oppEnergyChange);
        }
    }

    //add energy cost method
}
