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

    private List<Image> controlButtonImages = new List<Image>();

    public Dictionary<string, int> controlButtonAmounts = new Dictionary<string, int>(); // Number of controls using each button

    private GameObject controlButtonsTarget;

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
        controlButtonImages.Add(objectDict["controlButtons"].GetComponent<Image>());
        for (int i = 0; i < objectDict["controlButtons"].transform.childCount; i++)
        {
            controlButtonImages.Add(objectDict["controlButtons"].transform.GetChild(i).GetComponent<Image>());
        }

        EventMessenger.TriggerEvent("UpdateVolumeSliders");
    }
    public void ToggleFullscreen()
    {
        if (Screen.fullScreenMode == FullScreenMode.FullScreenWindow)
        {
            objectDict["fullScreenModeText"].GetComponent<TextMeshProUGUI>().text = "Windowed";
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        else
        {
            objectDict["fullScreenModeText"].GetComponent<TextMeshProUGUI>().text = "Full Screen";
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
    }
    public void IncrementButtonAmount(string button)
    {
        if (!controlButtonAmounts.ContainsKey(button))
        {
            controlButtonAmounts.Add(button, 1);
        }
        else
        {
            controlButtonAmounts[button]++;
        }
    }
    public void DecrementButtonAmount(string button)
    {
        if (!controlButtonAmounts.ContainsKey(button))
        {
            controlButtonAmounts.Add(button, 0);
        }
        else if (controlButtonAmounts[button] > 0)
        {
            controlButtonAmounts[button]--;
            EventMessenger.TriggerEvent("UpdateColor" + button);
        }
    }
    public void DeleteControl()
    {
        InputManager.Instance.SwitchControl(controlToEditData.strings[0], "None", controlToEditData.bools[1]);
    }
    public void EditControl()
    {
        objectDict["enterControlText"].SetActive(true);
        objectDict["currentlyEditingText"].GetComponent<TextMeshProUGUI>().text = "Currently editing:\n" + 
            controlToEditData.strings[0] + ", " + (!controlToEditData.bools[1] ? "Main button" : "Alternate button");
        controlToEditData.bools[0] = true;
        GameManager.doCloseMenuOnCancel = false;
    }
    public void ControlEdited()
    {
        objectDict["enterControlText"].SetActive(false);
        StartCoroutine(HandleCanCloseMenu());
        AudioManager.PlaySound("Control Edited Sound", 0.6f, 1.5f, true);
    }
    private IEnumerator HandleCanCloseMenu()
    {
        yield return new WaitForEndOfFrame();
        GameManager.doCloseMenuOnCancel = true;
    }
    public void ShowControlButtons(GameObject obj, Vector2 pos, string name, bool isAlt)
    {
        objectDict["controlButtons"].SetActive(true);
        objectDict["controlButtons"].transform.position = pos;
        controlToEditData.strings[0] = name;
        controlToEditData.bools[1] = isAlt;
        controlButtonsTarget = obj;
    }
    public void HideControlButtons()
    {
        objectDict["controlButtons"].SetActive(false);
    }
    public void UpdateControlButtonsPosition()
    {
        if (controlButtonsTarget)
        {
            objectDict["controlButtons"].transform.position = controlButtonsTarget.transform.position;
        }
    }
    private void OnGUI()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            foreach (Image image in controlButtonImages)
            {
                image.raycastTarget = false;
            }
        }
        else
        {
            foreach (Image image in controlButtonImages)
            {
                image.raycastTarget = true;
            }
        }
    }
}
