using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //[SerializeField] private GameObjectMessenger currentPuzzleMessenger; // objects[0] = currentPuzzle, objects[1] = new puzzle

    private static Dictionary<string, bool> completedDict = new Dictionary<string, bool>();

    private void OnEnable()
    {
        EventMessenger.StartListening("SetCurrentPuzzle", SetCurrentPuzzle);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("SetCurrentPuzzle", SetCurrentPuzzle);
    }
    public void SetCurrentPuzzle()
    {
        ObjectMessenger.SetGameObject("currentPuzzle", ObjectMessenger.GetGameObject("newPuzzle"));
        //currentPuzzleMessenger.objects[0] = currentPuzzleMessenger.objects[1];
        EventMessenger.TriggerEvent("CurrentPuzzleChanged");
    }
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
