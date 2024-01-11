using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeStand : PuzzleComponent
{
    private static float checkCountdown = 0.5f;
    private static float checkCountdownCounter = 0;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private List<Sprite> textures;

    [SerializeField] private int startingChimeCount;
    private int chimeCount;

    private bool isCountingDown;
    public override void OnEnable()
    {
        base.OnEnable();

        EventMessenger.StartListening("ResetCountdown" + controllerTransform.name, ResetCountdown);
    }
    public override void OnDisable()
    {
        base.OnDisable();

        EventMessenger.StopListening("ResetCountdown" + controllerTransform.name, ResetCountdown);
    }
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        isRangedInteractable = true;
        doStopInteraction = false;

        UpdateChimeCount(startingChimeCount);
    }
    private void ResetCountdown()
    {
        if (isCountingDown)
        {
            StopAllCoroutines();
            isCountingDown = false;
        }
    }
    private IEnumerator StartCountdown()
    {
        EventMessenger.TriggerEvent("ResetCountdown" + controllerTransform.name);
        isCountingDown = true;
        checkCountdownCounter = checkCountdown;
        while (checkCountdownCounter > 0)
        {
            checkCountdownCounter -= Time.deltaTime;
            yield return null;
        }
        EventMessenger.TriggerEvent("Check" + controllerTransform.name);
        isCountingDown = false;
        yield break;
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
        StartCoroutine(StartCountdown());
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
