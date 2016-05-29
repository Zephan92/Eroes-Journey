using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpHeight = 5.0f;
    public float jumpSpeed = 0.6f;
    public float buffer = 0.2f;
    private WeaponController wc;
    private CharacterController controller;
    private CharacterInformation cci;
    private CharacterInformation oci;
    private bool isMidJump = false;
    private bool isGrounded = true;

    void Start ()
    {
        wc = GetComponent<WeaponController>();
        cci = GetComponent<CharacterInformation>();
        oci = GetComponent<CharacterInformation>();
        controller = GetComponent<CharacterController>();
    }

	void Update() {
        if (DuelController.control.currentState == DuelStates.Start)
        {
            GetComponent<Tracker>().updateTrackerDisplays();
        }
        else if (DuelController.control.currentState == DuelStates.Battle)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                //isMidJump = true;
                //isGrounded = false;
            }

            if (isMidJump)
            {
                incrementJump(jumpHeight, controller.transform.position.y);
                if (controller.transform.position.y >= jumpHeight)
                {
                    isMidJump = false;
                }
            }

            if (controller.transform.position.y < 0.6f)
            {
                isGrounded = true;
            }

            Vector3 forward = transform.TransformDirection(Vector3.right);
            float curSpeed = speed * Input.GetAxis("Horizontal");
            controller.SimpleMove(forward * curSpeed);

            if (Input.GetButtonDown("Fire1"))
            {
                InitiateSpecialAttack(cci.Weapons["Weapon 1"]);
            }

            if (Input.GetButtonDown("Fire2"))
            {
                InitiateSpecialAttack(cci.Weapons["Weapon 2"]);
            }
            if (Input.GetButtonDown("Fire3"))
            {
                SwitchWeapon();
            }
        }
    }

    private void incrementJump(float jumpHeight, float curHeight) {
        Vector3 up = transform.TransformDirection(Vector3.up);
        float momentumModifier = Convert.ToSingle(1 / Math.Exp(Convert.ToDouble(curHeight)));
        transform.position = (up * jumpHeight * momentumModifier * jumpSpeed) + transform.position + new Vector3(0, buffer, 0);
    }

    private void InitiateSpecialAttack(Weapon weapon)
    {
        SpecialAttack specAtk = null;
        foreach (SpecialAttack attack in weapon.Attacks.Values)
        {
            if (attack.FromZones.Contains(cci.Zone) && attack.ToZones.Contains(oci.Zone))
                specAtk = attack;
        }

        if (specAtk != null)
        {
            float healthRecoil = specAtk.HealthRecoilModifier * weapon.Power;
            float energyRecoil = specAtk.EnergyRecoilModifier * weapon.Energy;
            float healthDrain = specAtk.HealthDrainModifier * weapon.Power;
            float energyDrain = specAtk.EnergyDrainModifier * weapon.Energy;
            wc.dealDamageToOpponent(healthRecoil, energyRecoil, healthDrain, energyDrain);
        }
        
    }

    private void SwitchWeapon()
    {
        cci.CurrentWeapon = cci.CurrentWeapon.Equals(cci.Weapons["Weapon 1"]) ? cci.Weapons["Weapon 2"] : cci.Weapons["Weapon 1"];
        GetComponent<Tracker>().updateTrackerDisplays();
    }
}
