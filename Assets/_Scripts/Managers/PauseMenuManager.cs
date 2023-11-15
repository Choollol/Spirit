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
