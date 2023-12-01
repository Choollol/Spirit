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
        Idle, Run, Jump, Fall, Jump_Straight, Jump_Move, Fall_Straight, Fall_Move, Attack_Melee, Attack_Ranged
    }

    protected Animator animator;
    protected InputController inputController;
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected string spriteName;

    protected Action action;

    [SerializeField] protected bool isJumpDynamic;
    [SerializeField] protected bool doesAttack;

    protected bool doPlayAnimations = true;

    protected bool doPlayAttackAnimation = true;

    public virtual void OnEnable()
    {
        EventMessenger.StartListening("DisableInputControllerAction", InputControllerDisabled);
    }
    public virtual void OnDisable()
    {
        EventMessenger.StopListening("DisableInputControllerAction", InputControllerDisabled);
    }
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
        if (GameManager.isGameActive)
        {
            DirectionUpdate();
            ActionUpdate();

            string animation = spriteName + "_" + action;

            if (animator.HasState(0, Animator.StringToHash(animation)) && doPlayAnimations)
            {
                animator.Play("Base Layer." + animation);
            }
        }
    }
    protected void InputControllerDisabled()
    {
        action = Action.Idle;
        animator.Play(spriteName + "_" + action);
    }
    private IEnumerator PlayAttackAnimation()
    {
        if (!animator.HasState(0, Animator.StringToHash(spriteName + "_" + action)))
        {
            yield break;
        }
        doPlayAttackAnimation = true;
        StartAttacking();
        if (!doPlayAttackAnimation)
        {
            yield break;
        }
        if (inputController.attackType == 0)
        {
            action = Action.Attack_Melee;
        }
        else if (inputController.attackType == 1)
        {
            action = Action.Attack_Ranged;
        }
        doPlayAnimations = false;
        animator.Play("Base Layer." + spriteName + "_" + action);
        Attacking();
        yield return null;
        while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return null;
        }
        EndAttacking();
        yield break;
    }
    protected virtual void StartAttacking()
    {

    }
    protected virtual void Attacking()
    {

    }
    protected virtual void EndAttacking()
    {
        doPlayAnimations = true;
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
    protected void ActionUpdate()
    {
        if (!doPlayAnimations)
        {
            return;
        }
        if (inputController.doAttack && doesAttack)
        {
            StartCoroutine(PlayAttackAnimation());
        }
        else if (inputController.isFalling)
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
        else if (inputController.isRising)
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
