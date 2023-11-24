using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float lifeTime;
    private float counter;

    [SerializeField] private List<string> tagsToIgnore;

    void Update()
    {
        if (counter > lifeTime)
        {
            Destroy(gameObject);
        }

        counter += Time.deltaTime;
    }
    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (string tag in tagsToIgnore)
        {
            if (!collision.gameObject.CompareTag(tag))
            {
                Destroy(gameObject);
                break;
            }
        }
    }
}
