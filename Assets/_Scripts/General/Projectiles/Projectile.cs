using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float counter;

    [SerializeField] private float launchForce;

    [SerializeField] private List<string> tagsToIgnore;

    [SerializeField] private GameObject collisionParticle;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        StartCoroutine(Launch());
    }
    void Update()
    {
        if (counter > lifeTime)
        {
            Destroy(gameObject);
        }

        counter += Time.deltaTime;
    }
    protected virtual IEnumerator Launch()
    {
        yield return null;
        Vector3 direction = Vector3.right;
        if (spriteRenderer.flipX)
        {
            direction *= -1;
        }
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        yield break;
    }
    protected virtual void Collided()
    {
        if (collisionParticle)
        {
            Instantiate(collisionParticle, transform.position, Quaternion.identity);
        }
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tag in tagsToIgnore)
        {
            if (!collision.gameObject.CompareTag(tag))
            {
                Collided();
                Destroy(gameObject);
                break;
            }
        }
    }
}
