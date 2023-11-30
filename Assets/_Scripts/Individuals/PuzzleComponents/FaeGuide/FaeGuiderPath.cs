using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaeGuiderPath : MonoBehaviour
{
    [SerializeField] private GameObject pathEndPrefab;
    void Start()
    {
        Transform leftEnd = Instantiate(pathEndPrefab, transform.parent).transform;
        Transform rightEnd = Instantiate(pathEndPrefab, transform.parent).transform;
        leftEnd.localPosition = new Vector2(transform.localPosition.x - (transform.localScale.x / 200 + 0.02f), transform.localPosition.y);
        rightEnd.localPosition = new Vector2(transform.localPosition.x + transform.localScale.x / 200 + 0.02f, transform.localPosition.y);
        rightEnd.AddRotation(180);
    }

}
