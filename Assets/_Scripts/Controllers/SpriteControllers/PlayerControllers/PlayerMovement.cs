using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : SpriteMovement
{
    [SerializeField] private PlayerData data;
    public override void Start()
    {
        base.Start();

        jumpForce = data.jumpForce;
        speed = data.speed;

        CameraController.Instance.SetTarget(gameObject);
    }
}
