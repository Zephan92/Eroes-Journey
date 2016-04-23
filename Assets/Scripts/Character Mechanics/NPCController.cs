using UnityEngine;
using System.Collections;
using System;

public class NPCController : MonoBehaviour
{
    private DuelController dc;

    public float speed = 6.0F;
    public float jumpHeight = 5.0f;
    public float jumpSpeed = 0.6f;
    public float buffer = 0.2f;
    private float fps = 30;
    
    private CharacterController controller;
    private WeaponController wc;
    private bool isJumping = false;
    private bool isGrounded = true;
    private bool isWalkingLeft = false;
    private bool isWalkingRight = false;
    private bool isMidJump = false;
    private bool aiEngineStarted = false;
    void Start()
    {
        dc = GameObject.FindGameObjectWithTag ("GameController").GetComponent<DuelController>();
        controller = GetComponent<CharacterController>();
        wc = GetComponent<WeaponController>();
    }

    void Update()
    {
        if (dc.currentState == DuelStates.Start)
        {
            if (!aiEngineStarted)
            {
                
                BattleAIEngine();
            }    
        }
        else if (dc.currentState == DuelStates.Battle)
        {
            if (!aiEngineStarted)
            {
                BattleAIEngine();
            }

            if (isGrounded && isJumping)
            {
               // isJumping = false;
               // isMidJump = true;
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

            if (isWalkingLeft || isWalkingRight)
            {
                Vector3 forward = transform.TransformDirection(Vector3.left);
                Vector3 backwards = transform.TransformDirection(Vector3.right);
                float curSpeed = speed * Time.deltaTime * fps;

                if (isWalkingLeft)
                    controller.SimpleMove(forward * curSpeed);
                else
                    controller.SimpleMove(backwards * curSpeed);
            }

            if (controller.transform.position.y < 0.6f)
            {
                isGrounded = true;
            }
        }
    }

    private void incrementJump(float jumpHeight, float curHeight)
    {
        Vector3 up = transform.TransformDirection(Vector3.up);
        float momentumModifier = Convert.ToSingle(1 / Math.Exp(Convert.ToDouble(curHeight)));
        transform.position = (up * jumpHeight * momentumModifier * jumpSpeed) + transform.position + new Vector3(0, buffer, 0);
    }

    private void BattleAIEngine()
    {
        if (dc.currentState != DuelStates.Battle)
        {
            return;
        }
        aiEngineStarted = true;
        float duration;
        isJumping = false;
        isWalkingLeft = false;
        isWalkingRight = false;

        int decision = UnityEngine.Random.Range(0, 3);
        int attackChance = UnityEngine.Random.Range(0, 10);
        float position = UnityEngine.Random.Range(-6f, 6f);

        switch (decision)
        {
            case 0://Walking
                if (transform.localPosition.x - position > 0)
                    isWalkingLeft = true;
                else
                    isWalkingRight = true;
                duration = UnityEngine.Random.Range(0, 2);
                if (duration > 1.5f)
                    speed = 8f;
                else
                    speed = 4f;
                if (attackChance >= 8)
                    Invoke("Attack", duration / 2);
                Invoke("BattleAIEngine", duration);
                break;

            case 1:
                isJumping = true;
                speed = 6f;
                if (transform.localPosition.x - position > 0)
                    isWalkingLeft = true;
                else
                    isWalkingRight = true;
                if (attackChance >= 7)
                    Invoke("Attack", 1.5f);
                Invoke("BattleAIEngine", 3f);
                break;


            default://Do nothing
                duration = UnityEngine.Random.Range(0, 1.5f);
                Invoke("BattleAIEngine", duration);
                break;
        }

    }

    void Attack()
    {
        int whichWeapon = UnityEngine.Random.Range(0, 3);
        if (whichWeapon > 0)
            wc.dealDamageToOpponent(1, 36, 2, -10);
        else
            wc.dealDamageToOpponent(1, 18, 1, 6);
    }
}
