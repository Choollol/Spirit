using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InputController : MonoBehaviour
{
    public enum InputType
    {
        None, User
    }

    public float horizontalInput { get; private set; }
    public bool doJump { get; private set; }
    public bool isJumpHeld { get; private set; }
    [HideInInspector] public bool isFalling;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isRising;

    public bool doAttack { get; private set; }
    public int attackType { get; private set; } // 0 for melee, 1 for ranged

    private float attackCooldown = 0.36f;
    private float attackTimer = 0;

    [SerializeField] private InputType inputType;

    [SerializeField] private string controlsPrefix; // For example: "Player 1" for "Player 1 Horizontal" movement,
                                                    // usually for local multiplayer

    public bool canAct { get; private set; }
    private void Start()
    {
        canAct = true;

        if (controlsPrefix != "")
        {
            controlsPrefix += " ";
        }
    }
    public virtual void Update()
    {
        horizontalInput = 0;
        if (canAct)
        {
            if (inputType == InputType.User)
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

                if (doAttack)
                {
                    doAttack = false;
                }
                doAttack = (InputManager.GetButtonDown("Melee") || InputManager.GetButtonDown("Shoot")) && attackTimer <= 0;
                if (doAttack)
                {
                    attackTimer = attackCooldown;
                    attackType = InputManager.GetButtonDown("Melee") ? 0 : 1;
                }
                if (attackTimer > 0)
                {
                    attackTimer -= Time.deltaTime;
                }

                if (InputManager.GetButtonDown("Reset"))
                {
                    EventMessenger.TriggerEvent("Reset");
                }
            }
        }
    }
}
