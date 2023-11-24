using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPylonController : PuzzleController
{
    // Messenger bools = does each tower have no bases

    protected override void ResetMessenger()
    {
        base.ResetMessenger();
        messenger.floats[0] = 0;
    }
}
