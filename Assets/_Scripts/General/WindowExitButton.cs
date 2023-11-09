using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindowExitButton : MonoBehaviour
{
    private Button button;
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(CloseWindow);
    }
    private void CloseWindow()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
