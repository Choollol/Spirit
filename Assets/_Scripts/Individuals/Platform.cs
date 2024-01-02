using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private float platformSize;
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        platformSize = transform.GetChild(0).localScale.x / 100;
        boxCollider.size = new Vector2(platformSize + 0.04f, boxCollider.size.y);
        transform.GetChild(1).SetLocalPosX(-(platformSize / 2 + 0.01f));
        transform.GetChild(2).SetLocalPosX(platformSize / 2 + 0.01f);
    }
}
