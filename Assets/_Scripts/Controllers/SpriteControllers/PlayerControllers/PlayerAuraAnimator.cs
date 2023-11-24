using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuraAnimator : SpriteAnimator
{
    private BoxCollider2D boxCollider;
    public override void Start()
    {
        base.Start();

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;
    }
    protected override void StartAttacking()
    {
        if (action == Action.Attack_Melee)
        {
            boxCollider.enabled = true;
        }
    }
    protected override void EndAttacking()
    {
        boxCollider.enabled = false;
    }
}
