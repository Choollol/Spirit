using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject controlTextHolder;
    [SerializeField] private GameObject controlTextPrefab;
    [SerializeField] private GameObject content;
    void Start()
    {
        foreach (var control in InputManager.Controls)
        {
            GameObject controlText = Instantiate(controlTextPrefab, controlTextHolder.transform);
            controlText.name = control.Key;
            for (int i = 0; i < controlText.transform.childCount; i++)
            {
                controlText.transform.GetChild(i).GetComponent<ControlText>().SetControlText();
            }
            RectTransform contentRect = content.GetComponent<RectTransform>();
            content.GetComponent<RectTransform>().sizeDelta += new Vector2(0, controlText.GetComponent<RectTransform>().sizeDelta.y);
        }
        content.GetComponent<RectTransform>().sizeDelta += new Vector2(0, 20);
    }

    void Update()
    {
        
    }
}
