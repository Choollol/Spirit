using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject controlTextHolder;
    [SerializeField] private GameObject controlTextPrefab;
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
        }
    }

    void Update()
    {
        
    }
}
