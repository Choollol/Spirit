using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerAuraAnimator : SpriteAnimator
{
    private CircleCollider2D circleCollider;

    private Collider2D[] overlapResults = new Collider2D[5];
    private ContactFilter2D contactFilter;
    public override void OnEnable()
    {
        base.OnEnable();

        EventMessenger.StartListening("MeleeInteracted", Interacted);
    }
    public override void OnDisable()
    {
        base.OnDisable();

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
        overlapResults = new Collider2D[5];
        if (inputController.attackType == 0 && 
            Physics2D.OverlapCircle(transform.position, circleCollider.radius - 0.02f, contactFilter, overlapResults) > 1)
        {
            foreach (Collider2D collider in overlapResults)
            {
                if (collider != null && collider.gameObject.CompareTag("Melee Interactable"))
                {
                    circleCollider.enabled = true;
                    StartCoroutine(Interact());
                    doPlayAttackAnimation = false;
                    return;
                }
            }
        }
    }
    private IEnumerator Interact()
    {
        int radiusChunks = 4;
        float radiusIncrement = circleCollider.radius /= radiusChunks;
        circleCollider.enabled = true;
        circleCollider.radius = 0;
        for (int i = 0; i < radiusChunks; i++)
        {
            circleCollider.radius += radiusIncrement;
            yield return null;
        }
        circleCollider.radius = radiusIncrement * radiusChunks;
        yield break;
    }
}
