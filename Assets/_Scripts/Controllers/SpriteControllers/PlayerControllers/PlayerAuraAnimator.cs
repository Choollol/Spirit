using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerAuraAnimator : SpriteAnimator
{
    private BoxCollider2D boxCollider;
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

        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false;

        framesBeforeAttackAnimation = 15;
    }
    private void Interacted()
    {
        EndAttacking();
        StopAllCoroutines();
    }
    protected override void StartAttacking()
    {
        if (action == Action.Attack_Melee)
        {
            boxCollider.enabled = true;
            StartCoroutine(DisableCollider());
        }
    }
    private IEnumerator DisableCollider()
    {
        for (int i = 0; i < framesBeforeAttackAnimation / Time.timeScale; i++)
        {
            yield return null;
        }
        boxCollider.enabled = false;
        yield break;
    }
    protected override void EndAttacking()
    {
        base.EndAttacking();

        boxCollider.enabled = false;
    }
}
