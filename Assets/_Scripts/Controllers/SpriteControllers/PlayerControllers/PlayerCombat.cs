using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : SpriteCombat
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileForce;
    private float projectileXOffset = 0.1f;
    protected override void RangedAttack()
    {
        if (!spriteRenderer.flipX)
        {
            GameObject proj = Instantiate(projectile, transform.position + new Vector3(projectileXOffset, 0), Quaternion.identity);
            proj.GetComponent<Rigidbody2D>().AddForce(Vector3.right * projectileForce, ForceMode2D.Impulse);
        }
        else
        {
            GameObject proj = Instantiate(projectile, transform.position - new Vector3(projectileXOffset, 0), Quaternion.identity);
            proj.GetComponent<SpriteRenderer>().flipX = true;
            proj.GetComponent<Rigidbody2D>().AddForce(Vector3.left * projectileForce, ForceMode2D.Impulse);
        }
    }
}
