using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private List<GameObject> uiList;
    [SerializeField] private List<Image> tabList;

    private Dictionary<string, GameObject> uiDict = new Dictionary<string, GameObject>();
    private Dictionary<string, Image> tabDict = new Dictionary<string, Image>();

    private string currentUI;

    private float inactiveTabOpacity = 0.5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        for (int i = 0; i < uiList.Count; i++)
        {
            uiDict.Add(uiList[i].name, uiList[i]);
            tabDict.Add(uiList[i].name, tabList[i]);
        }
    }
    private void Start()
    {
        SwitchUI(uiList[0].name);
    }
    private void Update()
    {
        if (InputManager.GetButtonDown("Move Left"))
        {
            SwitchUI("UI 1");
        }
        else if (InputManager.GetButtonDown("Move Right"))
        {
            SwitchUI("UI 2");
        }
    }
    public void ClearUI()
    {
        foreach (GameObject ui in uiList)
        {
            ui.SetActive(false);
        }
        foreach (Image tab in tabList)
        {
            if (tab.color.a > inactiveTabOpacity)
            {
                tab.SetAlpha(inactiveTabOpacity);
            }
        }
    }
    public void SwitchUI(string newUI)
    {
        ClearUI();
        currentUI = newUI;
        uiDict[currentUI].SetActive(true);
        tabDict[currentUI].SetAlpha(1);
        EventMessenger.TriggerEvent("UISwitched");
    }
}
