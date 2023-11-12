using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlText : MonoBehaviour, IPointerEnterHandler
{
    public enum ControlType
    {
        Name, Button, AltButton
    }
    [SerializeField] private ControlType controlType;

    private TextMeshProUGUI text;
    private string controlName;
    private void OnEnable()
    {
        if (controlType != ControlType.Name)
        {
            EventMessenger.StartListening("UpdateControl" + controlName + controlType, UpdateText);
        }
    }
    private void OnDisable()
    {
        if (controlType != ControlType.Name)
        {
            EventMessenger.StopListening("UpdateControl" + controlName + controlType, UpdateText);
        }
    }
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        controlName = transform.parent.name;

        UpdateText();
    }
    public void SetControlText()
    {
        if (controlType != ControlType.Name)
        {
            EventMessenger.StopListening("UpdateControl" + controlName + controlType, UpdateText);
            controlName = transform.parent.name;
            EventMessenger.StartListening("UpdateControl" + controlName + controlType, UpdateText);
        }
    }
    private void UpdateText()
    {
        switch (controlType)
        {
            case ControlType.Name:
                {
                    text.text = controlName;
                    break;
                }
            case ControlType.Button:
                {
                    if (InputManager.Controls[controlName][0] == KeyCode.None)
                    {
                        text.text = "";
                    }
                    else
                    {
                        text.text = InputManager.Controls[controlName][0].ToString();
                    }
                    DeleteExtraneousText();
                    break;
                }
            case ControlType.AltButton:
                {
                    if (InputManager.Controls[controlName][1] == KeyCode.None)
                    {
                        text.text = "";
                    }
                    else if (InputManager.Controls[controlName].Count > 1)
                    {
                        text.text = InputManager.Controls[controlName][1].ToString();
                    }
                    DeleteExtraneousText();
                    break;
                }
        }
    }
    private void DeleteExtraneousText()
    {
        string s = text.text.ToLower();
        if (s.Contains("arrow"))
        {
            text.text = text.text.Substring(0, text.text.Length - "arrow".Length);
        }
        else if (s.Contains("alpha"))
        {
            text.text = text.text.Substring(text.text.Length - 1, 1);
        }
        else if (s.Contains("left") && s != "left")//s.Contains("bracket"))
        {
            text.text = text.text[0] + text.text.Substring(4);
        }
        else if (s.Contains("right") && s != "right")
        {
            text.text = text.text[0] + text.text.Substring(5);
        }
        else if (s == "backquote")
        {
            text.text = "Backtick";
        }
        else if (s == "semicolon")
        {
            text.text = "SemiCol";
        }
        else if (s == "backslash")
        {
            text.text = "BSlash";
        }
    }
    public void OnPointerEnter(PointerEventData data)
    {
        if (controlType != ControlType.Name)
        {
            PauseMenuManager.Instance.ShowControlButtons(transform.position, controlName, controlType == ControlType.AltButton);
        }
    }
}
