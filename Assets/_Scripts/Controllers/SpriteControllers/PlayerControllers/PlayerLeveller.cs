using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveller : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ScriptablePrimitive expRequirements;

    private int level;
    private float exp;
    private void OnEnable()
    {
        EventMessenger.StartListening("PuzzleCompleted", GainExp);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("PuzzleCompleted", GainExp);
    }
    void Start()
    {
        level = playerData.level;
        exp = playerData.exp;

        level = 0;
        exp = 0;
    }
    private void GainExp()
    {
        exp += PrimitiveMessenger.floats["expToGive"];
        if (exp >= expRequirements.floats[level])
        {
            LevelUp();
        }
        playerData.exp = exp;
        Debug.Log("exp gained " + PrimitiveMessenger.floats["expToGive"]);
    }
    private void LevelUp()
    {
        exp -= expRequirements.floats[level];
        level++;
        playerData.level = level;
        Debug.Log("level up ;; exp " + exp);
        //AudioPlayer.PlaySound("Player Level Up Sound");
        switch (level)
        {
            case 1:
                {
                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
            case 4:
                {
                    break;
                }
            case 5:
                {
                    break;
                }
            case 6:
                {
                    break;
                }
            case 7:
                {
                    break;
                }
            case 8:
                {
                    break;
                }
        }
    }
}
