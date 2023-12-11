using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float horizontalParallaxAmount;
    [SerializeField] private float verticalParallaxAmount;

    private Transform cameraTransform;

    private Vector3 cameraOldPos;
    void Start()
    {
        cameraTransform = Camera.main.transform;

        //cameraOldPos = cameraTransform.position;
    }

    void Update()
    {
        Vector3 deltaMovement = cameraTransform.position - cameraOldPos;
        transform.position += new Vector3(deltaMovement.x * horizontalParallaxAmount, deltaMovement.y * verticalParallaxAmount);

        cameraOldPos = cameraTransform.position;
    }
}
