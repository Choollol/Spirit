using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float jumpForce;
    public float speed;
    public float extraJumpForce;
    public float extraSpeed;
    public int extraJumps;
}
