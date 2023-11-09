using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            }
        }
    }
}
