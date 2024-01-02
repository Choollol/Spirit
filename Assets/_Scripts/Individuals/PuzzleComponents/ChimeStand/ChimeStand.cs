using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeStand : PuzzleComponent
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<Sprite> textures;

    [SerializeField] private int startingChimeCount;
    private int chimeCount;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        isRangedInteractable = true;
        doStopInteraction = false;

        UpdateChimeCount(startingChimeCount);
    }
    public override void RangedInteract()
    {
        base.RangedInteract();

        UpdateChimeCount(chimeCount - 1);
        if (chimeCount < 0)
        {
            return;
        }
        if (chimeCount == 0)
        {
            PrimitiveMessenger.floats[controllerTransform.name] = 1;
            PrimitiveMessenger.bools[controllerTransform.name + transform.GetSiblingIndex()] = true;
        }
        AudioPlayer.PlaySound("Chime Stand Sound", 0.9f, 1.1f, true);
    }
    public override void ResetPuzzle()
    {
        base.ResetPuzzle();

        PrimitiveMessenger.bools[controllerTransform.name + transform.GetSiblingIndex()] = false;
        UpdateChimeCount(startingChimeCount);
    }
    private void UpdateChimeCount(int newChimeCount)
    {
        chimeCount = newChimeCount;
        if (chimeCount < 0)
        {
            return;
        }
        spriteRenderer.sprite = textures[chimeCount];
    }
}
