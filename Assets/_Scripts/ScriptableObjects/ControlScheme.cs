using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//[CreateAssetMenu(fileName = "ControlScheme", menuName = "ScriptableObjects/ControlScheme", order = 1)]
public class ControlScheme : ScriptableObject
{
    public List<string> controlNames = new List<string>();
    public List<string> controlButtons = new List<string>();
    public List<string> controlAltButtons = new List<string>();
}

#if UNITY_EDITOR
[CustomEditor(typeof(ControlScheme))]
class ControlSchemeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var controlScheme = (ControlScheme)target;
        if (controlScheme == null) { return; }
        Undo.RecordObject(controlScheme, "Undo Control Scheme");

        int defaultLabelFontSize = GUI.skin.label.fontSize;

        float controlTextFieldWidth = Screen.width / 5.5f;

        // Title
        GUI.skin.label.fontSize = 16;
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.Label("Controls", GUILayout.Height(30));
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        // Labels
        GUI.skin.label.fontSize = defaultLabelFontSize;
        GUILayout.BeginHorizontal();
        GUILayout.Label("Name", GUILayout.Height(20), GUILayout.Width(controlTextFieldWidth));
        GUILayout.Label("Button", GUILayout.Height(20), GUILayout.Width(controlTextFieldWidth));
        GUILayout.Label("Alt Button", GUILayout.Height(20), GUILayout.Width(controlTextFieldWidth));
        GUILayout.EndHorizontal();

        // Lists
        for (int i = 0; i < controlScheme.controlNames.Count; i++)
        {
            GUILayout.BeginHorizontal();
            controlScheme.controlNames[i] = 
                GUILayout.TextField(controlScheme.controlNames[i], GUILayout.Width(controlTextFieldWidth));
            GUILayout.FlexibleSpace();
            controlScheme.controlButtons[i] =
                GUILayout.TextField(controlScheme.controlButtons[i], GUILayout.Width(controlTextFieldWidth));
            GUILayout.FlexibleSpace();
            controlScheme.controlAltButtons[i] =
                    GUILayout.TextField(controlScheme.controlAltButtons[i], GUILayout.Width(controlTextFieldWidth));
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("^"))
            {
                if (i > 0)
                {
                    Swap(ref controlScheme.controlNames, i, i - 1);
                    Swap(ref controlScheme.controlButtons, i, i - 1);
                    Swap(ref controlScheme.controlAltButtons, i, i - 1);
                }
            }
            if (GUILayout.Button("v"))
            {
                if (i < controlScheme.controlNames.Count - 1)
                {
                    Swap(ref controlScheme.controlNames, i, i + 1);
                    Swap(ref controlScheme.controlButtons, i, i + 1);
                    Swap(ref controlScheme.controlAltButtons, i, i + 1);
                }
            }
            if (GUILayout.Button("+"))
            {
                controlScheme.controlNames.Insert(i + 1, "");
                controlScheme.controlButtons.Insert(i + 1, "");
                controlScheme.controlAltButtons.Insert(i, "");
            }
            if (GUILayout.Button("-"))
            {
                controlScheme.controlNames.RemoveAt(i);
                controlScheme.controlButtons.RemoveAt(i);
                controlScheme.controlAltButtons.RemoveAt(i);
            }
            GUILayout.EndHorizontal();
        }
        if (GUILayout.Button("Add Control"))
        {
            controlScheme.controlNames.Add("");
            controlScheme.controlButtons.Add("");
            controlScheme.controlAltButtons.Add("");
        }
        if (GUILayout.Button("Delete Control"))
        {
            controlScheme.controlNames.RemoveAt(controlScheme.controlNames.Count - 1);
            controlScheme.controlButtons.RemoveAt(controlScheme.controlButtons.Count - 1);
            controlScheme.controlAltButtons.RemoveAt(controlScheme.controlAltButtons.Count - 1);
        }
    }
    private void Swap(ref List<string> list, int index1, int index2)
    {
        string temp = list[index1];
        list[index1] = list[index2];
        list[index2] = temp;
    }
}
#endif