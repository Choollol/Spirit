using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;
using Codice.Client.BaseCommands.BranchExplorer;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    public static InputManager Instance { get { return instance; } }

    private static Dictionary<string, List<KeyCode>> controls = new Dictionary<string, List<KeyCode>>();
    public static Dictionary<string, List<KeyCode>> Controls
    {
        get { return controls; }
    }

    private int buttonToSwitch; // 0 for main control button, 1 for alternate control

    private static string path = "Assets/TextFiles/Controls.txt";
    [SerializeField] private TextAsset controlsFile;
    private List<string> controlsText = new List<string>();

    [SerializeField] private ScriptablePrimitive controlToEditData; // bools: doSwitch, isAlt. strings: name, keyCodeString

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        Load();
    }
    private void Update()
    {
        /*if (controlToEditData.bools[0])
        {
            SwitchControl(controlToEditData.strings[0], controlToEditData.strings[1], controlToEditData.bools[1]);
        }*/
    }
    public void SwitchControl(string name, string button, bool isAlt)
    {
        buttonToSwitch = isAlt ? 1 : 0;
        try
        {
            KeyCode key = Util.ToKeyCode(button);
            if (isAlt && controls[name].Count < 2)
            {
                controls[name].Add(key);
            }
            else
            {
                controls[name][buttonToSwitch] = key;
            }
        }
        catch
        {
            PauseMenuManager.Instance.InvalidButtonEntered();
        }
        controlToEditData.bools[0] = false;
        string controlType = isAlt ? "AltButton" : "Button";
        EventMessenger.TriggerEvent("UpdateControl" + name + controlType);
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
    public static void Save()
    {
        StreamWriter writer = new StreamWriter(path, false);

        foreach (var control in controls)
        {
            string text = control.Key + "\n" + control.Value[0] + "\n";
            if (control.Value.Count > 1)
            {
                text += control.Value[1] + "\n";
            }
            writer.WriteLine(text + "*");
        }
        writer.Close();

        /*controlScheme.controlNames = new List<string>();
        controlScheme.controlNames = new List<string>();
        controlScheme.controlNames = new List<string>();

        foreach (var control in controls)
        {
            controlScheme.controlNames.Add(control.Key);
            controlScheme.controlButtons.Add(control.Value[0].ToString());
            controlScheme.controlAltButtons.Add(control.Value[1].ToString());
        }*/
    }
    private void Load()
    {
        string[] temp = controlsFile.text.Trim().Split('*');
        for (int i = 0; i < temp.Length; i++)
        {
            controlsText.Add(temp[i]);
        }
        controlsText.RemoveAt(controlsText.Count - 1);
        foreach (string control in controlsText)
        {
            string[] parts = control.Trim().Split('\n');
            List<KeyCode> keysList = new List<KeyCode>();
            if (parts[1] != "")
            {
                keysList.Add(Util.ToKeyCode(parts[1]));
            }
            if (parts.Length < 3)
            {
                keysList.Add(KeyCode.None);
            }
            else if (parts[2] != "")
            {
                keysList.Add(Util.ToKeyCode(parts[2]));
            }

            controls.Add(parts[0].Trim(), keysList);
        }

        /*for (int i = 0; i < controlScheme.controlNames.Count; i++)
        {
            if (controlScheme.controlButtons[i] == "up" || controlScheme.controlButtons[i] == "down" ||
                controlScheme.controlButtons[i] == "left" || controlScheme.controlButtons[i] == "right")
            {
                controlScheme.controlButtons[i] += "arrow";
            }
            if (controlScheme.controlAltButtons[i] == "up" || controlScheme.controlAltButtons[i] == "down" ||
                controlScheme.controlAltButtons[i] == "left" || controlScheme.controlAltButtons[i] == "right")
            {
                controlScheme.controlAltButtons[i] += "arrow";
            }
            List<KeyCode> keysList = new List<KeyCode>();
            if (controlScheme.controlButtons[i] != "")
            {
                keysList.Add(Util.ToKeyCode(controlScheme.controlButtons[i]));
            }
            if (controlScheme.controlAltButtons[i] != "")
            {
                keysList.Add(Util.ToKeyCode(controlScheme.controlAltButtons[i]));
            }
            controls.Add(controlScheme.controlNames[i], keysList);
        }*/
    }
    private void OnGUI()
    {
        if (controlToEditData.bools[0] && Event.current.keyCode.ToString() != "None")
        {
            SwitchControl(controlToEditData.strings[0], Event.current.keyCode.ToString(), controlToEditData.bools[1]);
            PauseMenuManager.Instance.ControlEdited();
        }
    }
}