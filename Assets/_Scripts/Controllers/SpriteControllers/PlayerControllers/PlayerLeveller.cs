using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLeveller : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private ScriptablePrimitive expRequirements;

    [SerializeField] private GameObject expGainedText;

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
        playerData.level = level;
        playerData.exp = exp;
    }
    private void GainExp()
    {
        exp += PrimitiveMessenger.floats["expToGive"];
        if (exp >= expRequirements.floats[level])
        {
            LevelUp();
        }
        playerData.exp = exp;
        GameObject expText = Instantiate(expGainedText, transform.position + new Vector3(0, 0.2f), Quaternion.identity);
        expText.GetComponent<RisingText>().UpdateText("+" + PrimitiveMessenger.floats["expToGive"] + " exp");
    }
    private void LevelUp()
    {
        exp -= expRequirements.floats[level];
        level++;
        playerData.level = level;
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
