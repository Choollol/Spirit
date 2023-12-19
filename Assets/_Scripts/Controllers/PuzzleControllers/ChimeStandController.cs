using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeStandController : PuzzleController
{
    // floats[name] = 0: No chimes with 0 | floats[name] = 1: Has chimes with 0
    public override void OnEnable()
    {
        base.OnEnable();

        EventMessenger.StartListening("PlayerProjectileDestroyed", CheckForCompletion);
    }
    public override void OnDisable()
    {
        base.OnDisable();

        EventMessenger.StopListening("PlayerProjectileDestroyed", CheckForCompletion);
    }
    public override void Start()
    {
        base.Start();

        PrimitiveMessenger.floats[name] = 0;
    }
    protected override void CheckForCompletion()
    {
        if (PrimitiveMessenger.floats[name] == 0)
        {
            return;
        }

        base.CheckForCompletion();

        PrimitiveMessenger.floats[name] = 0;
    }
}
