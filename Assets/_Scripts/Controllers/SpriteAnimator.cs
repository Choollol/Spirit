using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class SpriteAnimator : MonoBehaviour
{
    /*
     * Requires animator with animation clips in base layer
     * Animation naming: SpriteName_Action
     * Facing right is default direction, left is when sprite is flipped
     */
    public enum Action
    {
        Idle, Run, Jump, Jump_Straight, Jump_Move, Fall, Fall_Straight, Fall_Move
    }

    private Animator animator;
    private InputController inputController;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private string spriteName;

    private Action action;

    [SerializeField] private bool isJumpDynamic;
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        if (!(inputController = GetComponent<InputController>()))
        {
            inputController = transform.parent.GetComponent<InputController>();
        }
        spriteRenderer = GetComponent<SpriteRenderer>();

        action = Action.Idle;
    }

    void Update()
    {
        if (inputController.canAct)
        {
            DirectionUpdate();
            ActionUpdate();
        }

        string animation = spriteName + "_" + action;

        if (animator.HasState(0, Animator.StringToHash(animation)))
        {
            animator.Play("Base Layer." + animation);
        }

    }
    protected virtual void DirectionUpdate()
    {
        if (inputController.horizontalInput < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (inputController.horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }
    private void ActionUpdate()
    {
        if (inputController.isFalling)
        {
            action = Action.Fall;
            if (isJumpDynamic)
            {
                if (inputController.horizontalInput != 0)
                {
                    action = Action.Fall_Move;
                }
                else
                {
                    action = Action.Fall_Straight;
                }
            }
        }
        else if (inputController.isJumping)
        {
            action = Action.Jump;
            if (isJumpDynamic)
            {
                if (inputController.horizontalInput != 0)
                {
                    action = Action.Jump_Move;
                }
                else
                {
                    action = Action.Jump_Straight;
                }
            }
        }
        else if (inputController.horizontalInput != 0)
        {
            action = Action.Run;
        }
        else
        {
            action = Action.Idle;
        }
    }
    public void SetAction(Action newAction)
    {
        action = newAction;
    }
}
