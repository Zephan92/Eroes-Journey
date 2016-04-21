using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
    public GameObject opponent;
    private CharacterInformation charCI;
    private CharacterInformation oppCI;
    //baseline attack cost is as follows: 12*damage - 6*recoil + oppEnergyCost;

	// Use this for initialization
	void Start () {
        charCI = gameObject.GetComponent<CharacterInformation>();
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
}
