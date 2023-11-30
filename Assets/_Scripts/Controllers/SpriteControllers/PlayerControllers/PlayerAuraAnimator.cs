using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerAuraAnimator : SpriteAnimator
{
    private CircleCollider2D circleCollider;

    private Collider2D[] overlapResults = new Collider2D[5];
    private ContactFilter2D contactFilter;
    private void OnEnable()
    {
        EventMessenger.StartListening("MeleeInteracted", Interacted);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("MeleeInteracted", Interacted);
    }
    public override void Start()
    {
        base.Start();

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;

        contactFilter.NoFilter();
        contactFilter.useTriggers = true;
    }
    private void Interacted()
    {
        circleCollider.enabled = false;
    }
    protected override void StartAttacking()
    {
        if (inputController.attackType == 0 && 
            Physics2D.OverlapCircle(transform.position, circleCollider.radius - 0.02f, contactFilter, overlapResults) > 1)
        {
            foreach (Collider2D collider in overlapResults)
            {
                if (collider != null && collider.gameObject.CompareTag("Melee Interactable"))
                {
                    circleCollider.enabled = true;
                    doPlayAttackAnimation = false;
                    return;
                }
            }
        }
    }
}
