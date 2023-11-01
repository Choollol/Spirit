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
    public List<string> controlNames = new List<string>();
    public List<string> controlButtons = new List<string>();
    private static Dictionary<string, KeyCode> controls = new Dictionary<string, KeyCode>();
    private void Awake()
    {
        for (int i = 0; i < controlNames.Count; i++)
        {
            if (controlButtons[i] == "up" || controlButtons[i] == "down" ||
                controlButtons[i] == "left" || controlButtons[i] == "right")
            {
                controlButtons[i] += "arrow";
            }
            controls.Add(controlNames[i], (KeyCode)Enum.Parse(typeof(KeyCode), controlButtons[i], true));
        }
    }
    void Start()
    {

    }

    void Update()
    {

    }
    public static bool GetButtonDown(string name)
    {
        return Input.GetKeyDown(controls[name]);
    }
    public static bool GetButton(string name)
    {
        return Input.GetKey(controls[name]);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(InputManager))]
class InputManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var inputManager = (InputManager)target;
        if (inputManager == null) { return; }
        Undo.RecordObject(inputManager, "Undo Input Manager");

        int defaultLabelFontSize = GUI.skin.label.fontSize;

        GUI.skin.label.fontSize = 16;
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Controls", GUILayout.Height(30));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        GUI.skin.label.fontSize = defaultLabelFontSize;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", GUILayout.Height(20));
        GUILayout.FlexibleSpace();
        GUILayout.Label("Button", GUILayout.Height(20), GUILayout.Width(150));
        //GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        for (int i = 0; i < inputManager.controlNames.Count; i++)
        {
            GUILayout.BeginHorizontal();
            inputManager.controlNames[i] = GUILayout.TextField(inputManager.controlNames[i], GUILayout.MaxWidth(Screen.width / 3));
            GUILayout.FlexibleSpace();
            inputManager.controlButtons[i] = GUILayout.TextField(inputManager.controlButtons[i], GUILayout.MaxWidth(Screen.width / 3));
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add Control"))
        {
            inputManager.controlNames.Add("");
            inputManager.controlButtons.Add("");
        }
        if (GUILayout.Button("Delete Control"))
        {
            inputManager.controlNames.RemoveAt(inputManager.controlNames.Count - 1);
            inputManager.controlButtons.RemoveAt(inputManager.controlButtons.Count - 1);
        }
    }
}
#endif