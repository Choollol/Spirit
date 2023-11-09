using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuManager : MonoBehaviour
{
    private static PauseMenuManager instance;
    public static PauseMenuManager Instance { get { return instance; } }

    [SerializeField] private ScriptablePrimitive controlToEditData;

    [SerializeField] private List<GameObject> objectList;
    private Dictionary<string, GameObject> objectDict = new Dictionary<string, GameObject>();

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
    }
    void Start()
    {
        foreach (GameObject obj in objectList)
        {
            objectDict.Add(obj.name.ToCamelCase(), obj);
        }
    }

    void Update()
    {
        /*if (InputManager.GetButtonDown("Cancel"))
        {
            if (objectDict["controlsKeyWindow"].activeSelf)
            {
                objectDict["controlsKeyWindow"].SetActive(false);
            }
            else if (objectDict["editControlWindow"].activeSelf)
            {
                CloseControlSwitchWindow();
            }
        }*/
    }
    /*public void SwitchControl()
    {
        TMP_InputField inputField = objectDict["input"].GetComponent<TMP_InputField>();
        controlToEditData.strings[1] = inputField.text;
        controlToEditData.bools[0] = true;
        inputField.text = "";
        CloseControlSwitchWindow();
    }*/
    public void InvalidButtonEntered()
    {
        objectDict["invalidEntryText"].SetActive(true);
    }
    public void DeleteControl()
    {
        InputManager.Instance.SwitchControl(controlToEditData.strings[0], "None", controlToEditData.bools[1]);
    }
    public void EditControl()
    {
        objectDict["enterControlText"].SetActive(true);
        controlToEditData.bools[0] = true;
        GameManager.doCloseMenuOnCancel = false;
    }
    public void ControlEdited()
    {
        objectDict["enterControlText"].SetActive(false);
        StartCoroutine(HandleCanCloseMenu());
    }
    private IEnumerator HandleCanCloseMenu()
    {
        yield return new WaitForEndOfFrame();
        GameManager.doCloseMenuOnCancel = true;
    }
    public void ShowControlButtons(Vector2 pos, string name, bool isAlt)
    {
        objectDict["controlButtons"].SetActive(true);
        objectDict["controlButtons"].transform.position = pos;
        controlToEditData.strings[0] = name;
        controlToEditData.bools[1] = isAlt;
    }
    public void HideControlButtons()
    {
        objectDict["controlButtons"].SetActive(false);
    }
    /*public void OpenControlSwitchWindow()
    {
        objectDict["editControlWindow"].SetActive(true);
        GameManager.doCloseMenuOnCancel = false;
        objectDict["invalidEntryText"].SetActive(false);
    }
    private void CloseControlSwitchWindow()
    {
        objectDict["editControlWindow"].SetActive(false);
        CanCloseMenuOnCancel();
    }
    public void OpenControlsKeyWindow()
    {
        objectDict["controlsKeyWindow"].SetActive(true);
    }*/
}
