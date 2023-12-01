using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    //[SerializeField] private GameObjectMessenger currentPuzzleMessenger; // objects[0] = currentPuzzle, objects[1] = new puzzle

    // world name, (puzzle name, puzzle completion)
    //public static Dictionary<string, KeyValuePair<string, bool>> completedDict { get; private set; } 
    public static Dictionary<string, Dictionary<string, bool>> completedDict { get; private set; }

    private void Awake()
    {
        completedDict = new Dictionary<string, Dictionary<string, bool>>();
        string[] worlds = Enum.GetNames(typeof(GameManager.World));
        for (int i = 0; i < worlds.Length; i++)
        {
            completedDict.Add(((GameManager.World)i).ToString(), new Dictionary<string, bool>());
        }
    }
    private void OnEnable()
    {
        EventMessenger.StartListening("SetCurrentPuzzle", SetCurrentPuzzle);
        EventMessenger.StartListening("WorldSwitched", UpdateCompletePuzzles);
        EventMessenger.StartListening("GameLoaded", UpdateCompletePuzzles);
    }
    private void OnDisable()
    {
        EventMessenger.StopListening("SetCurrentPuzzle", SetCurrentPuzzle);
        EventMessenger.StopListening("WorldSwitched", UpdateCompletePuzzles);
        EventMessenger.StopListening("GameLoaded", UpdateCompletePuzzles);
    }
    public void SetCurrentPuzzle()
    {
        ObjectMessenger.SetGameObject("currentPuzzle", ObjectMessenger.GetGameObject("newPuzzle"));
        //currentPuzzleMessenger.objects[0] = currentPuzzleMessenger.objects[1];
        EventMessenger.TriggerEvent("CurrentPuzzleChanged");
    }
    private void UpdateCompletePuzzles()
    {
        foreach (var puzzleName in completedDict[GameManager.currentWorld.ToString()].Keys.ToList())
        {
            EventMessenger.TriggerEvent("SetComplete" + puzzleName);
        }
    }
    public static void AddPuzzle(string world, string name)
    {
        completedDict[world].Add(name, false);
    }
    public static void CompletePuzzle(string world, string name)
    {
        completedDict[world][name] = true;
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
