using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCombat : MonoBehaviour
{
    protected InputController inputController;
    protected SpriteRenderer spriteRenderer;
    void Start()
    {
        inputController = GetComponent<InputController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (inputController.doAttack)
        {
            if (inputController.attackType == 0)
            {
                MeleeAttack();
            }
            else
            {
                RangedAttack();
            }
        }
    }
    protected virtual void MeleeAttack()
    {

    }
    protected virtual void RangedAttack()
    {

    }
}
