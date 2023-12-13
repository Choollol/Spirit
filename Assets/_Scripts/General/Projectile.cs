using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float counter;

    [SerializeField] private List<string> tagsToIgnore;

    [SerializeField] private GameObject collisionParticle;
    void Update()
    {
        if (counter > lifeTime)
        {
            Destroy(gameObject);
        }

        counter += Time.deltaTime;
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
