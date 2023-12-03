using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveller : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private List<int> expRequirements;

    private int level;
    private float exp;
    private void OnEnable()
    {
        EventMessenger.StartListening("GivePlayerExp", GainExp);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("GivePlayerExp", GainExp);
    }
    void Start()
    {
        level = playerData.level;
        exp = playerData.exp;
    }
    private void GainExp()
    {
        exp += PrimitiveMessenger.floats["expToGive"];
        playerData.exp = exp;
        if (exp >= expRequirements[level - 1])
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        level++;
        playerData.level = level;
        //AudioPlayer.PlaySound("Player Level Up Sound");
    }
}
