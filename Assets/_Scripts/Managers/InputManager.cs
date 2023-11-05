using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InputManager : MonoBehaviour
{
    [SerializeField] private ControlScheme controlScheme;
    private static Dictionary<string, List<KeyCode>> controls = new Dictionary<string, List<KeyCode>>();
    private void Awake()
    {
        for (int i = 0; i < controlScheme.controlNames.Count; i++)
        {
            if (controlScheme.controlButtons[i] == "up" || controlScheme.controlButtons[i] == "down" ||
                controlScheme.controlButtons[i] == "left" || controlScheme.controlButtons[i] == "right")
            {
                controlScheme.controlButtons[i] += "arrow";
            }
            if (controlScheme.altControlButtons[i] == "up" || controlScheme.altControlButtons[i] == "down" ||
                controlScheme.altControlButtons[i] == "left" || controlScheme.altControlButtons[i] == "right")
            {
                controlScheme.altControlButtons[i] += "arrow";
            }
            List<KeyCode> keysList = new List<KeyCode>();
            if (controlScheme.controlButtons[i] != "")
            {
                keysList.Add((KeyCode)Enum.Parse(typeof(KeyCode), controlScheme.controlButtons[i], true));
            }
            if (controlScheme.altControlButtons[i] != "")
            {
                keysList.Add((KeyCode)Enum.Parse(typeof(KeyCode), controlScheme.altControlButtons[i], true));
            }
            controls.Add(controlScheme.controlNames[i], keysList);
        }
    }
    public static bool GetButtonDown(string name)
    {
        foreach (KeyCode key in controls[name])
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;
    }
    public static bool GetButton(string name)
    {
        foreach (KeyCode key in controls[name])
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;
    }
}