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

    [HideInInspector] public static bool allowInputControllerAction = true;

    public bool canAct { get; protected set; }
    public float horizontalInput { get; protected set; }
    public bool doJump { get; protected set; }
    public bool isJumpHeld { get; protected set; }
    [HideInInspector] public bool isFalling;
    [HideInInspector] public bool isJumping;
    [HideInInspector] public bool isRising;

    public bool doAttack { get; protected set; }
    public int attackType { get; protected set; } // 0 for melee, 1 for ranged

    [SerializeField] protected InputType inputType;

    public virtual void OnEnable()
    {
        EventMessenger.StartListening("EnableInputControllerAction", EnableInputControllerAction);
        EventMessenger.StartListening("DisableInputControllerAction", DisableInputControllerAction);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("EnableInputControllerAction", EnableInputControllerAction);
        EventMessenger.StopListening("DisableInputControllerAction", DisableInputControllerAction);
    }
    public virtual void Start()
    {
        canAct = true;
    }
    public virtual void Update()
    {
        horizontalInput = 0;
    }
    private void EnableInputControllerAction()
    {
        allowInputControllerAction = true;
    }
    private void DisableInputControllerAction()
    {
        allowInputControllerAction = false;
    }
}
