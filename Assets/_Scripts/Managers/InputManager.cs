using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
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

        //LoadDefaultControls();
    }
    public static void SetControls(List<string> names, List<string> buttons, List<string> altButtons)
    {
        for (int i = 0; i < names.Count; i++)
        {
            List<KeyCode> keysList = new List<KeyCode>()
            {
                Util.ToKeyCode(buttons[i]),
                Util.ToKeyCode(altButtons[i])
            };
            controls.Add(names[i], new List<KeyCode>() { Util.ToKeyCode(buttons[i]), Util.ToKeyCode(altButtons[i]) });
        }
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
            Debug.Log("Invalid control");
        }
        controlToEditData.bools[0] = false;
        string controlType = isAlt ? "AltButton" : "Button";
        EventMessenger.TriggerEvent("UpdateControl" + name + controlType);
    }
    public static bool GetButtonDown(string name)
    {
        return Input.GetKeyDown(controls[name][0]) || Input.GetKeyDown(controls[name][1]);
        /*foreach (KeyCode key in controls[name])
        {
            if (Input.GetKeyDown(key))
            {
                return true;
            }
        }
        return false;*/
    }
    public static bool GetButton(string name)
    {
        return Input.GetKey(controls[name][0]) || Input.GetKey(controls[name][1]);
        /*foreach (KeyCode key in controls[name])
        {
            if (Input.GetKey(key))
            {
                return true;
            }
        }
        return false;*/
    }
    /*public static void Save()
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
    }*/
    public void LoadDefaultControls()
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
    }
    private void OnGUI()
    {
        if (controlToEditData.bools[0] && Event.current.keyCode.ToString() != "None")
        {
            if (!GetButtonDown("Cancel"))
            {
                SwitchControl(controlToEditData.strings[0], Event.current.keyCode.ToString(), controlToEditData.bools[1]);
            }
            PauseMenuManager.Instance.ControlEdited();
            controlToEditData.bools[0] = false;
        }
    }
}