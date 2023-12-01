using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : InputController
{

    [SerializeField] protected string controlsPrefix; // For example: "Player 1" for "Player 1 Horizontal" movement,
                                                      // usually for local multiplayer

    private float attackCooldown = 0.36f;
    private float attackTimer = 0;

    private float resetCooldown = 0.2f;
    private float resetTimer = 0;
    public override void Start()
    {
        base.Start();

        if (controlsPrefix != "")
        {
            controlsPrefix += " ";
        }
    }
    public override void Update()
    {
        base.Update();

        if (canAct)
        {
            if (!InputManager.GetButton("Move Left") || !InputManager.GetButton("Move Right"))
            {
                if (InputManager.GetButton("Move Left"))
                {
                    horizontalInput = -1;
                }
                else if (InputManager.GetButton("Move Right"))
                {
                    horizontalInput = 1;
                }
            }
            doJump = InputManager.GetButtonDown(controlsPrefix + "Jump");
            isJumpHeld = InputManager.GetButton(controlsPrefix + "Jump");

            doAttack = (InputManager.GetButtonDown("Interact") || InputManager.GetButtonDown("Shoot")) && attackTimer <= 0;
            if (doAttack)
            {
                attackTimer = attackCooldown;
                attackType = InputManager.GetButtonDown("Interact") ? 0 : 1;
                if (attackType == 0)
                {

                }
                else
                {
                    AudioPlayer.PlaySound("Player Shoot Sound");
                }
            }

            if (InputManager.GetButtonDown("Reset") && resetTimer <= 0)
            {
                EventMessenger.TriggerEvent("Reset");
                resetTimer = resetCooldown;
            }

            if (attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }
            if (resetTimer > 0)
            {
                resetTimer -= Time.deltaTime;
            }
        }
    }
}
