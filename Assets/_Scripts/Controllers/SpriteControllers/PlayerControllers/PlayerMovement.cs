using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : SpriteMovement
{
    [SerializeField] private PlayerData data;
    private void OnEnable()
    {
        EventMessenger.StartListening("UpdatePlayerData", UpdatePlayerData);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("UpdatePlayerData", UpdatePlayerData);
    }
    public override void Start()
    {
        base.Start();

        UpdatePlayerData();

        CameraController.Instance.SetTarget(gameObject);
    }
    public void UpdatePlayerData()
    {
        jumpForce = data.jumpForce + data.extraJumpForce;
        speed = data.speed + data.extraSpeed;
        extraJumps = data.extraJumps;
    }
}
