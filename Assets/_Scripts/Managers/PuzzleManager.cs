using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObjectMessenger currentPuzzleMessenger; // objects[0] = currentPuzzle

    private static Dictionary<string, bool> completedDict = new Dictionary<string, bool>();

    public static void CompletePuzzle(string name)
    {
        if (completedDict.TryGetValue(name, out _))
        {
            completedDict[name] = true;
        }
        else
        {
            completedDict[name] = true;
        }
    }
    public static bool IsComplete(string name)
    {
        if (completedDict.TryGetValue(name, out _))
        {
            return true;
        }
        return false;
    }
}
